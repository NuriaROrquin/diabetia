using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using Amazon.Textract.Model;
using Amazon.Textract;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Diabetia.Domain.Exceptions;


namespace Diabetia.Infrastructure.Providers
{
    public class TagRecognitionProvider : ITagRecognitionProvider
    {
        private readonly IConfiguration _configuration;

        public TagRecognitionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<NutritionTag> GetChFromDocument(string ocrRequest)
        {

            string awsAccessKey = _configuration["AwsAccessKeyId"];
            string awsSecretKey = _configuration["AwsSecretAccessKey"];
            var region = RegionEndpoint.USEast2;
            var uniqueId = getUniqueId();

            string idOnBucket = $"Textract/{uniqueId}.jpg";

            await CreateObjectS3Async(ocrRequest, idOnBucket);

            DetectDocumentTextResponse result = await ProcessingTextract(awsAccessKey, awsSecretKey, region, idOnBucket);

            string jsonResponse = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(jsonResponse);

            NutritionTag textractResult = new NutritionTag();

            textractResult.UniqueId = uniqueId;
            textractResult.CarbohydratesText = string.Join(" ", result.Blocks.Where(b => b.BlockType == BlockType.LINE).Select(b => b.Text));

            return textractResult;

        }

        /// <summary>
        /// Guarda el archivo en base64 en el bucket S3 de amazon.
        /// </summary>
        /// <param name="file">El archivo en formato base64 que se desea guardar en el bucket S3.</param>
        /// <returns>Un identificador único (uniqueId) del archivo guardado en el bucket.</returns>
        /// <exception cref="CantCreatObjectS3Async"></exception>
        public async Task<string> SaveMedicalExaminationOnBucket(string file)
        {
            var uniqueId = getUniqueId();
            string idOnBucket = $"Textract/{uniqueId}.pdf";
            await CreateObjectS3Async(file, idOnBucket);
            return uniqueId;
        }

        /// <summary>
        /// Guarda el archivo en el Bucket S3 de Amazon.
        /// </summary>
        /// <param name="file">El archivo en formato base64 que se desea guardar en el bucket S3.</param>
        /// <param name="idOnBucket">Un identificador único del archivo para guardar en el bucket S3.</param>
        /// <returns>Un boolean acorde a si se pudo guardar o no el archivo</returns>
        private async Task CreateObjectS3Async(string file, string idOnBucket)
        {
            string awsAccessKey = _configuration["AwsAccessKeyId"];
            string awsSecretKey = _configuration["AwsSecretAccessKey"];
            string bucketName = _configuration["BucketName"];
            string regionSecret = _configuration["Region"];

            var region = RegionEndpoint.GetBySystemName(regionSecret);
            var client = new AmazonS3Client(awsAccessKey, awsSecretKey, region);

            byte[] imageData = Convert.FromBase64String(file);
            Stream imageStream = new MemoryStream(imageData);

            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = idOnBucket,
                InputStream = imageStream
            };
            await client.PutObjectAsync(request);
        }

        /// <summary>
        /// Genera un ID unico.
        /// </summary>
        /// <returns></returns>
        public String getUniqueId()
        {
            long timestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            string uniqueId = $"{timestamp}_{Guid.NewGuid()}";
            return uniqueId;
        }

        private async Task<DetectDocumentTextResponse> ProcessingTextract(string awsAccesKey, string awsSecretKey, RegionEndpoint region, string idOnBucket)
        {
            var client = new AmazonTextractClient(awsAccesKey, awsSecretKey, region);

            var request = new DetectDocumentTextRequest
            {
                Document = new Document
                {
                    S3Object = new Amazon.Textract.Model.S3Object
                    {
                        Bucket = "textract-console-us-east-2-7a438fab-112f-422b-98ba-cbc0d7f642e8",
                        Name = idOnBucket,
                    }

                }
            };

            var response = await client.DetectDocumentTextAsync(request);

            return response;
        }

        public async Task DeleteFileFromBucket(string idOnBucket)
        {
            string awsAccessKey = _configuration["AwsAccessKeyId"];
            string awsSecretKey = _configuration["AwsSecretAccessKey"];
            var region = RegionEndpoint.USEast2; ;

            var client = new AmazonS3Client(awsAccessKey, awsSecretKey, region);
            string idOnBucketComplete = $"Textract/{idOnBucket}.pdf";

            var result = await DeleteObjectS3Async(awsAccessKey, awsSecretKey, region, idOnBucketComplete);

            if (!result)
            {
                throw new CantDeleteObjectS3Async();
            }
        }

        private async Task<bool> DeleteObjectS3Async(string awsAccesKey, string awsSecretKey, RegionEndpoint region, string idOnBucketComplete)
        {
            var bucketName = "textract-console-us-east-2-7a438fab-112f-422b-98ba-cbc0d7f642e8";
            var objectKey = idOnBucketComplete;

            var client = new AmazonS3Client(awsAccesKey, awsSecretKey, region);

            try
            {
                await client.DeleteObjectAsync(bucketName, objectKey);
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine("Error al subir el objeto al bucket de Amazon S3: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
                return false;
            }
        }
    }
}

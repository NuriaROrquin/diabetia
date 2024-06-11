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
using Diabetia.Application.Exceptions;


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

            string awsAccessKey = _configuration["AWS_ACCESS_KEY_ID"];
            string awsSecretKey = _configuration["AWS_SECRET_ACCESS_KEY"];
            var region = RegionEndpoint.USEast2;
            var uniqueId = getUniqueId();

            string idOnBucket = $"Textract/{uniqueId}.jpg";

            await CreateObjectS3Async(awsAccessKey, awsSecretKey, region, ocrRequest, idOnBucket);

            DetectDocumentTextResponse result = await ProcessingTextract(awsAccessKey, awsSecretKey, region, idOnBucket);

            string jsonResponse = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(jsonResponse);

            NutritionTag textractResult = new NutritionTag();

            textractResult.UniqueId = uniqueId;
            textractResult.CarbohydratesText = string.Join(" ", result.Blocks.Where(b => b.BlockType == BlockType.LINE).Select(b => b.Text));

            return textractResult;

        }

        public async Task<string> SaveMedicalExaminationOnBucket(string file)
        {
            string awsAccessKey = _configuration["AWS_ACCESS_KEY_ID"];
            string awsSecretKey = _configuration["AWS_SECRET_ACCESS_KEY"];
            var region = RegionEndpoint.USEast2;
            var uniqueId = getUniqueId();

            string idOnBucket = $"Textract/{uniqueId}.pdf";

            var result = await CreateObjectS3Async(awsAccessKey, awsSecretKey, region, file, idOnBucket);

            if (!result)
            {
                throw new CantCreatObjectS3Async(); 
            }

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

        private async Task<bool> CreateObjectS3Async(string awsAccesKey, string awsSecretKey, RegionEndpoint region, string ocrRequest, string idOnBucket)
        {
            var bucketName = "textract-console-us-east-2-7a438fab-112f-422b-98ba-cbc0d7f642e8";
            var objectKey = idOnBucket;

            var client = new AmazonS3Client(awsAccesKey, awsSecretKey, region);

            var trasnferUtility = new TransferUtility(client);

            byte[] imageData = Convert.FromBase64String(ocrRequest);
            Stream imageStream = new MemoryStream(imageData);

            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = objectKey,
                InputStream = imageStream
            };

            try
            {
                await client.PutObjectAsync(request);
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

        public String getUniqueId()
        {
            long timestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            string uniqueId = $"{timestamp}_{Guid.NewGuid()}";
            return uniqueId;
        }

        public async Task DeleteFileFromBucket(string idOnBucket)
        {
            string awsAccessKey = _configuration["AWS_ACCESS_KEY_ID"];
            string awsSecretKey = _configuration["AWS_SECRET_ACCESS_KEY"];
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

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


namespace Diabetia.Infrastructure.Providers
{
    public class ApiAmazonProvider : IApiAmazonProvider
    {
        private readonly IConfiguration _configuration;

        public ApiAmazonProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetChFromDocument(string ocrRequest)
        {
            string awsAccessKey = _configuration["awsAccessKey"];
            string awsSecretKey = _configuration["awsSecretKey"];
            var region = RegionEndpoint.USEast2;
            var uniqueId = getUniqueId();

            await CreateObjectS3Async(awsAccessKey, awsSecretKey, region, ocrRequest, uniqueId);

            DetectDocumentTextResponse result = await ProcessingTextract(awsAccessKey, awsSecretKey, region, uniqueId);

            string jsonResponse = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(jsonResponse);

            NutritionTag textractResult = new NutritionTag();

            textractResult.CarbohydratesText = string.Join(" ", result.Blocks.Where(b => b.BlockType == BlockType.LINE).Select(b => b.Text));

            return textractResult.CarbohydratesText;

        }

        private async Task<DetectDocumentTextResponse> ProcessingTextract(string awsAccesKey, string awsSecretKey, RegionEndpoint region, string uniqueId)
        {
            var client = new AmazonTextractClient(awsAccesKey, awsSecretKey, region);

            var request = new DetectDocumentTextRequest
            {
                Document = new Document
                {
                    S3Object = new Amazon.Textract.Model.S3Object
                    {
                        Bucket = "textract-console-us-east-2-7a438fab-112f-422b-98ba-cbc0d7f642e8",
                        Name = $"Textract/{uniqueId}.jpg"
                    }

                }
            };

            var response = await client.DetectDocumentTextAsync(request);

            return response;
        }

        private async Task<bool> CreateObjectS3Async(string awsAccesKey, string awsSecretKey, RegionEndpoint region, string ocrRequest, string uniqueId)
        {
            var bucketName = "textract-console-us-east-2-7a438fab-112f-422b-98ba-cbc0d7f642e8";
            var objectKey = $"Textract/{uniqueId}.jpg";

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
    }
}

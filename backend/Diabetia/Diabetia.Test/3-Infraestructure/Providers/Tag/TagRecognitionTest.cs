using Amazon.S3;
using Amazon.S3.Model;
using Diabetia.Infrastructure.Providers;
using FakeItEasy;
using Microsoft.Extensions.Configuration;

namespace Diabetia_Infrastructure.Providers
{
    public class TagRecognitionTest
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _amazonS3Client;
        private readonly TagRecognitionProvider _tagRecognitionProvider;
        public TagRecognitionTest()
        {
            _configuration = A.Fake<IConfiguration>();
            _amazonS3Client = A.Fake<IAmazonS3>();

            A.CallTo(() => _configuration["AwsAccessKeyId"]).Returns("fakeAccessKeyId");
            A.CallTo(() => _configuration["AwsSecretAccessKey"]).Returns("fakeSecretAccessKey");
            A.CallTo(() => _configuration["BucketName"]).Returns("fakeBucketName");
            A.CallTo(() => _configuration["Region"]).Returns("us-east-1");

            _tagRecognitionProvider = new TagRecognitionProvider(_configuration, _amazonS3Client);
        }

        [Fact]
        public async Task TagRecognitionProvider_WhenCallWithValidData_ShouldSaveExaminationOnBucketSuccessfully()
        {

            // Arrange
            string base64File = "fakeBase64String";
            byte[] imageData = new byte[] { 0x01, 0x02, 0x03 };
            var memoryStream = new MemoryStream(imageData);

            A.CallTo(() => _amazonS3Client.PutObjectAsync(A<PutObjectRequest>._, A<CancellationToken>._))
                .Returns(Task.FromResult(new PutObjectResponse()));

            // Act
            var result = await _tagRecognitionProvider.SaveMedicalExamination(base64File);

            // Assert
            Assert.NotNull(result);
        }
    }


}

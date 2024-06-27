using Diabetia.Domain.Entities;

namespace Diabetia.Domain.Services
{
    public interface ITagRecognitionProvider
    {
        public Task DeleteFileFromBucket(string idOnBucket);
        public Task<NutritionTag> GetChFromDocument(string ocrRequest);
        public Task<string> SaveMedicalExaminationOnBucket(string file);
    }
}

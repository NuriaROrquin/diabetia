using System;
using Diabetia.Domain.Entities;

namespace Diabetia.Domain.Services
{
    public interface ITagRecognitionProvider
    {
        public Task<NutritionTag> GetChFromDocument(string ocrRequest);
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class FoodDish
    {
        public string? ImageBase64 { get; set; }

        [JsonPropertyName("imageId")]
        public int ImageId { get; set; }

        [JsonPropertyName("segmentationResults")]
        public IEnumerable<SegmentationResult> SegmentationResults { get; set; }

        public FoodDish()
        {
            SegmentationResults = new List<SegmentationResult>();
        }

    }

    public class SegmentationResult
    {
        [JsonPropertyName("foodItemPosition")]
        public int FoodItemPosition { get; set; }

        [JsonPropertyName("recognitionResults")]
        public IEnumerable<RecognitionResult> RecognitionResults { get; set; }

        public SegmentationResult()
        {
            RecognitionResults = new List<RecognitionResult>();
        }
    }

    public class RecognitionResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("prob")]
        public float Prob { get; set; }

    }
    /*
    public class NutriScore
    {
        public string NutriScoreCategory { get; set; }
        public int NutriScoreStandardized { get; set; }
    }*/
}
using System.Text.Json.Serialization;

namespace Diabetia.Domain.Entities
{
    public class FoodDish
    {
        public string? ImageBase64 { get; set; }

        [JsonPropertyName("imageId")]
        public int ImageId { get; set; }

        [JsonPropertyName("segmentation_results")]
        public IEnumerable<SegmentationResult>? SegmentationResults { get; set; }

    }

    public class FoodInfo
    {
        public double Carbohydrates { get; set; }
        public int Quantity { get; set; }
    }

    public class SegmentationResult
    {
        [JsonPropertyName("food_item_position")]
        public int FoodItemPosition { get; set; }

        [JsonPropertyName("recognition_results")]
        public IEnumerable<RecognitionResult>? RecognitionResults { get; set; }
    }

    public class RecognitionResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("prob")]
        public float Prob { get; set; }

    }
}
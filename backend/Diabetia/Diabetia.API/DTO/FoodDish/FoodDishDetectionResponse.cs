using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.FoodDish
{
    public class FoodDishDetectionResponse
    {
        public int ImageId { get; set; }
        public List<SegmentationResult> SegmentationResults { get; set; }

        public FoodDishDetectionResponse()
        {
            SegmentationResults = new List<SegmentationResult>();
        }

    }

    public class SegmentationResult
    {
        public int FoodItemPosition { get; set; }
        public List<RecognitionResult> RecognitionResults { get; set; }

    }

    public class RecognitionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prob { get; set; }
    }

}
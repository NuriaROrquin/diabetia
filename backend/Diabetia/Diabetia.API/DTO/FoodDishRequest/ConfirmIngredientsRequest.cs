using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.FoodDishRequest
{
    public class ConfirmIngredientsRequest
    {
        public int ImageId { get; set; }
        public List<SegmentationResultDTO> SegmentationResults { get; set; }

        public FoodDish ToDomain()
        {
            var foodDish = new FoodDish()
            {
                ImageId = ImageId,
                SegmentationResults = ConvertToDomainSegmentationResults(SegmentationResults)
            };

            return foodDish;
        }

        private IEnumerable<SegmentationResult> ConvertToDomainSegmentationResults(List<SegmentationResultDTO> dtoSegmentationResults)
        {
            if (dtoSegmentationResults == null)
                return null;

            return dtoSegmentationResults.Select(dtoSeg => new SegmentationResult
            {
                FoodItemPosition = dtoSeg.FoodItemPosition,
                RecognitionResults = ConvertToDomainRecognitionResults(dtoSeg.RecognitionResults)
            }).ToList();
        }

        private IEnumerable<RecognitionResult> ConvertToDomainRecognitionResults(IEnumerable<RecognitionResultDTO> dtoRecognitionResults)
        {
            if (dtoRecognitionResults == null)
                return null;

            return dtoRecognitionResults.Select(dtoRec => new RecognitionResult
            {
                Id = dtoRec.Id,
                Name = dtoRec.Name,
                Prob = dtoRec.Prob
            }).ToList();
        }
    }

    public class SegmentationResultDTO
    {
        public int FoodItemPosition { get; set; }
        public List<RecognitionResultDTO> RecognitionResults { get; set; }
    }

    public class RecognitionResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prob { get; set; }
    }
}

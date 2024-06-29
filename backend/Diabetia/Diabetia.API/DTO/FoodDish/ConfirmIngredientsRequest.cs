namespace Diabetia.API.DTO.FoodDish
{
    public class ConfirmIngredientsRequest
    {
        public int ImageId { get; set; }
        public List<SegmentationResult> SegmentationResults { get; set; }
    }
}
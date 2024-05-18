namespace Diabetia.API.DTO
{
    public class OcrResponse
    {
        public string CarbohydratesText { get; set; }
        public float portion { get; set; }
        public float grPerPortion { get; set; }
        public float chInPortion { get; set; }
    }
}

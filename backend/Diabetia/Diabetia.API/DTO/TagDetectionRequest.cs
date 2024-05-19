namespace Diabetia.API.DTO
{
    public class TagDetectionRequest
    {
        public string ImageBase64 { get; set; }

        public float portion { get; set; }
    }
}

namespace Diabetia.API.DTO
{
    public class TagDetectionRequest
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ImageBase64 { get; set; }

        public float Portion { get; set; }

        public bool SavePreference { get; set; }
    }
}

namespace Diabetia.API.DTO.TagResponse
{
    public class TagDetectionResponse
    {
        public string Id { get; set; }
        public float Portion { get; set; }
        public float GrPerPortion { get; set; }
        public float ChInPortion { get; set; }

        public string UniqueIdTag { get; set; }
    }
}

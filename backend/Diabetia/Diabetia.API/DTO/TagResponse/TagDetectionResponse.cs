namespace Diabetia.API.DTO.TagResponse
{
    public class TagDetectionResponse
    {
        //public string CarbohydratesText { get; set; }
        //Se en el string anterior antes viajaba la cantidad de ch consumida. En este DTO quedo deprecado, pasó al TagRegistration
        public string Id { get; set; }
        public float Portion { get; set; }
        public float GrPerPortion { get; set; }
        public float ChInPortion { get; set; }

        public string UniqueIdTag { get; set; }
    }
}

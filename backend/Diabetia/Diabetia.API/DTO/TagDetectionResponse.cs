namespace Diabetia.API.DTO
{
    public class TagDetectionResponse
    {
        //public string CarbohydratesText { get; set; }
        //Se en el string anterior antes viajaba la cantidad de ch consumida. En este DTO quedo deprecado, pasó al TagRegistration
        public float portion { get; set; }
        public float grPerPortion { get; set; }
        public float chInPortion { get; set; }
    }
}

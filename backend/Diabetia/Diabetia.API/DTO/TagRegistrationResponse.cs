namespace Diabetia.API.DTO
{
    public class TagRegistrationResponse
    {
        public List<PerTag> Tags { get; set; }

        public float ChTotal { get; set; }

        public TagRegistrationResponse()
        {
            Tags = new List<PerTag>();
        }
    }

    public class PerTag
    {
        public string Id { get; set; }

        public float Portion { get; set; }

        public float GrPerPortion { get; set; }

        public float ChInPortion { get; set; }

        public float ChCalculated { get; set; }
    }
}

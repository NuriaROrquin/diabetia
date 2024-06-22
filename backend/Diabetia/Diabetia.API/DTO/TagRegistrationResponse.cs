using Diabetia.API.DTO.EventResponse.Food;

namespace Diabetia.API.DTO
{
    public class TagRegistrationResponse : FoodResponse
    {
        public List<ResponsePerTag> Tags { get; set; }
        public TagRegistrationResponse()
        {
            Tags = new List<ResponsePerTag>();
        }
    }

    public class ResponsePerTag
    {
        public string Id { get; set; }

        public float Portion { get; set; }

        public float GrPerPortion { get; set; }

        public float ChInPortion { get; set; }

        public float ChCalculated { get; set; }

        public string UniqueId { get; set; }
    }
}

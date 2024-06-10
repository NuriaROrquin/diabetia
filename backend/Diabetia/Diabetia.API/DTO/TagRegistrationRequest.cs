using Diabetia.API.DTO.EventRequest;

namespace Diabetia.API.DTO
{
    public class TagRegistrationRequest : BasicEventRequest
    {
        public string Id { get; set; }

        public float Portion { get; set; }

        public float grPerPortion { get; set; }

        public float ChInPortion { get; set; }
    }
}

using Diabetia.API.DTO.EventRequest;

namespace Diabetia.API.DTO.TagRequestFromBody
{
    public class TagRegistrationRequest : BasicEventRequest
    {
        public List<RequestPerTag> Tags { get; set; }

        public TagRegistrationRequest()
        {
            Tags = new List<RequestPerTag>();
        }
    }
    public class RequestPerTag
    {
        public string Id { get; set; }

        public float Portion { get; set; }

        public float GrPerPortion { get; set; }

        public float ChInPortion { get; set; }

    }
}

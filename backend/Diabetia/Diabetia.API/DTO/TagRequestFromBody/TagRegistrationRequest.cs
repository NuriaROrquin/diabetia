using Diabetia.API.DTO.EventRequest;
using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.TagRequestFromBody
{
    public class TagRegistrationRequest : BasicEventRequest
    {
        public List<RequestPerTag> Tags { get; set; }

        public TagRegistrationRequest()
        {
            Tags = new List<RequestPerTag>();
        }

        public List<NutritionTag> ToDomain()
        {
            return Tags.Select(tag => new NutritionTag
            {
                UniqueId = string.Empty,
                Portion = tag.Portion,
                GrPerPortion = tag.GrPerPortion,
                ChInPortion = tag.ChInPortion,
                ChCalculated = 0, 
                CarbohydratesText = string.Empty
            }).ToList();
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

using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.HomeRequest
{
    public class TimelineResponse
    {
        public TimelineResponse(Timeline timeline)
        {
            this.Timeline = timeline;
        }

        public Timeline Timeline { get; set; }
    }

}

using Diabetia.Domain.Utilities;

namespace Diabetia.API.DTO.HomeRequest
{
    public class MetricsRequest
    {
        public string Email { get; set; }
        public DateFilter? DateFilter { get; set; }
    }

}

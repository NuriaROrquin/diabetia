using Diabetia.Common.Utilities;

namespace Diabetia.API
{
    public class MetricsRequest
    {
        public string Email { get; set; }
        public DateFilter? DateFilter { get; set; }
    }

}

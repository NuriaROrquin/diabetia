using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Common.Utilities;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace Diabetia.Application.UseCases
{
    public class HomeUseCase
    {
        private readonly IHomeRepository _homeRepository;

        public HomeUseCase(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<Metrics> ShowMetrics(string Email, int Timelapse)
        {
            Metrics metrics = new Metrics();

            metrics.PhysicalActivity = await _homeRepository.GetPhysicalActivity(Email, (int)TypeEventEnum.ACTIVIDADFISICA, Timelapse);

            metrics.Carbohydrates = await _homeRepository.GetChMetrics(Email, (int)TypeEventEnum.COMIDA, Timelapse);

            metrics.Glycemia = await _homeRepository.GetGlucose(Email, (int)TypeEventEnum.GLUCOSA, Timelapse);

            metrics.Hyperglycemia =  await _homeRepository.GetHyperglycemia(Email, Timelapse);

            metrics.Hypoglycemia = await _homeRepository.GetHypoglycemia(Email, Timelapse);

            metrics.Insulin = await _homeRepository.GetInsulin(Email, (int)TypeEventEnum.INSULINA, Timelapse);

            return metrics;
        }
    }
}

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

        public async Task<Metrics> ShowMetrics(string Email, DateFilter? dateFilter)
        {
            Metrics metrics = new Metrics();

            metrics.PhysicalActivity = await _homeRepository.GetPhysicalActivity(Email, (int)TypeEventEnum.ACTIVIDADFISICA, dateFilter);

            metrics.Carbohydrates = await _homeRepository.GetChMetrics(Email, (int)TypeEventEnum.COMIDA, dateFilter);

            metrics.Glycemia = await _homeRepository.GetGlucose(Email, (int)TypeEventEnum.GLUCOSA, dateFilter);

            metrics.Hyperglycemia =  await _homeRepository.GetHyperglycemia(Email, dateFilter);

            metrics.Hypoglycemia = await _homeRepository.GetHypoglycemia(Email, dateFilter);

            metrics.Insulin = await _homeRepository.GetInsulin(Email, (int)TypeEventEnum.INSULINA, dateFilter);

            return metrics;

            
        }

    }
}

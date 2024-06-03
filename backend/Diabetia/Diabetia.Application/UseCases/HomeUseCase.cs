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

        public async Task<Metrics> ShowMetrics(string Email)
        {
            Metrics Metrics = new Metrics();

            Metrics.PhysicalActivity = await _homeRepository.GetPhysicalActivity(Email, (int)TypeEventEnum.ACTIVIDADFISICA);

            Metrics.Carbohydrates = await _homeRepository.GetChMetrics(Email, (int)TypeEventEnum.COMIDA);

            Metrics.Glycemia = await _homeRepository.GetGlucose(Email, (int)TypeEventEnum.GLUCOSA);

            Metrics.Hyperglycemia =  await _homeRepository.GetHyperglycemia(Email);

            Metrics.Hypoglycemia = await _homeRepository.GetHypoglycemia(Email);

            Metrics.Insulin = await _homeRepository.GetInsulin(Email, (int)TypeEventEnum.INSULINA);

            return Metrics;
        }
    }
}

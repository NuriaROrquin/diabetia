using Amazon.Runtime.Internal;
using Diabetia.Domain.Services;
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

        public async Task PhysicalActivity(int idUser, int IdEvento)
        {
            await _homeRepository.GetPhysicalActivity(idUser, IdEvento);
        }

    }
}

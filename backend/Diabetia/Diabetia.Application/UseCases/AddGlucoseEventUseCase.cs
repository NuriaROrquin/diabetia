using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases
{
    public class AddGlucoseEventUseCase
    {
        private readonly IGlucoseEventRepository _glucoseEventRepository;

        public AddGlucoseEventUseCase(IGlucoseEventRepository glucoseEventRepository)
        {
            _glucoseEventRepository = glucoseEventRepository;
        }
        public async Task AddGlucoseEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdDevicePacient, int? IdFoodEvent, bool? PostFoodMedition)
        {
            await _glucoseEventRepository.AddGlucoseEvent(Email, KindEvent, EventDate, FreeNote, Glucose, IdDevicePacient, IdFoodEvent, PostFoodMedition);
        }
    }
}

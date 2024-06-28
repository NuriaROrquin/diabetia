using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.ReportingUseCases
{
    public class FoodReportUseCase
    {
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IReportingRepository _reportingRepository;
        public FoodReportUseCase(IPatientValidator patientValidator, IUserRepository userRepository, IReportingRepository reportingRepository)
        {
            _patientValidator = patientValidator;
            _userRepository = userRepository;
            _reportingRepository = reportingRepository;
        }
        
        public async Task<List<EventSummary>> GetFoodToReporting(string email, DateTime dateFrom, DateTime dateTo)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfFoodConsumed = await _reportingRepository.GetFoodEventSummaryByPatientId(patient.Id, dateFrom, dateTo);
            if (listOfFoodConsumed == null || listOfFoodConsumed.Count == 0)
            {
                return new List<EventSummary>();
            }
            return listOfFoodConsumed;
        }
    }

    
}

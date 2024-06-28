
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.ReportingUseCases
{
    public class PhysicalActivityReportUseCase
    {
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IReportingRepository _reportingRepository;
        public PhysicalActivityReportUseCase(IPatientValidator patientValidator, IUserRepository userRepository, IReportingRepository reportingRepository) 
        { 
            _patientValidator = patientValidator;
            _userRepository = userRepository;
            _reportingRepository = reportingRepository;
        }

        public async Task<List<EventSummary>> GetPhysicalActivityToReporting(string email, DateTime dateFrom, DateTime dateTo)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfPhysicalActivities = await _reportingRepository.GetPhysicalActivityEventSummaryByPatientId(patient.Id, dateFrom, dateTo);
            if (listOfPhysicalActivities == null || listOfPhysicalActivities.Count == 0)
            {
                return new List<EventSummary>();
            }
            return listOfPhysicalActivities;
        }
    }
}

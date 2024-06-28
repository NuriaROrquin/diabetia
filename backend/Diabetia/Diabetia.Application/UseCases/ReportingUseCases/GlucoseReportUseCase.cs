
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.ReportingUseCases
{
    public class GlucoseReportUseCase
    {
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IReportingRepository _reportingRepository;
        public GlucoseReportUseCase(IPatientValidator patientValidator, IUserRepository userRepository, IReportingRepository reportingRepository)
        {
            _patientValidator = patientValidator;
            _userRepository = userRepository;
            _reportingRepository = reportingRepository;
        }

        public async Task<List<EventSummary>> GetGlucoseToReporting(string email, DateTime dateFrom, DateTime dateTo)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfGlucoseMeasures = await _reportingRepository.GetAmountGlucoseEventsToReportByPatientId(patient.Id, dateFrom, dateTo);
            if (listOfGlucoseMeasures == null || listOfGlucoseMeasures.Count == 0)
            {
                return new List<EventSummary>();
            }
            return listOfGlucoseMeasures;
        }
    }
}

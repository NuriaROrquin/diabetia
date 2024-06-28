using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Domain.Utilities;
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
            var listOfGlucoseMeasures = await _reportingRepository.GetGlucoseEventSummaryByPatientId(patient.Id, dateFrom, dateTo);
            if (listOfGlucoseMeasures == null || listOfGlucoseMeasures.Count == 0)
            {
                return new List<EventSummary>();
            }
            return listOfGlucoseMeasures;
        }

        public async Task<List<GlucoseMeasurement>> GetHyperglycemiaGlucoseToReporting(string email)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfGlucoseMeasures = await _reportingRepository.GetHyperglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPERGLUCEMIA);
            if (listOfGlucoseMeasures == null || listOfGlucoseMeasures.Count == 0)
            {
                return new List<GlucoseMeasurement>();
            }
            return listOfGlucoseMeasures;
        }

        public async Task<List<GlucoseMeasurement>> GetHypoglycemiaGlucoseToReporting(string email)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfGlucoseMeasures = await _reportingRepository.GetHypoglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPOGLUCEMIA);
            if (listOfGlucoseMeasures == null || listOfGlucoseMeasures.Count == 0)
            {
                return new List<GlucoseMeasurement>();
            }
            return listOfGlucoseMeasures;
        }
    }
}

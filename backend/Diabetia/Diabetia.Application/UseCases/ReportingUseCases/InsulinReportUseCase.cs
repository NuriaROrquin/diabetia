using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.ReportingUseCases
{
    public class InsulinReportUseCase
    {
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IReportingRepository _reportingRepository;
        public InsulinReportUseCase(IPatientValidator patientValidator, IUserRepository userRepository, IReportingRepository reportingRepository)
        {
            _patientValidator = patientValidator;
            _userRepository = userRepository;
            _reportingRepository = reportingRepository;
        }

        public async Task<List<EventoInsulina>> GetInsulinToReporting(string email, DateTime dateFrom, DateTime dateTo)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfInsulinEvents = await _reportingRepository.GetInsulinEventsToReportByPatientId(patient.Id, dateFrom, dateTo);
            if (listOfInsulinEvents == null || listOfInsulinEvents.Count == 0)
            {
                return new List<EventoInsulina>();
            }

            return listOfInsulinEvents;
        }
    }
}
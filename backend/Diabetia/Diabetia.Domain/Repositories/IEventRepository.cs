using Diabetia.Domain.Entities.Events;

namespace Diabetia.Domain.Repositories
{
    public interface IEventRepository 
    {
        public Task AddPhysicalActivityEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime);

        public Task AddGlucoseEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdDevicePacient, int? IdFoodEvent, bool? PostFoodMedition);

        public Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin);

        public Task<IEnumerable<PhysicalActivityEvent>> GetPhysicalActivity(int patientId);

        public Task<IEnumerable<FoodEvent>> GetFoods(int patientId);

        public Task<IEnumerable<ExamEvent>> GetExams(int patientId);

        public Task<IEnumerable<GlucoseEvent>> GetGlycemia(int patientId);

        public Task<IEnumerable<InsulinEvent>> GetInsulin(int patientId);

        public Task<IEnumerable<HealthEvent>> GetHealth(int patientId);

        public Task<IEnumerable<MedicalVisitEvent>> GetMedicalVisit(int patientId);


    }
}

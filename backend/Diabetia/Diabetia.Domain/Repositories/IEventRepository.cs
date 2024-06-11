using Diabetia.Domain.Entities;
﻿using Diabetia.Common.Utilities;
using Diabetia.Domain.Entities.Events;

namespace Diabetia.Domain.Repositories
{
    public interface IEventRepository 
    {
        public Task AddPhysicalActivityEventAsync(string Email, int KindEvent, DateTime EventDate, String FreeNote, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime);

        public Task EditPhysicalActivityEventAsync(string Email, int EventId, DateTime EventDate, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime, string FreeNote);

        public Task DeletePhysicalActivityEventAsync(int IdEvent);

        public Task AddGlucoseEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition);

        public Task EditGlucoseEvent(int IdEvent, string Email, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition);

        public Task DeleteGlucoseEvent(int IdEvent);

        public Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin);

        public Task EditInsulinEvent(int IdEvent, string Email, DateTime EventDate, String FreeNote, int Insulin);

        public Task DeleteInsulinEvent(int IdEvent);
        
        public Task<float> AddFoodManuallyEvent(string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote);

        public Task EditFoodManuallyEvent(int idEvent, string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote);

        public Task<IEnumerable<AdditionalDataIngredient>> GetIngredients();

        public Task<IEnumerable<PhysicalActivityEvent>> GetPhysicalActivity(int patientId, DateTime? date);

        public Task<IEnumerable<FoodEvent>> GetFoods(int patientId, DateTime? date);

        public Task<IEnumerable<ExamEvent>> GetExams(int patientId, DateTime? date);

        public Task<IEnumerable<GlucoseEvent>> GetGlycemia(int patientId, DateTime? date);

        public Task<IEnumerable<InsulinEvent>> GetInsulin(int patientId, DateTime? date);

        public Task<IEnumerable<HealthEvent>> GetHealth(int patientId, DateTime? date);

        public Task<IEnumerable<MedicalVisitEvent>> GetMedicalVisit(int patientId, DateTime? date);

        public Task<TypeEventEnum> GetEventType(int idEvent);

        public Task<GlucoseEvent> GetGlucoseEventById(int idEvent);

        public Task<InsulinEvent> GetInsulinEventById(int id);

        public Task<FoodEvent> GetFoodEventById(int id);

        Task<PhysicalActivityEvent> GetPhysicalActivityById(int id);

        Task<MedicalVisitEvent> GetMedicalVisitEventById(int id);

        Task<HealthEvent> GetHealthEventById(int id);

        Task<ExamEvent> GetExamEventById(int id);

        Task<ExamEvent> GetFreeNoteEventById(int id);
    }
}

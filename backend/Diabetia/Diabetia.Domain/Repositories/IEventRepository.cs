﻿using Diabetia.Domain.Entities;
﻿using Diabetia.Domain.Utilities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;


namespace Diabetia.Domain.Repositories
{
    public interface IEventRepository 
    {
        // --------------------------------------- ⬇⬇ Physical Activity ⬇⬇ ---------------------------------------------------
        public Task AddPhysicalActivityEventAsync(int patientId, EventoActividadFisica physicalActivity);

        public Task EditPhysicalActivityEventAsync(EventoActividadFisica physicalActivity);

        public Task DeletePhysicalActivityEventAsync(int IdEvent);

        // -------------------------------------------- ⬇⬇ Glucose ⬇⬇ ---------------------------------------------------------
        public Task AddGlucoseEventAsync(int patientId, EventoGlucosa glucose);

        public Task EditGlucoseEventAsync(EventoGlucosa glucose);

        public Task DeleteGlucoseEventAsync(int IdEvent);

        // -------------------------------------------- ⬇⬇ Insuline ⬇⬇ ---------------------------------------------------------
        public Task AddInsulinEventAsync(int patientId, EventoInsulina insulin);

        public Task EditInsulinEventAsync(EventoInsulina insulin);

        public Task DeleteInsulinEventAsync(int IdEvent);

        // -------------------------------------------- ⇊ Food Manually ⇊ ----------------------------------------------------
        public Task<float> AddFoodEventAsync(int patientId, EventoComidum foodEvent);

        public Task<float> EditFoodEventAsync(EventoComidum foodEvent);

        public Task DeleteFoodEventAsync(int patientId);

        // -------------------------------------------- ⬇⬇ Tag Food ⬇⬇ ---------------------------------------------------------
        public Task AddFoodByTagEvent(int patientId, DateTime eventDate, int carbohydrates);

        public Task DeleteFoodEven(int id);

        // -------------------------------------------- ⬇⬇ Medical Examination ⬇⬇ -----------------------------------------------
        public Task AddMedicalExaminationEventAsync(int patientId, EventoEstudio medicalExamination, string fileSavedId);

        public Task<string> DeleteMedicalExaminationEvent(int id);

        // ------------------------------------------- ⬇⬇ Medical Visit ⬇⬇ ---------------------------------------------------------
        public Task AddMedicalVisitEventAsync(int patientId, EventoVisitaMedica medicalVisit);
        public Task EditMedicalVisitEventAsync(EventoVisitaMedica medicalVisit);
        public Task DeleteMedicalVisitEventAsync(int eventId);

        // ------------------------------------------- ⬇⬇ Free Note ⬇⬇ ---------------------------------------------------------

        public Task AddFreeNoteEventAsync(int patientId, CargaEvento freeNote);
        public Task EditFreeNoteEventAsync(CargaEvento freeNoteEvent);
        public Task DeleteFreeNoteEventAsync(int eventId);

        // ------------------------------------------- ⬇⬇ General Gets ⬇⬇ ----------------------------------------------------------

        public Task<CargaEvento> GetEventByIdAsync(int eventId);
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

        public Task <bool> CheckPatientEvent(string email, CargaEvento eventToValidate);
        
    }
}

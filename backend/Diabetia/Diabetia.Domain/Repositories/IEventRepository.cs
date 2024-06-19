using Diabetia.Domain.Entities;
﻿using Diabetia.Domain.Utilities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Diabetia.Domain.Repositories
{
    public interface IEventRepository 
    {
        // --------------------------------------- ⇊ Physical Activity ⇊ ---------------------------------------------------
        public Task AddPhysicalActivityEventAsync(int patientId, EventoActividadFisica physicalActivity);

        public Task EditPhysicalActivityEventAsync(EventoActividadFisica physicalActivity);

        public Task DeletePhysicalActivityEventAsync(int IdEvent);

        // -------------------------------------------- ⇊ Glucose ⇊ ---------------------------------------------------------
        public Task AddGlucoseEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition);

        public Task EditGlucoseEvent(int IdEvent, string Email, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition);

        public Task DeleteGlucoseEvent(int IdEvent);

        // -------------------------------------------- ⇊ Insuline ⇊ ---------------------------------------------------------
        public Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin);

        public Task EditInsulinEvent(int IdEvent, string Email, DateTime EventDate, String FreeNote, int Insulin);

        public Task DeleteInsulinEvent(int IdEvent);

        // -------------------------------------------- ⇊ Food Manually ⇊ ----------------------------------------------------
        public Task<float> AddFoodManuallyEvent(string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote);

        public Task EditFoodManuallyEvent(int idEvent, string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote);

        // -------------------------------------------- ⇊ Tag Food ⇊ ---------------------------------------------------------
        public Task AddFoodByTagEvent(string email, DateTime eventDate, int carbohydrates);

        public Task DeleteFoodEven(int id);

        // -------------------------------------------- ⇊ Medical Examination ⇊ -----------------------------------------------
        public Task AddMedicalExaminationEvent(string email, DateTime eventDate, string fileSaved, string examinationType, int? idProfessional, string? freeNote);

        public Task<string> DeleteMedicalExaminationEvent(int id);

        // ------------------------------------------- Medical Visit ---------------------------------------------------------
        public Task AddMedicalVisitEventAsync(int patientId, EventoVisitaMedica medicalVisit);
        public Task EditMedicalVisitEventAsync(EventoVisitaMedica medicalVisit);
        public Task DeleteMedicalVisitEventAsync(int eventId);

        // ------------------------------------------- General Gets ----------------------------------------------------------

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

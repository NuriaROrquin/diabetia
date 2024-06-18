using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Utilities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Models;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Entities;
using Diabetia.Infrastructure.EF;
//using System.Data.Entity;

namespace Diabetia.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private diabetiaContext _context;

        public EventRepository(diabetiaContext context)
        {
            _context = context;
        }

        // -------------------------------------------------------- ⇊ Physical Activity Event ⇊ -------------------------------------------------------
        public async Task AddPhysicalActivityEventAsync(int patientId, EventoActividadFisica physicalActivity)
        {
           
            bool IsDone = physicalActivity.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = physicalActivity.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = physicalActivity.IdCargaEventoNavigation.FechaEvento,
                NotaLibre = physicalActivity.IdCargaEventoNavigation.NotaLibre,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            physicalActivity.IdCargaEventoNavigation = newEvent;
            _context.EventoActividadFisicas.Add(physicalActivity);
            await _context.SaveChangesAsync();
        }

        public async Task EditPhysicalActivityEventAsync(EventoActividadFisica physicalActivity)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == physicalActivity.IdCargaEventoNavigation.Id);
            @event.FechaEvento = physicalActivity.IdCargaEventoNavigation.FechaEvento;
            @event.FueRealizado = physicalActivity.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            @event.NotaLibre = physicalActivity.IdCargaEventoNavigation.NotaLibre;

            var physicalEvent = await _context.EventoActividadFisicas.FirstOrDefaultAsync(pe => pe.IdCargaEvento == physicalActivity.IdCargaEventoNavigation.Id);
            if (physicalEvent == null)
            {
                throw new PhysicalEventNotMatchException();
            }

            physicalEvent.IdActividadFisica = physicalActivity.IdActividadFisica;
            physicalEvent.Duracion = physicalActivity.Duracion;

            _context.CargaEventos.Update(@event);
            _context.EventoActividadFisicas.Update(physicalEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhysicalActivityEventAsync(int IdEvent)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == IdEvent);
            var PhysicalEvent = await _context.EventoActividadFisicas.FirstOrDefaultAsync(eaf => eaf.IdCargaEvento == @event.Id);

            _context.EventoActividadFisicas.Remove(PhysicalEvent);
            _context.CargaEventos.Remove(@event);

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⇊ Glucose Event ⇊ -------------------------------------------------------
        public async Task AddGlucoseEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            // 1- Guardar el evento
            bool IsDone = EventDate <= DateTime.Now ? true : false;
            var NewEvent = new CargaEvento
            {
                IdPaciente = patient.Id,
                IdTipoEvento = IdKindEvent,
                FechaActual = DateTime.Now,
                FechaEvento = EventDate,
                NotaLibre = FreeNote,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(NewEvent);
            await _context.SaveChangesAsync();

            var lastInsertedIdEvent = await _context.CargaEventos.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            var devicePatientId = await _context.DispositivoPacientes
                .Where(x => x.IdPaciente == patient.Id)
                .Select(x => x.IdDispositivo)
                .FirstOrDefaultAsync();

            // 2- Guardar evento Glucosa            
            var NewGlucoseEvent = new EventoGlucosa
            {
                IdCargaEvento = lastInsertedIdEvent.Id,
                Glucemia = Glucose,
                IdDispositivoPaciente = devicePatientId,
                IdEventoComida = IdFoodEvent,
                MedicionPostComida = PostFoodMedition,
            };

            _context.EventoGlucosas.Add(NewGlucoseEvent);
            await _context.SaveChangesAsync();

        }

        public async Task EditGlucoseEvent(int EventId, string Email, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null) { throw new UserEventNotFoundException(); }
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            if (patient == null) { throw new PatientNotFoundException(); }
            var EventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == EventId);
            if (EventLoad == null) { throw new EventNotFoundException(); }
            if (EventLoad.IdPaciente != patient.Id) { throw new EventNotRelatedWithPatientException(); }
            var GlucoseEvent = await _context.EventoGlucosas.FirstOrDefaultAsync(eg => eg.IdCargaEvento == EventLoad.Id);
            if (GlucoseEvent == null) { throw new GlucoseEventNotMatchException("No se encontró la carga de glucosa relacionada."); }

            var devicePatientId = await _context.DispositivoPacientes
                .Where(x => x.IdPaciente == patient.Id)
                .Select(x => x.IdDispositivo)
                .FirstOrDefaultAsync();

            // 1- Modificar el evento
            bool IsDone = EventDate <= DateTime.Now ? true : false;
            EventLoad.FechaActual = DateTime.Now;
            EventLoad.FechaEvento = EventDate;
            EventLoad.NotaLibre = FreeNote;
            EventLoad.FueRealizado = IsDone;
            EventLoad.EsNotaLibre = false;
            GlucoseEvent.Glucemia = Glucose;
            GlucoseEvent.IdDispositivoPaciente = devicePatientId;
            GlucoseEvent.IdEventoComida = IdFoodEvent;
            GlucoseEvent.MedicionPostComida = PostFoodMedition;

            _context.CargaEventos.Update(EventLoad);
            _context.EventoGlucosas.Update(GlucoseEvent);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteGlucoseEvent(int IdEvent)
        {        
            var EventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == IdEvent);
            if (EventLoad == null) { throw new EventNotFoundException(); }
            var GlucoseEvent = await _context.EventoGlucosas.FirstOrDefaultAsync(eg => eg.IdCargaEvento == EventLoad.Id);
            if (GlucoseEvent == null) { throw new InsulinEventNotMatchException("No se encontró la carga de glucosa relacionada."); }

            // Eliminar el evento de carga
            _context.EventoGlucosas.Remove(GlucoseEvent);
            _context.CargaEventos.Remove(EventLoad);

            // Guardar los cambios en el contexto
            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⇊ Insuline Event ⇊ -------------------------------------------------------
        public async Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            var patientInsulin = await _context.InsulinaPacientes.FirstOrDefaultAsync(u => u.IdPaciente == patient.Id);

            // 1- Guardar el evento
            bool IsDone = EventDate <= DateTime.Now ? true : false;
            var NewEvent = new CargaEvento
            {
                IdPaciente = patient.Id,
                IdTipoEvento = IdKindEvent,
                FechaActual = DateTime.Now,
                FechaEvento = EventDate,
                NotaLibre = FreeNote,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(NewEvent);
            await _context.SaveChangesAsync();

            var lastInsertedIdEvent = await _context.CargaEventos.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            // 2- Guardar evento Glucosa            
            var NewInsulinEvent = new EventoInsulina
            {
                IdCargaEvento = lastInsertedIdEvent.Id,
                InsulinaInyectada = Insulin,
                IdInsulinaPaciente = patientInsulin.Id,
            };

            _context.EventoInsulinas.Add(NewInsulinEvent);
            await _context.SaveChangesAsync();
        }

        public async Task EditInsulinEvent(int IdEvent, string Email, DateTime EventDate, String FreeNote, int Insulin)
        {
            var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (User == null) { throw new UserEventNotFoundException(); }
            var Patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == User.Id);
            if (Patient == null) { throw new PatientNotFoundException(); }
            var PatientInsulin = await _context.InsulinaPacientes.FirstOrDefaultAsync(ip => ip.IdPaciente == Patient.Id);
            if (PatientInsulin == null) { throw new PatientInsulinRelationNotFoundException(); }
            var EventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == IdEvent);
            if (EventLoad == null) { throw new EventNotFoundException(); }
            if (EventLoad.IdPaciente != Patient.Id) { throw new EventNotRelatedWithPatientException(); }
            var InsulinEvent = await _context.EventoInsulinas.FirstOrDefaultAsync(ei => ei.IdCargaEvento == EventLoad.Id);
            if (InsulinEvent == null) { throw new InsulinEventNotMatchException("No se encontró la carga de insulina relacionada."); }

            // 1- Modificar el evento
            bool IsDone = EventDate <= DateTime.Now ? true : false;
            var FechaActual = DateTime.Now;
            EventLoad.FechaEvento = EventDate;
            EventLoad.NotaLibre = FreeNote;
            EventLoad.FueRealizado = IsDone;
            EventLoad.EsNotaLibre = false;

            InsulinEvent.InsulinaInyectada = Insulin;

            _context.CargaEventos.Update(EventLoad);
            _context.EventoInsulinas.Update(InsulinEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInsulinEvent(int IdEvent)
        {
            var EventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == IdEvent);
            if (EventLoad == null) { throw new EventNotFoundException(); }
            var InsulinEvent = await _context.EventoInsulinas.FirstOrDefaultAsync(ei => ei.IdCargaEvento == EventLoad.Id);
            if (InsulinEvent == null) { throw new InsulinEventNotMatchException("No se encontró la carga de insulina relacionada.");}

            // Eliminar el evento de carga
            _context.EventoInsulinas.Remove(InsulinEvent);
            _context.CargaEventos.Remove(EventLoad);

            // Guardar los cambios en el contexto
            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⇊ Food Manually Event ⇊ -------------------------------------------------------
        public async Task<float> AddFoodManuallyEvent(string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote)
        {
            // Obtener el usuario por email
            var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (User == null) { throw new UserEventNotFoundException(); }

            // Obtener el paciente por id de usuario
            var Patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == User.Id);
            if (Patient == null) { throw new PatientNotFoundException(); }

            // 1- Guardar el evento
            bool IsDone = EventDate <= DateTime.Now ? true : false;
            var newEvent = new CargaEvento
            {
                IdPaciente = Patient.Id,
                IdTipoEvento = IdKindEvent,
                FechaActual = DateTime.Now,
                FechaEvento = EventDate,
                NotaLibre = FreeNote,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            var lastInsertedIdEvent = await _context.CargaEventos
                                            .Where(x => x.IdPaciente == Patient.Id)
                                            .OrderByDescending(e => e.Id)
                                            .FirstOrDefaultAsync();

            decimal totalChFood = 0;
            var ingredientComidumList = new List<IngredienteComidum>();

            foreach (var ingredient in ingredients)
            {
                var searchIngredient = await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == ingredient.IdIngredient);
                if (searchIngredient != null)
                {
                    totalChFood += ingredient.Quantity * searchIngredient.Carbohidratos;
                    var ingredientComidum = new IngredienteComidum
                    {
                        IdIngrediente = ingredient.IdIngredient,
                        IdEventoComida = lastInsertedIdEvent.Id,
                        CantidadIngerida = (int)ingredient.Quantity,
                        Proteinas = ingredient.Quantity * searchIngredient.Proteinas,
                        GrasasTotales = ingredient.Quantity * searchIngredient.GrasasTotales,
                        Carbohidratos = ingredient.Quantity * searchIngredient.Carbohidratos,
                        Sodio = ingredient.Quantity * searchIngredient.Sodio,
                        FibraAlimentaria = ingredient.Quantity * searchIngredient.FibraAlimentaria
                    };
                    ingredientComidumList.Add(ingredientComidum);
                }
            }

            // Insertar el evento de comida en la tabla `evento_comida`
            var newFoodEvent = new EventoComidum
            {
                IdCargaEvento = lastInsertedIdEvent.Id,
                IdTipoCargaComida = (int)FoodChargeTypeEnum.MANUAL,
                Carbohidratos = totalChFood,
            };

            _context.EventoComida.Add(newFoodEvent);
            await _context.SaveChangesAsync();

            foreach (var ingredientComidum in ingredientComidumList)
            {
                ingredientComidum.IdEventoComida = newFoodEvent.Id;
                _context.IngredienteComida.Add(ingredientComidum);
            }

            await _context.SaveChangesAsync();

            return (float)newFoodEvent.Carbohidratos;
        }

        public async Task EditFoodManuallyEvent(int idEvent, string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote)
        {
            // Obtener el usuario por email
            var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (User == null) { throw new UserEventNotFoundException(); }

            // Obtener el paciente por id de usuario
            var Patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == User.Id);
            if (Patient == null) { throw new PatientNotFoundException(); }

            var EventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == idEvent);
            if (EventLoad == null) { throw new EventNotFoundException(); }
            if (EventLoad.IdPaciente != Patient.Id) { throw new EventNotRelatedWithPatientException(); }

            var foodEvent = await _context.EventoComida.FirstOrDefaultAsync(ei => ei.IdCargaEvento == EventLoad.Id);
            if (foodEvent == null) { throw new FoodEventNotMatchException("No se encontró el evento de comida relacionado."); }

            var ingredientFood = await _context.IngredienteComida.FirstOrDefaultAsync(fi => fi.IdEventoComida == foodEvent.Id);
            if (ingredientFood == null) { throw new IngredientFoodRelationNotFoundException(); }

                       
            
            // 1- Guardar el evento
            bool IsDone = EventDate <= DateTime.Now ? true : false;
            EventLoad.IdPaciente = Patient.Id;
            EventLoad.IdTipoEvento = IdKindEvent;
            EventLoad.FechaActual = DateTime.Now;
            EventLoad.FechaEvento = EventDate;
            EventLoad.NotaLibre = FreeNote;
            EventLoad.FueRealizado = IsDone;
            EventLoad.EsNotaLibre = false;

            _context.CargaEventos.Update(EventLoad);
            await _context.SaveChangesAsync();

            decimal totalChFood = 0;
            var ingredientComidumList = new List<IngredienteComidum>();

            foreach (var ingredient in ingredients)
            {
                var searchIngredient = await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == ingredient.IdIngredient);
                if (searchIngredient != null)
                {
                    totalChFood += ingredient.Quantity * searchIngredient.Carbohidratos;
                    var ingredientComidum = new IngredienteComidum
                    {
                        IdIngrediente = ingredient.IdIngredient,
                        IdEventoComida = EventLoad.Id,
                        CantidadIngerida = (int)ingredient.Quantity,
                        Proteinas = ingredient.Quantity * searchIngredient.Proteinas,
                        GrasasTotales = ingredient.Quantity * searchIngredient.GrasasTotales,
                        Carbohidratos = ingredient.Quantity * searchIngredient.Carbohidratos,
                        Sodio = ingredient.Quantity * searchIngredient.Sodio,
                        FibraAlimentaria = ingredient.Quantity * searchIngredient.FibraAlimentaria
                    };
                    ingredientComidumList.Add(ingredientComidum);
                }
            }

            // Insertar el evento de comida en la tabla `evento_comida`

            foodEvent.IdCargaEvento = EventLoad.Id;
            foodEvent.IdTipoCargaComida = (int)FoodChargeTypeEnum.MANUAL;
            foodEvent.Carbohidratos = totalChFood;


            _context.EventoComida.Update(foodEvent);
            await _context.SaveChangesAsync();

            var clearFppdIngredientTable = _context.IngredienteComida
                                            .Where(ic => ic.IdEventoComida == foodEvent.Id)
                                            .ToList();

            _context.IngredienteComida.RemoveRange(clearFppdIngredientTable);

            foreach (var ingredientComidum in ingredientComidumList)
            {
                ingredientComidum.IdEventoComida = foodEvent.Id;
                _context.IngredienteComida.Add(ingredientComidum);
            }

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⇊ Tag Food Event ⇊ -------------------------------------------------------------
        public async Task AddFoodByTagEvent(string email, DateTime eventDate, int carbohydrates)
        {
            var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (User == null) { throw new UserEventNotFoundException(); }

            // Obtener el paciente por id de usuario
            var Patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == User.Id);
            if (Patient == null) { throw new PatientNotFoundException(); }

            // 1- Guardar el evento
            bool IsDone = eventDate <= DateTime.Now ? true : false;

            var newEvent = new CargaEvento
            {
                IdPaciente = Patient.Id,
                IdTipoEvento = (int)TypeEventEnum.COMIDA,
                FechaActual = DateTime.Now,
                FechaEvento = eventDate,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            var lastInsertedIdEvent = await _context.CargaEventos
                                            .Where(x => x.IdPaciente == Patient.Id)
                                            .OrderByDescending(e => e.Id)
                                            .FirstOrDefaultAsync();

            // Insertar el evento de comida en la tabla `evento_comida`
            var newFoodEvent = new EventoComidum
            {
                IdCargaEvento = lastInsertedIdEvent.Id,
                IdTipoCargaComida = (int)FoodChargeTypeEnum.TAG,
                Carbohidratos = carbohydrates,
            };

            _context.EventoComida.Add(newFoodEvent);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteFoodEven(int id)
        {
            var EventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == id);
            if (EventLoad == null) { throw new EventNotFoundException(); }

            var foodEvent = await _context.EventoComida.FirstOrDefaultAsync(ei => ei.IdCargaEvento == EventLoad.Id);
            if (foodEvent == null) { throw new FoodEventNotMatchException("No se encontró el evento de comida relacionado."); }



            if (foodEvent.IdTipoCargaComida == (int)FoodChargeTypeEnum.MANUAL)
            {
                var foodIngredient = await _context.IngredienteComida
                                    .Where(fi => fi.IdEventoComida == foodEvent.Id)
                                    .ToListAsync();
                if (foodIngredient == null) { throw new IngredientFoodRelationNotFoundException(); }

                _context.IngredienteComida.RemoveRange(foodIngredient);

                await _context.SaveChangesAsync();
            }

            _context.EventoComida.Remove(foodEvent);
            _context.CargaEventos.Remove(EventLoad);

            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------- ⇊ Medical Examination Event ⇊ --------------------------------------------------------
        public async Task AddMedicalExaminationEvent(string email, DateTime eventDate, string file, string examinationType, int? idProfessional, string? freeNote)
        {
            var User = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (User == null) { throw new UserEventNotFoundException(); }

            // Obtener el paciente por id de usuario
            var Patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == User.Id);
            if (Patient == null) { throw new PatientNotFoundException(); }

            // 1- Guardar el evento
            bool IsDone = eventDate <= DateTime.Now ? true : false;

            var newEvent = new CargaEvento
            {
                IdPaciente = Patient.Id,
                IdTipoEvento = (int)TypeEventEnum.ESTUDIOS,
                FechaActual = DateTime.Now,
                FechaEvento = eventDate,
                NotaLibre = freeNote,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            var lastInsertedIdEvent = await _context.CargaEventos
                                            .Where(x => x.IdPaciente == Patient.Id)
                                            .OrderByDescending(e => e.Id)
                                            .FirstOrDefaultAsync();

            var newMedicalExamination = new EventoEstudio
            {
                IdCargaEvento = lastInsertedIdEvent.Id,
                Archivo = file,
                TipoEstudio = examinationType,
                IdProfesional = idProfessional,
            };

            _context.EventoEstudios.Add(newMedicalExamination);
            await _context.SaveChangesAsync();
        }

        public async Task<string> DeleteMedicalExaminationEvent(int id)
        {
            var eventLoad = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == id);
            if (eventLoad == null) { throw new EventNotFoundException(); }

            var ExaminationEvent = await _context.EventoEstudios.FirstOrDefaultAsync(es => es.IdCargaEvento == eventLoad.Id);
            if (ExaminationEvent == null) { throw new ExaminationEventNotFoundException(); }

            _context.EventoEstudios.Remove(ExaminationEvent);
            _context.CargaEventos.Remove(eventLoad);
            
            await _context.SaveChangesAsync();

            return ExaminationEvent.Archivo;
        }


        // ------------------------------------------------------ ⇊ Medical Visit Event ⇊ ------------------------------------------------------------
        public async Task AddMedicalVisitEventAsync(EventoVisitaMedica medicalVisit)
        {
            bool IsDone = medicalVisit.IdCargaEventoNavigation.FechaEvento.Date <= DateTime.Now.Date;
            var newEvent = new CargaEvento
            {
                IdPaciente = medicalVisit.IdCargaEventoNavigation.IdPaciente,
                IdTipoEvento = medicalVisit.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = medicalVisit.IdCargaEventoNavigation.FechaEvento,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            var newMedicalVisitEvent = new EventoVisitaMedica
            {
                IdCargaEvento = medicalVisit.IdCargaEventoNavigation.Id,
                IdProfesional = medicalVisit.IdProfesional,
                Descripcion = medicalVisit.Descripcion
            };
            _context.EventoVisitaMedicas.Add(newMedicalVisitEvent);
            await _context.SaveChangesAsync();

            // VER QUE HACEMOS CON RECORDATORIO
            //if (Recordatory && RecordatoryDate.HasValue)
            //{
            //    var newRecordatory = new Recordatorio
            //    {
            //        IdTipoEvento = KindEventId,
            //        FechaInicio = DateOnly.FromDateTime(RecordatoryDate.Value),
            //        HorarioActividad = TimeOnly.FromDateTime(RecordatoryDate.Value),
            //    };
            //    _context.Recordatorios.Add(newRecordatory);
            //    await _context.SaveChangesAsync();
            //    int IdRecordatory = newRecordatory.Id;

            //    var newRecordatoryEvent = new RecordatorioEvento
            //    {
            //        IdCargaEvento = IdLoadEvent,
            //        IdRecordatorio = IdRecordatory,
            //        IdDiaSemana = 1,
            //        FechaHoraRecordatorio = RecordatoryDate.Value
            //    };
            //    _context.RecordatorioEventos.Add(newRecordatoryEvent);
            //    await _context.SaveChangesAsync();
        }

        public async Task EditMedicalVisitEventAsync(EventoVisitaMedica medicalVisit)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == medicalVisit.IdCargaEventoNavigation.Id);
            @event.FechaEvento = medicalVisit.IdCargaEventoNavigation.FechaEvento;
            @event.FueRealizado = medicalVisit.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;

            var medicalVisitEvent = await _context.EventoVisitaMedicas.FirstOrDefaultAsync(vm => vm.IdCargaEvento == medicalVisit.Id);
            if (medicalVisitEvent == null)
            {
                throw new EventNotMatchException();
            }

            medicalVisitEvent.IdProfesional = medicalVisit.IdProfesional;
            medicalVisitEvent.Descripcion = medicalVisit.Descripcion;
            _context.CargaEventos.Update(@event);
            _context.EventoVisitaMedicas.Update(medicalVisitEvent);

            // VER COMO SE HACE LO DE RECORDATORIOS
            //if (Recordatory && RecordatoryDate.HasValue)
            //{
            //    var recordatoryEvent = await _context.RecordatorioEventos.FirstOrDefaultAsync(re => re.IdCargaEvento == EventId);
            //    if (recordatoryEvent == null)
            //    {
            //        throw new RecordatoryNotMatchException();
            //    }
            //    recordatoryEvent.FechaHoraRecordatorio = RecordatoryDate.Value;

            //    var recordatory = await _context.Recordatorios.FirstOrDefaultAsync(r => r.Id == recordatoryEvent.IdRecordatorio);
            //    if (recordatory == null)
            //    {
            //        throw new RecordatoryNotMatchException();
            //    }
            //    recordatory.FechaInicio = DateOnly.FromDateTime(RecordatoryDate.Value);
            //    recordatory.HorarioActividad = TimeOnly.FromDateTime(RecordatoryDate.Value);
            //    _context.RecordatorioEventos.Update(recordatoryEvent);
            //    _context.Recordatorios.Update(recordatory);
            //}
            //else
            //{
            //    var recordatoryEvent = await _context.RecordatorioEventos.FirstOrDefaultAsync(re => re.IdCargaEvento == EventId);
            //    if (recordatoryEvent != null)
            //    {
            //        var recordatory = await _context.Recordatorios.FirstOrDefaultAsync(r => r.Id == recordatoryEvent.IdRecordatorio);
            //        _context.RecordatorioEventos.Remove(recordatoryEvent);
            //        _context.Recordatorios.Remove(recordatory);

            //    }
            //}
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalVisitEventAsync(int eventId)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
            var medicalVisitEvent = await _context.EventoActividadFisicas.FirstOrDefaultAsync(eaf => eaf.IdCargaEvento == @event.Id);

            _context.EventoActividadFisicas.Remove(medicalVisitEvent);
            _context.CargaEventos.Remove(@event);

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⇊ General Gets ⇊ -----------------------------------------------------------------
        public async Task<IEnumerable<AdditionalDataIngredient>> GetIngredients()
        {
            var ingredientsDatabase = await _context.Ingredientes.Join(_context.UnidadMedidaIngredientes,
                    i => i.IdUnidadMedidaIngrediente,
                    umi => umi.Id,
                    (i, umi) => new AdditionalDataIngredient
                    {
                        IdIngredient = i.Id,
                        Name = i.Nombre,
                        Unit = new Unit
                        {
                            Id = umi.Id,
                            UnitName = umi.Medida
                        }
                    }).ToListAsync();

            return ingredientsDatabase;
        }

        public async Task<IEnumerable<PhysicalActivityEvent>> GetPhysicalActivity(int patientId, DateTime? date = null)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var physicalActivityEvents = await query
                .Join(_context.TipoEventos,
                    ce => ce.IdTipoEvento,
                    te => te.Id,
                    (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoActividadFisicas,
                    joined => joined.CargaEvento.Id,
                    eaf => eaf.IdCargaEvento,
                    (joined, eaf) => new { joined.CargaEvento, joined.TipoEvento, EventoActividadFisica = eaf })
                .Join(_context.ActividadFisicas,
                    joined => joined.EventoActividadFisica.IdActividadFisica,
                    af => af.Id,
                    (joined, af) => new PhysicalActivityEvent
                    {
                        IdEvent = joined.CargaEvento.Id,
                        IdEventType = joined.TipoEvento.Id,
                        IdPhysicalEducationEvent = joined.EventoActividadFisica.IdActividadFisica,
                        DateEvent = joined.CargaEvento.FechaEvento,
                        Title = joined.TipoEvento.Tipo,
                        Duration = joined.EventoActividadFisica.Duracion
                    })
                .ToListAsync();

            return physicalActivityEvents;
        }

        public async Task<IEnumerable<FoodEvent>> GetFoods(int patientId, DateTime? date = null)
        {
            IQueryable<CargaEvento> query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var foodEvents = await query
                .Join(_context.TipoEventos,
                    ce => ce.IdTipoEvento,
                    te => te.Id,
                    (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoComida,
                    joined => joined.CargaEvento.Id,
                    ec => ec.IdCargaEvento,
                    (joined, ec) => new { joined.CargaEvento, joined.TipoEvento, EventoComida = ec })
                .Select(joined =>
                    joined.EventoComida.IdTipoCargaComida == 1 ?
                    _context.IngredienteComida
                        .Where(ic => ic.IdEventoComida == joined.EventoComida.Id)
                        .Join(_context.Ingredientes,
                              ic => ic.IdIngrediente,
                              i => i.Id,
                              (ic, i) => new FoodEvent
                              {
                                  IdEvent = joined.CargaEvento.Id,
                                  IdEventType = joined.TipoEvento.Id,
                                  DateEvent = joined.CargaEvento.FechaEvento,
                                  Title = joined.TipoEvento.Tipo,
                                  IngredientName = i.Nombre,
                              })
                        .FirstOrDefault() :
                    new FoodEvent
                    {
                        IdEvent = joined.CargaEvento.Id,
                        IdEventType = joined.TipoEvento.Id,
                        DateEvent = joined.CargaEvento.FechaEvento,
                        Title = joined.TipoEvento.Tipo,
                        IngredientName = null,
                    })
                .ToListAsync();

            return foodEvents;
        }

        public async Task<IEnumerable<ExamEvent>> GetExams(int patientId, DateTime? date = null)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var examEvents = await query
                .Join(_context.TipoEventos,
                        ce => ce.IdTipoEvento,
                        te => te.Id,
                        (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoEstudios,
                        joined => joined.CargaEvento.Id,
                        ee => ee.IdCargaEvento,
                        (joined, ee) => new ExamEvent
                        {
                            IdEvent = joined.CargaEvento.Id,
                            IdEventType = joined.TipoEvento.Id,
                            DateEvent = joined.CargaEvento.FechaEvento,
                            Title = ee.TipoEstudio ?? joined.TipoEvento.Tipo
                        })
                .ToListAsync();

            return examEvents;
        }

        public async Task<IEnumerable<GlucoseEvent>> GetGlycemia(int patientId, DateTime? date = null)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var glucoseEvents = await query
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoGlucosas,
                      joined => joined.CargaEvento.Id,
                      eg => eg.IdCargaEvento,
                      (joined, eg) => new GlucoseEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.TipoEvento.Tipo,
                          GlucoseLevel = eg.Glucemia
                      })
                .ToListAsync();

            return glucoseEvents;
        }

        public async Task<IEnumerable<InsulinEvent>> GetInsulin(int patientId, DateTime? date = null)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var insulinEvents = await query
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoInsulinas,
                      joined => joined.CargaEvento.Id,
                      ei => ei.IdCargaEvento,
                      (joined, ei) => new { joined.CargaEvento, joined.TipoEvento, EventoInsulina = ei })
                .Join(_context.InsulinaPacientes,
                      joined => joined.EventoInsulina.IdInsulinaPaciente,
                      ip => ip.Id,
                      (joined, ip) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, InsulinaPaciente = ip })
                .Join(_context.TipoInsulinas,
                      joined => joined.InsulinaPaciente.IdTipoInsulina,
                      ti => ti.Id,
                      (joined, ti) => new InsulinEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.TipoEvento.Tipo,
                          InsulinType = ti.Nombre,
                          Dosage = joined.EventoInsulina.InsulinaInyectada
                      })
                .ToListAsync();

            return insulinEvents;
        }

        public async Task<IEnumerable<HealthEvent>> GetHealth(int patientId, DateTime? date = null)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var healthEvents = await query
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoSaluds,
                      joined => joined.CargaEvento.Id,
                      es => es.IdCargaEvento,
                      (joined, es) => new { joined.CargaEvento, joined.TipoEvento, EventoSalud = es })
                .Join(_context.Enfermedads,
                      joined => joined.EventoSalud.IdEnfermedad,
                      e => e.Id,
                      (joined, e) => new HealthEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = e.Nombre
                      })
                .ToListAsync();

            return healthEvents;
        }

        public async Task<IEnumerable<MedicalVisitEvent>> GetMedicalVisit(int patientId, DateTime? date = null)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId);

            if (date.HasValue)
            {
                query = query.Where(ce => ce.FechaEvento.Date == date.Value.Date);
            }

            var medicalVisitEvents = await query
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoVisitaMedicas,
                      joined => joined.CargaEvento.Id,
                      evm => evm.IdCargaEvento,
                      (joined, evm) => new MedicalVisitEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.TipoEvento.Tipo,
                          Description = evm.Descripcion,
                      })
                .ToListAsync();

            return medicalVisitEvents;
        }

        // ------------------------------------------------------------------------------------------------------------
        public async Task<CargaEvento> GetEventByIdAsync(int eventId)
        {
            var cargaEvento = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
            return cargaEvento;
        }

        public async Task<TypeEventEnum> GetEventType(int idEvent)
        {
            var typeId = await _context.CargaEventos
                .Where(ce => ce.Id == idEvent)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => te.Id)
                .FirstOrDefaultAsync();

            if (Enum.IsDefined(typeof(TypeEventEnum), typeId))
            {
                return (TypeEventEnum)typeId;
            }
            else
            {
                throw new InvalidOperationException("No existe el tipo de evento");
            }
        }

        public async Task<GlucoseEvent> GetGlucoseEventById(int id)
        {
            var glucoseEvent = await _context.CargaEventos
                .Where(ce => ce.Id == id)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoGlucosas,
                      joined => joined.CargaEvento.Id,
                      eg => eg.IdCargaEvento,
                      (joined, eg) => new GlucoseEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.TipoEvento.Tipo,
                          GlucoseLevel = eg.Glucemia,
                          IdDevice = eg.IdDispositivoPaciente,
                          FreeNote = joined.CargaEvento.NotaLibre

                      })
                .FirstOrDefaultAsync();

            return glucoseEvent;
        }

        public async Task<InsulinEvent> GetInsulinEventById(int id)
        {
            var query = _context.CargaEventos
                .Where(ce => ce.Id == id);

            var insulinEvent = await query
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoInsulinas,
                      joined => joined.CargaEvento.Id,
                      ei => ei.IdCargaEvento,
                      (joined, ei) => new { joined.CargaEvento, joined.TipoEvento, EventoInsulina = ei })
                .Join(_context.InsulinaPacientes,
                      joined => joined.EventoInsulina.IdInsulinaPaciente,
                      ip => ip.Id,
                      (joined, ip) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, InsulinaPaciente = ip })
                .Join(_context.TipoInsulinas,
                      joined => joined.InsulinaPaciente.IdTipoInsulina,
                      ti => ti.Id,
                      (joined, ti) => new InsulinEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.TipoEvento.Tipo,
                          InsulinType = ti.Nombre,
                          Dosage = joined.EventoInsulina.InsulinaInyectada,
                          FreeNote = joined.CargaEvento.NotaLibre
                      })
                .FirstOrDefaultAsync();

            return insulinEvent;
        }

        public async Task<FoodEvent> GetFoodEventById(int id)
        {
            var foodEvent = await _context.CargaEventos
                .Where(ce => ce.Id == id)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoComida,
                      joined => joined.CargaEvento.Id,
                      ec => ec.IdCargaEvento,
                      (joined, ec) => new { joined.CargaEvento, joined.TipoEvento, EventoComida = ec })
                .Select(joined =>
                    joined.EventoComida.IdTipoCargaComida == 1 ?
                    _context.IngredienteComida
                        .Where(ic => ic.IdEventoComida == joined.EventoComida.Id)
                        .Join(_context.Ingredientes,
                              ic => ic.IdIngrediente,
                              i => i.Id,
                              (ic, i) => new FoodEvent
                              {
                                  IdEvent = joined.CargaEvento.Id,
                                  IdEventType = joined.TipoEvento.Id,
                                  DateEvent = joined.CargaEvento.FechaEvento,
                                  Title = joined.TipoEvento.Tipo,
                                  IngredientName = i.Nombre,
                              })
                        .FirstOrDefault() :
                    new FoodEvent
                    {
                        IdEvent = joined.CargaEvento.Id,
                        IdEventType = joined.TipoEvento.Id,
                        DateEvent = joined.CargaEvento.FechaEvento,
                        Title = joined.TipoEvento.Tipo,
                        IngredientName = null,
                    })
                .FirstOrDefaultAsync();

            return foodEvent;
        }

        public async Task<PhysicalActivityEvent> GetPhysicalActivityById(int id)
        {
            var physicalActivityEvent = await _context.CargaEventos
                .Where(ce => ce.Id == id)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoActividadFisicas,
                      joined => joined.CargaEvento.Id,
                      eaf => eaf.IdCargaEvento,
                      (joined, eaf) => new { joined.CargaEvento, joined.TipoEvento, EventoActividadFisica = eaf })
                .Join(_context.ActividadFisicas,
                      joined => joined.EventoActividadFisica.IdActividadFisica,
                      af => af.Id,
                      (joined, af) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoActividadFisica, ActividadFisica = af })
                .Join(_context.ActividadFisicas,
                      joined => joined.EventoActividadFisica.IdActividadFisica,
                      af => af.Id,
                      (joined, af) => new PhysicalActivityEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          IdPhysicalEducationEvent = joined.EventoActividadFisica.IdActividadFisica,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.ActividadFisica.Nombre,
                          Duration = joined.EventoActividadFisica.Duracion
                      })
                .FirstOrDefaultAsync();

            return physicalActivityEvent;
        }

        public async Task<MedicalVisitEvent> GetMedicalVisitEventById(int id)
        {
            var medicalVisitEvent = await _context.CargaEventos
                .Where(ce => ce.Id == id)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoVisitaMedicas,
                      joined => joined.CargaEvento.Id,
                      evm => evm.IdCargaEvento,
                      (joined, evm) => new MedicalVisitEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = joined.TipoEvento.Tipo,
                          Description = evm.Descripcion,
                      })
                .FirstOrDefaultAsync();

            return medicalVisitEvent;
        }

        public async Task<HealthEvent> GetHealthEventById(int id)
        {
            var healthEvent = await _context.CargaEventos
                .Where(ce => ce.Id == id)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoSaluds,
                      joined => joined.CargaEvento.Id,
                      es => es.IdCargaEvento,
                      (joined, es) => new { joined.CargaEvento, joined.TipoEvento, EventoSalud = es })
                .Join(_context.Enfermedads,
                      joined => joined.EventoSalud.IdEnfermedad,
                      e => e.Id,
                      (joined, e) => new HealthEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = e.Nombre
                      })
                .FirstOrDefaultAsync();

            return healthEvent;
        }

        public async Task<ExamEvent> GetExamEventById(int id)
        {
            var examEvent = await _context.CargaEventos
                .Where(ce => ce.Id == id)
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoEstudios,
                      joined => joined.CargaEvento.Id,
                      ee => ee.IdCargaEvento,
                      (joined, ee) => new ExamEvent
                      {
                          IdEvent = joined.CargaEvento.Id,
                          IdEventType = joined.TipoEvento.Id,
                          DateEvent = joined.CargaEvento.FechaEvento,
                          Title = ee.TipoEstudio ?? joined.TipoEvento.Tipo
                      })
                .FirstOrDefaultAsync();

            return examEvent;
        }

        public Task<ExamEvent> GetFreeNoteEventById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckPatientEvent(string email, CargaEvento eventToValidate)
        {
            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuarioNavigation.Email == email);
            if (patient.Id != eventToValidate.IdPaciente)
            {
                return false;
            }
            return true;
        }

    }
}

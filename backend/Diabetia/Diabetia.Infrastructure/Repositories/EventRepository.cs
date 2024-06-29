using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Utilities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Models;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Entities;
using Diabetia.Infrastructure.EF;
using Amazon.S3.Model;


namespace Diabetia.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private diabetiaContext _context;

        public EventRepository(diabetiaContext context)
        {
            _context = context;
        }

        // -------------------------------------------------------- ⬇⬇ Physical Activity Event ⬇⬇ -------------------------------------------------------
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

        // -------------------------------------------------------- ⬇⬇ Glucose Event ⬇⬇ -------------------------------------------------------
        public async Task AddGlucoseEventAsync(int patientId, EventoGlucosa glucose)
        {
            bool IsDone = glucose.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = glucose.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = glucose.IdCargaEventoNavigation.FechaEvento,
                NotaLibre = glucose.IdCargaEventoNavigation.NotaLibre,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            glucose.IdCargaEventoNavigation = newEvent;
            _context.EventoGlucosas.Add(glucose);
            await _context.SaveChangesAsync();
        }

        public async Task EditGlucoseEventAsync(EventoGlucosa glucose)
        {
            var loadedEvent = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == glucose.IdCargaEventoNavigation.Id);
            loadedEvent.FechaEvento = glucose.IdCargaEventoNavigation.FechaEvento;
            loadedEvent.FueRealizado = glucose.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            loadedEvent.NotaLibre = glucose.IdCargaEventoNavigation.NotaLibre;

            var glucoseEvent = await _context.EventoGlucosas.FirstOrDefaultAsync(pe => pe.IdCargaEvento == glucose.IdCargaEventoNavigation.Id);
            if (glucoseEvent == null)
            {
                throw new GlucoseEventNotMatchException();
            }

            glucoseEvent.Glucemia = glucose.Glucemia;
            glucoseEvent.IdDispositivoPaciente = glucose.IdDispositivoPaciente;
            glucoseEvent.IdEventoComida = glucose.IdEventoComida;
            glucoseEvent.MedicionPostComida = glucose.MedicionPostComida;

            _context.CargaEventos.Update(loadedEvent);
            _context.EventoGlucosas.Update(glucoseEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGlucoseEventAsync(int IdEvent)
        {
            var loadedEvent = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == IdEvent);
            var glucoseEvent = await _context.EventoGlucosas.FirstOrDefaultAsync(eg => eg.IdCargaEvento == loadedEvent.Id);

            _context.EventoGlucosas.Remove(glucoseEvent);
            _context.CargaEventos.Remove(loadedEvent);

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⬇⬇ Insuline Event ⬇⬇ -------------------------------------------------------
        public async Task AddInsulinEventAsync(int patientId, EventoInsulina insulin)
        {
            bool IsDone = insulin.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = insulin.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = insulin.IdCargaEventoNavigation.FechaEvento,
                NotaLibre = insulin.IdCargaEventoNavigation.NotaLibre,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            insulin.IdCargaEventoNavigation = newEvent;
            _context.EventoInsulinas.Add(insulin);
            await _context.SaveChangesAsync();
        }

        public async Task EditInsulinEventAsync(EventoInsulina insulin)
        {
            var loadedEvent = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == insulin.IdCargaEventoNavigation.Id);
            loadedEvent.FechaEvento = insulin.IdCargaEventoNavigation.FechaEvento;
            loadedEvent.FueRealizado = insulin.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            loadedEvent.NotaLibre = insulin.IdCargaEventoNavigation.NotaLibre;

            var insulinEvent = await _context.EventoInsulinas.FirstOrDefaultAsync(pe => pe.IdCargaEvento == insulin.IdCargaEventoNavigation.Id);
            if (insulinEvent == null)
            {
                throw new InsulinEventNotMatchException();
            }

            insulinEvent.InsulinaInyectada = insulin.InsulinaInyectada;

            _context.CargaEventos.Update(loadedEvent);
            _context.EventoInsulinas.Update(insulinEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInsulinEventAsync(int idEvent)
        {
            var loadedEvent = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == idEvent);
            var insulinEvent = await _context.EventoInsulinas.FirstOrDefaultAsync(ei => ei.IdCargaEvento == loadedEvent.Id);

            _context.EventoInsulinas.Remove(insulinEvent);
            _context.CargaEventos.Remove(loadedEvent);

            // Guardar los cambios en el contexto
            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⬇⬇ Food Manually Event ⬇⬇ -------------------------------------------------------
        public async Task<float> AddFoodEventAsync(int patientId, EventoComidum foodEvent)
        {

            bool IsDone = foodEvent.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = foodEvent.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = foodEvent.IdCargaEventoNavigation.FechaEvento,
                NotaLibre = foodEvent.IdCargaEventoNavigation.NotaLibre,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            foreach (var ingredient in foodEvent.IngredienteComida)
            {
                var searchIngredient = await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == ingredient.IdIngrediente);

                if (searchIngredient != null)
                {
                    ingredient.Carbohidratos = ingredient.CantidadIngerida * searchIngredient.Carbohidratos;
                    ingredient.GrasasTotales = ingredient.CantidadIngerida * searchIngredient.GrasasTotales;
                    ingredient.Proteinas = ingredient.CantidadIngerida * searchIngredient.Proteinas;
                    ingredient.Sodio = ingredient.CantidadIngerida * searchIngredient.Sodio;
                    ingredient.FibraAlimentaria = ingredient.CantidadIngerida * searchIngredient.FibraAlimentaria;


                    foodEvent.Carbohidratos += (decimal)ingredient.Carbohidratos;
                    foodEvent.GrasasTotales += (decimal)ingredient.GrasasTotales;
                    foodEvent.Proteinas += (decimal)ingredient.Proteinas;
                    foodEvent.Sodio += ingredient.Sodio ?? 0;
                    foodEvent.FibraAlimentaria += ingredient.FibraAlimentaria ?? 0;
                }
            }

            foodEvent.IdCargaEventoNavigation = newEvent;
            _context.EventoComida.Add(foodEvent);
            await _context.SaveChangesAsync();

            return (float)foodEvent.Carbohidratos;
        }

        public async Task<float> EditFoodEventAsync(EventoComidum foodManually)
        {
            var loadedEvent = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == foodManually.IdCargaEventoNavigation.Id);
            loadedEvent.FechaEvento = foodManually.IdCargaEventoNavigation.FechaEvento;
            loadedEvent.FueRealizado = foodManually.IdCargaEventoNavigation.FechaEvento <= DateTime.Now;
            loadedEvent.NotaLibre = foodManually.IdCargaEventoNavigation.NotaLibre;

            var foodEvent = await _context.EventoComida.Include(e => e.IngredienteComida)
                                                       .FirstOrDefaultAsync(pe => pe.IdCargaEvento == foodManually.IdCargaEventoNavigation.Id);
            if (foodEvent == null)
            {
                throw new FoodEventNotMatchException();
            }

            foodEvent.Carbohidratos = 0;
            foodEvent.GrasasTotales = 0;
            foodEvent.Proteinas = 0;
            foodEvent.Sodio = 0;
            foodEvent.FibraAlimentaria = 0;

            foreach (var ingredient in foodManually.IngredienteComida)
            {
                var searchIngredient = await _context.Ingredientes.FirstOrDefaultAsync(i => i.Id == ingredient.IdIngrediente);

                if (searchIngredient != null)
                {
                    decimal newCarbohidratos = ingredient.CantidadIngerida * searchIngredient.Carbohidratos;
                    decimal newGrasasTotales = ingredient.CantidadIngerida * searchIngredient.GrasasTotales;
                    decimal newProteinas = ingredient.CantidadIngerida * searchIngredient.Proteinas;
                    decimal newSodio = ingredient.CantidadIngerida * (ingredient.Sodio ?? 0);
                    decimal newFibraAlimentaria = ingredient.CantidadIngerida * (ingredient.FibraAlimentaria ?? 0);

                    var existingIngredient = foodEvent.IngredienteComida.FirstOrDefault(ic => ic.IdIngrediente == ingredient.IdIngrediente);
                    if (existingIngredient != null)
                    {
                        existingIngredient.CantidadIngerida = ingredient.CantidadIngerida;
                        existingIngredient.Carbohidratos = newCarbohidratos;
                        existingIngredient.GrasasTotales = newGrasasTotales;
                        existingIngredient.Proteinas = newProteinas;
                        existingIngredient.Sodio = newSodio;
                        existingIngredient.FibraAlimentaria = newFibraAlimentaria;

                        foodEvent.Carbohidratos += newCarbohidratos;
                        foodEvent.GrasasTotales += newGrasasTotales;
                        foodEvent.Proteinas += newProteinas;
                        foodEvent.Sodio += newSodio;
                        foodEvent.FibraAlimentaria += newFibraAlimentaria;
                    }
                    else
                    {
                        foodEvent.IngredienteComida.Add(new IngredienteComidum
                        {
                            IdIngrediente = ingredient.IdIngrediente,
                            CantidadIngerida = ingredient.CantidadIngerida,
                            Carbohidratos = newCarbohidratos,
                            GrasasTotales = newGrasasTotales,
                            Proteinas = newProteinas,
                            Sodio = newSodio,
                            FibraAlimentaria = newFibraAlimentaria,
                        });

                        foodEvent.Carbohidratos += newCarbohidratos;
                        foodEvent.GrasasTotales += newGrasasTotales;
                        foodEvent.Proteinas += newProteinas;
                        foodEvent.Sodio += newSodio;
                        foodEvent.FibraAlimentaria += newFibraAlimentaria;
                    }
                }
            }

            var currentIngredients = foodManually.IngredienteComida.Select(ic => ic.IdIngrediente).ToList();
            var ingredientsToDelete = foodEvent.IngredienteComida.Where(ic => !currentIngredients.Contains(ic.IdIngrediente)).ToList();
            foreach (var ingredientToDelete in ingredientsToDelete)
            {
                _context.IngredienteComida.Remove(ingredientToDelete);
            }

            _context.EventoComida.Update(foodEvent);
            await _context.SaveChangesAsync();

            return (float)foodEvent.Carbohidratos;
        }

        public async Task DeleteFoodEventAsync(int idEvent)
        {
            var loadedEvent = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == idEvent);
            var foodEvent = await _context.EventoComida.FirstOrDefaultAsync(ec => ec.IdCargaEvento == loadedEvent.Id);
            var currentIngredients = foodEvent.IngredienteComida.ToList();

            _context.IngredienteComida.RemoveRange(currentIngredients);
            _context.EventoComida.Remove(foodEvent);
            _context.CargaEventos.Remove(loadedEvent);

            // Guardar los cambios en el contexto
            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⬇⬇ Tag Food Event ⬇⬇ -------------------------------------------------------------
        public async Task AddFoodByTagEvent(int patientId, DateTime eventDate, int carbohydrates)
        {
            bool IsDone = eventDate <= DateTime.Now ? true : false;

            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = (int)TypeEventEnum.COMIDA,
                FechaActual = DateTime.Now,
                FechaEvento = eventDate,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            var lastInsertedIdEvent = await _context.CargaEventos
                                            .Where(x => x.IdPaciente == patientId)
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

        // --------------------------------------------------- ⬇⬇ Medical Examination Event ⬇⬇ --------------------------------------------------------
        public async Task AddMedicalExaminationEventAsync(int patientId, EventoEstudio medicalExamination, string fileSavedId)
        {
            bool IsDone = medicalExamination.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = medicalExamination.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = medicalExamination.IdCargaEventoNavigation.FechaEvento,
                NotaLibre = medicalExamination.IdCargaEventoNavigation.NotaLibre,
                FueRealizado = IsDone,
                EsNotaLibre = false,
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            medicalExamination.IdCargaEventoNavigation = newEvent;
            medicalExamination.Archivo = fileSavedId;

            _context.EventoEstudios.Add(medicalExamination);
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


        // ------------------------------------------------------ ⬇⬇ Medical Visit Event ⬇⬇ ------------------------------------------------------------
        public async Task AddMedicalVisitEventAsync(int patientId, EventoVisitaMedica medicalVisit)
        {
            bool IsDone = medicalVisit.IdCargaEventoNavigation.FechaEvento.Date <= DateTime.Now.Date;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = medicalVisit.IdCargaEventoNavigation.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = medicalVisit.IdCargaEventoNavigation.FechaEvento,
                FueRealizado = IsDone,
                EsNotaLibre = false,
                NotaLibre = medicalVisit.IdCargaEventoNavigation.NotaLibre
            };

            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();

            medicalVisit.IdCargaEventoNavigation = newEvent;
            _context.EventoVisitaMedicas.Add(medicalVisit);
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
            @event.FueRealizado = medicalVisit.IdCargaEventoNavigation.FechaEvento <= DateTime.Now ? true : false;
            @event.FechaEvento = medicalVisit.IdCargaEventoNavigation.FechaEvento;
            @event.NotaLibre = medicalVisit.IdCargaEventoNavigation.NotaLibre;

            var medicalVisitEvent = await _context.EventoVisitaMedicas.FirstOrDefaultAsync(vm => vm.IdCargaEvento == medicalVisit.IdCargaEventoNavigation.Id);
            if (medicalVisitEvent == null)
            {
                throw new EventNotMatchException();
            }

            medicalVisitEvent.IdProfesional = medicalVisit.IdProfesional;
            medicalVisitEvent.Descripcion = medicalVisit.Descripcion;
            _context.CargaEventos.Update(@event);
            _context.EventoVisitaMedicas.Update(medicalVisitEvent);
            await _context.SaveChangesAsync();
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

        }

        public async Task DeleteMedicalVisitEventAsync(int eventId)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
            var medicalVisitEvent = await _context.EventoVisitaMedicas.FirstOrDefaultAsync(eaf => eaf.IdCargaEvento == @event.Id);

            _context.EventoVisitaMedicas.Remove(medicalVisitEvent);
            _context.CargaEventos.Remove(@event);

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⬇⬇ Free Note Event ⬇⬇ -------------------------------------------------------
        public async Task AddFreeNoteEventAsync(int patientId, CargaEvento freeNoteEvent)
        {
            bool IsDone = freeNoteEvent.FechaEvento.Date <= DateTime.Now.Date;
            var newEvent = new CargaEvento
            {
                IdPaciente = patientId,
                IdTipoEvento = freeNoteEvent.IdTipoEvento,
                FechaActual = DateTime.Now,
                FechaEvento = freeNoteEvent.FechaEvento,
                FueRealizado = IsDone,
                EsNotaLibre = true,
                NotaLibre = freeNoteEvent.NotaLibre
            };
            _context.CargaEventos.Add(newEvent);
            await _context.SaveChangesAsync();
        }

        public async Task EditFreeNoteEventAsync(CargaEvento freeNoteEvent)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == freeNoteEvent.Id);
            @event.FechaEvento = freeNoteEvent.FechaEvento;
            @event.FueRealizado = freeNoteEvent.FechaEvento <= DateTime.Now ? true : false;
            @event.NotaLibre = freeNoteEvent.NotaLibre;

            _context.CargaEventos.Update(@event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFreeNoteEventAsync(int eventId)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
            _context.CargaEventos.Remove(@event);

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------- ⬇⬇ General Gets ⬇⬇ -----------------------------------------------------------------
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
                        Duration = joined.EventoActividadFisica.Duracion,
                        TypeActivity = joined.EventoActividadFisica.IdActividadFisicaNavigation.Nombre,
                        //Duration = joined.EventoActividadFisica.Duracion,
                        FreeNote = joined.CargaEvento.NotaLibre
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

            var cargaEventosQuery = await query
                .Join(_context.TipoEventos,
                      ce => ce.IdTipoEvento,
                      te => te.Id,
                      (ce, te) => new { CargaEvento = ce, TipoEvento = te })
                .Join(_context.EventoInsulinas,
                      joined => joined.CargaEvento.Id,
                      ei => ei.IdCargaEvento,
                      (joined, ei) => new { joined.CargaEvento, joined.TipoEvento, EventoInsulina = ei })
                .ToListAsync();

            var insulinEvents = cargaEventosQuery
                .GroupJoin(_context.InsulinaPacientes,
                      joined => joined.EventoInsulina.IdInsulinaPaciente,
                      ip => ip.Id,
                      (joined, ipgroup) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, InsulinaPacientes = ipgroup.DefaultIfEmpty() })
                .SelectMany(joined => joined.InsulinaPacientes.DefaultIfEmpty(),
                            (joined, ip) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, InsulinaPaciente = ip })
                .GroupJoin(_context.TipoInsulinas,
                      joined => joined.InsulinaPaciente != null ? joined.InsulinaPaciente.IdTipoInsulina : (int?)null,
                      ti => ti.Id,
                      (joined, tigroup) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, joined.InsulinaPaciente, TipoInsulinas = tigroup.DefaultIfEmpty() })
                .SelectMany(joined => joined.TipoInsulinas.DefaultIfEmpty(),
                            (joined, ti) => new InsulinEvent
                            {
                                IdEvent = joined.CargaEvento.Id,
                                IdEventType = joined.TipoEvento.Id,
                                DateEvent = joined.CargaEvento.FechaEvento,
                                Title = joined.TipoEvento.Tipo,
                                InsulinType = ti != null ? ti.Nombre : null,
                                Dosage = joined.EventoInsulina.InsulinaInyectada,
                            })
                .ToList();

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
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
            return @event;
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
                .GroupJoin(_context.InsulinaPacientes,
                      joined => joined.EventoInsulina.IdInsulinaPaciente,
                      ip => ip.Id,
                      (joined, ipgroup) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, InsulinaPacientes = ipgroup })
                .SelectMany(joined => joined.InsulinaPacientes.DefaultIfEmpty(),
                            (joined, ip) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, InsulinaPaciente = ip })
                .GroupJoin(_context.TipoInsulinas,
                      joined => joined.InsulinaPaciente.IdTipoInsulina,
                      ti => ti.Id,
                      (joined, tigroup) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoInsulina, joined.InsulinaPaciente, TipoInsulinas = tigroup })
                .SelectMany(joined => joined.TipoInsulinas.DefaultIfEmpty(),
                            (joined, ti) => new InsulinEvent
                            {
                                IdEvent = joined.CargaEvento.Id,
                                IdEventType = joined.TipoEvento.Id,
                                DateEvent = joined.CargaEvento.FechaEvento,
                                Title = joined.TipoEvento.Tipo,
                                InsulinType = ti != null ? ti.Nombre : null,
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

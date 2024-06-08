using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;
using Diabetia.Domain.Entities.Events;
using Diabetia.Application.Exceptions;

namespace Diabetia.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private diabetiaContext _context;

        public EventRepository(diabetiaContext context)
        {
            _context = context;
        }

        public async Task AddPhysicalActivityEventAsync(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int IdPhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                throw new UserNotFoundOnDBException();
            }
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
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
            int IdLoadEvent = NewEvent.Id;

            TimeSpan difference = FinishTime - IniciateTime;
            double totalMinutes = difference.TotalMinutes;
            int eventDuration = (int)Math.Ceiling(totalMinutes);
            var NewPhysicalEvent = new EventoActividadFisica
            {
                IdCargaEvento = IdLoadEvent,
                IdActividadFisica = IdPhysicalActivity,
                Duracion = eventDuration,
            };

            _context.EventoActividadFisicas.Add(NewPhysicalEvent);
            await _context.SaveChangesAsync();
        }

        public async Task EditPhysicalActivityEventAsync(string Email, int EventId, DateTime EventDate, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime, string FreeNote)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == EventId);
            if (@event == null)
            {
                throw new EventNotFoundException();
            }
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                throw new UserEventNotFoundException();
            }
            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
            if (@event.IdPaciente != patient.Id)
            {
                throw new EventNotRelatedWithPatientException();
            }

            @event.FechaEvento = EventDate;
            @event.FueRealizado = EventDate <= DateTime.Now ? true : false;
            @event.NotaLibre = FreeNote;

            var physicalEvent = await _context.EventoActividadFisicas.FirstOrDefaultAsync(pe => pe.IdCargaEvento == EventId);
            if (physicalEvent == null)
            {
                throw new PhysicalEventNotMatchException();
            }

            TimeSpan difference = FinishTime - IniciateTime;
            double totalMinutes = difference.TotalMinutes;
            int eventDuration = (int)Math.Ceiling(totalMinutes);

            physicalEvent.IdActividadFisica = PhysicalActivity;
            physicalEvent.Duracion = eventDuration;

            _context.CargaEventos.Update(@event);
            _context.EventoActividadFisicas.Update(physicalEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhysicalActivityEventAsync(string Email, int EventId)
        {
            var @event = await _context.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == EventId);
            if (@event == null)
            {
                throw new EventNotFoundException();
            }
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                throw new UserEventNotFoundException();
            }
            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == @event.IdPaciente);
            if (patient == null)
            {
                throw new EventNotRelatedWithPatientException(); ;
            }
            if (user.Id != patient.IdUsuario)
            {
                throw new MismatchUserPatientException();
            }
            var physicalEvent = await _context.EventoActividadFisicas.FirstOrDefaultAsync(eaf => eaf.IdCargaEvento == EventId);
            if (physicalEvent == null)
            {
                throw new PhysicalEventNotMatchException();
            }

            _context.EventoActividadFisicas.Remove(physicalEvent);
            _context.CargaEventos.Remove(@event);
            await _context.SaveChangesAsync();
        }

        public async Task AddGlucoseEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdDevicePacient, int? IdFoodEvent, bool? PostFoodMedition)
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

            // 2- Guardar evento Glucosa            
            var NewGlucoseEvent = new EventoGlucosa
            {
                IdCargaEvento = lastInsertedIdEvent.Id,
                Glucemia = Glucose,
                IdDispositivoPaciente = IdDevicePacient,
                IdEventoComida = IdFoodEvent,
                MedicionPostComida = PostFoodMedition,
            };

            _context.EventoGlucosas.Add(NewGlucoseEvent);
            await _context.SaveChangesAsync();

        }

        public async Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            var patientInsulin = await _context.InsulinaPacientes.FirstOrDefaultAsync(ip => ip.IdPaciente == patient.Id);

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
                        IdEvent = joined.EventoActividadFisica.Id,
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
            var query = _context.CargaEventos
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
               .Join(_context.IngredienteComida,
                   joined => joined.EventoComida.Id,
                   ic => ic.IdEventoComida,
                   (joined, ic) => new { joined.CargaEvento, joined.TipoEvento, joined.EventoComida, IngredienteComida = ic })
               .Join(_context.Ingredientes,
                   joined => joined.IngredienteComida.IdIngrediente,
                   i => i.Id,
                   (joined, i) => new FoodEvent
                   {
                       IdEvent = joined.CargaEvento.Id,
                       IdEventType = joined.TipoEvento.Id,
                       DateEvent = joined.CargaEvento.FechaEvento,
                       Title = joined.TipoEvento.Tipo,
                       IngredientName = i.Nombre,
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

    }
}

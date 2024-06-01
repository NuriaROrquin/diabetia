using Amazon.CognitoIdentityProvider.Model;
using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;

namespace Diabetia.Infrastructure.Repositories
{

    public class GlucoseEventRepository : IGlucoseEventRepository
    {
        private diabetiaContext _context;

        public GlucoseEventRepository(diabetiaContext context)
        {
            _context = context;
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
    }
}

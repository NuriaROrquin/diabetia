﻿using Diabetia.API;
using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Diabetia.Infrastructure.Repositories
{
    public class HomeRepository : IHomeRepository
    {

        private diabetiaContext _context;

        public HomeRepository(diabetiaContext context)
        {
            _context = context;
        }


        public async Task GetPhysicalActivity(int idUser, int idEvento)
        {
            var evento = await _context.CargaEventos.FirstOrDefaultAsync(e => e.IdPaciente == idUser);
            var activity = await _context.EventoActividadFisicas.FirstOrDefaultAsync(e => e.IdCargaEvento == evento.Id);
            if (activity != null)
            {
                Console.WriteLine(activity);
            }
        }

        /*
        public async Task CompleteUserInfo(string name, string email, string gender, string lastname, int weight, string phone)
        {

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                user.NombreCompleto = String.Concat(name, " ", lastname);
                user.Genero = gender;
                user.Telefono = phone;
            }
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
        }

        public async Task UpdateUserInfo(int typeDiabetes, bool useInsuline, string typeInsuline, string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            if (pac == null)
            {
                var pac_new = new Paciente
                {
                    IdUsuario = user.Id,
                    IdTipoDiabetes = typeDiabetes,
                    UsaInsulina = useInsuline,
                    IdSensibilidadInsulina = 1,
            };
                _context.Pacientes.Add(pac_new);
            }
            else {
                pac.IdUsuario = user.Id;
                pac.IdTipoDiabetes = typeDiabetes;
                pac.UsaInsulina = useInsuline;
                pac.IdSensibilidadInsulina = 1;
                _context.Pacientes.Update(pac);
            }
            await _context.SaveChangesAsync();
        }*/
    }
}

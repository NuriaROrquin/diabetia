using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;
using Diabetia.Infraestructure.EF;

namespace Diabetia.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private diabetiaContext _context;

        public AuthRepository(diabetiaContext context)
        {
            this._context = context;
        }

        public async Task <string> GetUsernameByEmailAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user.Username;
            }
            return "";
        }

        public async Task<string> GetUserHashAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                string hashCode = user.Hash;
                return hashCode;
            }
            return "";
        }

        public async Task <bool> GetUserStateAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                if (user.EstaActivo.HasValue)
                {
                    return user.EstaActivo.Value;
                }
                else
                {
                    throw new InvalidOperationException("El usuario no tiene estado asignado.");
                }
            }
            throw new NotImplementedException();
        }

        public async Task SaveUserHashAsync(string username, string email, string hash)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Hash = hash; //TODO: exception ya estoy registrado.
            }
            else
            {
                user = new Usuario
                {
                    Email = email,
                    Username = username,
                    NombreCompleto = username,
                    Hash = hash
                };
                _context.Usuarios.Add(user);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveUserUsernameAsync(string email, string username)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Username = username.ToLower();
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el email {email}.");
            }
            
        }

        public async Task SetUserStateActiveAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.EstaActivo = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el email {email}.");
            }

        }

        public async Task<bool> CheckUsernameOnDatabaseAsync(string username)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username.ToLower());
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task ResetUserAttemptsAsync(string username)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username.ToLower());
            if (user != null)
            {
                user.IntentosFallidos = 0;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el nombre {username}.");
            }
        }

        public async Task<bool> CheckEmailOnDatabaseAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}

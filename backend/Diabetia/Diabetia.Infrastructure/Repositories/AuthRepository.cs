using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Application.Exceptions;

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
            var newUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (newUser != null)
            {
                string hashCode = newUser.Hash;
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

        public async Task SaveUserHashAsync(Usuario user, string hash)
        {
            var newUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (newUser != null)
            {
                newUser.Hash = hash; //TODO: exception ya estoy registrado.
            }
            else
            {
                newUser = new Usuario
                {
                    Email = user.Email,
                    Username = user.Username,
                    NombreCompleto = user.Username,
                    Hash = hash
                };
                _context.Usuarios.Add(newUser);
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveUserUsernameAsync(Usuario user)
        {
            var newUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (newUser != null)
            {
                newUser.Username = user.Username.ToLower();
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró un usuario con el email {user.Email}.");
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

        public async Task CheckEmailOnDatabaseAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new EmailAlreadyExistsException();
            }
        }
    }
}

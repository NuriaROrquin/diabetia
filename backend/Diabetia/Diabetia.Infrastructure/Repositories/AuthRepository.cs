using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;
using System.Data.Entity.Core;

namespace Diabetia.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private diabetiaContext _context;

        public AuthRepository(diabetiaContext context)
        {
            this._context = context;
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

        public async Task SaveUserHashAsync(string username, string email, string hash)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Hash = hash;
            }
            else
            {
                user = new Usuario
                {
                    Email = email,
                    Username = username,
                    Hash = hash
                };
                _context.Usuarios.Add(user);
            }

            await _context.SaveChangesAsync();
        }

    }
}

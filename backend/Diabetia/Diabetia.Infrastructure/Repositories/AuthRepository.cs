using Diabetia.API;
using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Diabetia.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private diabetiaContext _context;

        public AuthRepository(diabetiaContext context)
        {
            this._context = context;
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
                    NombreCompleto = username,
                    Hash = hash
                };
                _context.Usuarios.Add(user);
            }

            await _context.SaveChangesAsync();
        }
    }
}

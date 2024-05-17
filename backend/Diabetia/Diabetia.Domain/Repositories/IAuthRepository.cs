using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Repositories
{
    public interface IAuthRepository
    {
        public Task SaveUserHashAsync(string username, string email, string hashCode);
    }
}

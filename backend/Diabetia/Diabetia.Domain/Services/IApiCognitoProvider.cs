using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Services
{
    public interface IApiCognitoProvider
    {

        public Task<string> RegisterUserAsync(string username, string password, string email);

        public Task<bool> ConfirmEmailVerificationAsync(string username, string hashCode, string confirmationCode);

        public Task<string> LoginUserAsync(string username, string password);


    }
}

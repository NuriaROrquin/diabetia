using Diabetia.API.DTO;

namespace Diabetia.API
{
    public class AuthRegisterRequest : AuthUserRequest
    {
        public string Email {  get; set; }
        public string Password { get; set; }
   
    }
}
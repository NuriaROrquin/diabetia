using Diabetia.API.DTO;

namespace Diabetia.API
{
    public class RegisterRequest : UserRequest
    {
        public string Email {  get; set; }
        public string Password { get; set; }
   
    }
}
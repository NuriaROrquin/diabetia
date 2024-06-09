namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthRegisterRequest : AuthUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
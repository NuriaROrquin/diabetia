namespace Diabetia.API.DTO
{
    public class AuthConfirmPasswordRecoverRequest : AuthUserRequest
    {
        public string ConfirmationCode { get; set; }
        public string Password { get; set; }
    }
}

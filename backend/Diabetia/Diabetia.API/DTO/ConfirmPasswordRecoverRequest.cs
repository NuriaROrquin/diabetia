namespace Diabetia.API.DTO
{
    public class ConfirmPasswordRecoverRequest : UserRequest
    {
        public string ConfirmationCode { get; set; }
        public string Password { get; set; }
    }
}

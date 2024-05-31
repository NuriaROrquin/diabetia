namespace Diabetia.API.DTO
{
    public class ConfirmEmailRequest : UserRequest
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
    }
}

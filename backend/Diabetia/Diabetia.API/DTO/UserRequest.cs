namespace Diabetia.API.DTO
{
    public class UserRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }
        public string? confirmationCode { get; set; }
        public string username { get; set; }
    }
}

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthLoginResponse
    {
        public string Token { get; set; }
        public int? StepCompleted { get; set; }
    }
}

namespace Diabetia.Domain.Entities
{
    public class User
    {
        public string Token { get; set; }
        public bool InformationCompleted { get; set; }
        public string Email { get; set; }
        public string? Name{ get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public double? Weight { get; set; }
        public DateOnly? BirthDate { get; set; }

    }
}

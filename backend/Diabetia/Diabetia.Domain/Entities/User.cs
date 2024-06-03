using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Patient : User
    {
        public int? TypeDiabetes { get; set; }
        public bool? UseInsuline { get; set; }
        public int? TypeInsuline { get; set; }
        public int? Frequency { get; set; }

    }
}

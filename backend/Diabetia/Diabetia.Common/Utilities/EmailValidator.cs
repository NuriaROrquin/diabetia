using System.Text.RegularExpressions;
using Diabetia.Interfaces;

namespace Diabetia.Common.Utilities
{
    public class EmailValidator : IEmailValidator
    {
        public bool IsValidEmail(string email) // Cambia a un método de instancia
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
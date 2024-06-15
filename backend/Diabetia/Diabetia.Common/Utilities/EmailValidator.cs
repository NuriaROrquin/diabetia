using System.Text.RegularExpressions;
using Diabetia.Common.Exceptions;
using Diabetia.Interfaces;

namespace Diabetia.Common.Utilities
{
    public class EmailValidator : IEmailValidator
    {
        public void IsValidEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException();
            }
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (! Regex.IsMatch(email, pattern))
            {
                throw new InvalidEmailException();
            }
        }
    }
}
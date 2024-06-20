using System.Text.RegularExpressions;
using Diabetia.Interfaces;

namespace Diabetia.Domain.Utilities.Validations
{
    public class InputValidator : IInputValidator
    {
        private readonly string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private readonly string textPattern = @"^[a-zA-Z\s]+$";

        public bool IsEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return Regex.IsMatch(input, emailPattern);
        }

        public bool IsText(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return Regex.IsMatch(input, textPattern);
        }
    }
}

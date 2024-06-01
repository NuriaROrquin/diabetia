using System.Text.RegularExpressions;

namespace Diabetia.Common.Utilities
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
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
namespace Diabetia.Interfaces
{
    public interface IInputValidator
    {
        public bool IsEmail(string input);
        public bool IsText(string input);
    }
}
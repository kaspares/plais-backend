namespace Plais.Exceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException() : base("Password change failed.") { }
    }
}

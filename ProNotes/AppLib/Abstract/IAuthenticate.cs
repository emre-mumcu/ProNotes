namespace ProNotes.AppLib.Abstract
{
    public interface IAuthenticate
    {
        public bool AuthenticateUser(string Username, string Password);
    }
}
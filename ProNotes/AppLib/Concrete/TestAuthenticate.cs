using ProNotes.AppLib.Abstract;

namespace ProNotes.AppLib.Concrete
{
    public class TestAuthenticate : IAuthenticate
    {
        public bool AuthenticateUser(string UserId, string Password) => true;
    }
}
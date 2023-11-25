namespace ProNotes.AppLib.Abstract
{
    public interface IAuthorize
    {
        public Task<Microsoft.AspNetCore.Authentication.AuthenticationTicket?> AuthorizeUserAsync(string Username);
    }
}
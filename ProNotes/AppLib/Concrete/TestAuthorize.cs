using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProNotes.AppLib.Abstract;
using System.Security.Claims;

namespace ProNotes.AppLib.Concrete
{
    public class TestAuthorize : IAuthorize
    {
        private ClaimsPrincipal GetPrincipal(string userId) =>
            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, "John"),
                new Claim(ClaimTypes.Surname, "Doe"),
                new Claim(ClaimTypes.Email, "john@doe.com"),
                new Claim(ClaimTypes.Role, "Guest"),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Role, "Administrator"),
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme
                )
            );

        private AuthenticationProperties GetProperties() => new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(AppConstants.Auth_Cookie_ExpireTimeSpan),
            IsPersistent = true,
            IssuedUtc = DateTimeOffset.UtcNow,
            RedirectUri = AppConstants.Auth_Cookie_RedirectUrl
        };

        private Task<AuthenticationTicket?> GetAuthenticationTicketAsync(string userId)
        {
            AuthenticationTicket tiket = new AuthenticationTicket(GetPrincipal(userId), GetProperties(), CookieAuthenticationDefaults.AuthenticationScheme);

            TaskCompletionSource<AuthenticationTicket?> tcs = new TaskCompletionSource<AuthenticationTicket?>();

            Task<AuthenticationTicket?> authTask = tcs.Task;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);

                try
                {
                    tcs.SetResult(tiket);
                }
                catch (Exception ex)
                {
                    tcs.SetResult(null);
                    tcs.SetException(ex);
                }
            });

            return authTask;
        }

        public Task<AuthenticationTicket?> AuthorizeUserAsync(string userId) => GetAuthenticationTicketAsync(userId);

    }
}
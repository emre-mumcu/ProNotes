using System.Security.Claims;

namespace ProNotes.AppLib.MVC.Extensions
{
    public static class ClaimExtension
    {
        public static bool HasClaim(this ClaimsIdentity ci, string ClaimType, string? ClaimValue = null)
        {
            if (ClaimValue is null)
                return ci.Claims.Any(c => c.Type == ClaimType);
            else
                return ci.Claims.Any(c => c.Type == ClaimType && c.Value == ClaimValue);
        }

        public static bool HasRole(this ClaimsIdentity ci, string RoleName)
        {
            return ci.Claims.Where(c => c.Type == ClaimTypes.Role).Any(c => c.Value == RoleName);
        }

        public static Claim? GetClaim(this ClaimsIdentity ci, string ClaimType)
        {
            return ci?.Claims?.Where(c => c.Type == ClaimType)?.FirstOrDefault();
        }

        public static List<Claim>? GetClaims(this ClaimsIdentity ci, string ClaimType)
        {
            return ci?.Claims?.Where(c => c.Type == ClaimType)?.ToList();
        }

        public static List<string>? GetRoles(this ClaimsIdentity ci)
        {
            return ci?.Claims?.Where(c => c.Type == ClaimTypes.Role)?.Select(c => c.Value)?.ToList();
        }
    }
}
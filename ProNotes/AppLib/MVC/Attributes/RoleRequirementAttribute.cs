using Microsoft.AspNetCore.Mvc;
using ProNotes.AppLib.MVC.Filters;

namespace ProNotes.AppLib.MVC.Attributes
{
    /// <summary>
    /// ClaimTypes.Role must be SET with the required Role
    /// </summary>

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RoleRequirementAttribute : TypeFilterAttribute
    {
        public RoleRequirementAttribute(string Role) : base(typeof(RoleRequirementFilter))
        {
            Arguments = new object[] { Role };
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using ProNotes.AppLib.MVC.Extensions;
using System.Security.Claims;

namespace ProNotes.AppLib.MVC.Tools
{
    // Requires:
    // builder.Services.AddHttpContextAccessor();
    // builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

    public class RazorTools
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly IActionContextAccessor? _actionContextAccessor;
        private readonly LinkGenerator _generator;

        public RazorTools(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor, LinkGenerator generator)
        {
            _httpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
            _generator = generator;
        }

        public string GetActionDescriptor()
        {
            return _actionContextAccessor?.ActionContext?.ActionDescriptor.Id ?? string.Empty;
        }

        public string GetControllerName()
        {
            if (_actionContextAccessor?.ActionContext is ControllerContext)
            {
                ControllerContext? controllerContext = _actionContextAccessor?.ActionContext as ControllerContext;
                return controllerContext?.ActionDescriptor.ControllerName ?? string.Empty;
            }

            return string.Empty;
        }

        public string GetActionName()
        {
            if (_actionContextAccessor?.ActionContext is ControllerContext)
            {
                ControllerContext? controllerContext = _actionContextAccessor?.ActionContext as ControllerContext;
                return controllerContext?.ActionDescriptor.ActionName ?? string.Empty;
            }

            return string.Empty;
        }

        public string GetCurrentUri()
        {
            var routeValues = _actionContextAccessor?.ActionContext?.ActionDescriptor.RouteValues;
            var scheme = _httpContextAccessor?.HttpContext?.Request.Scheme;
            HostString host = _httpContextAccessor?.HttpContext?.Request.Host ?? new HostString();

            return _generator.GetUriByAction(
                        action: GetActionName(),
                        controller: GetControllerName(),
                        values: routeValues,
                        scheme: scheme,
                        host: host)
                    ?? string.Empty;
        }

        public ISession? GetCurrentSession()
        {
            return _httpContextAccessor?.HttpContext?.Session;
        }

        public ClaimsIdentity? GetCurrentUserIdentity()
        {
            return _httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;
        }

        public IEnumerable<Claim>? GetCurrentUserClaims()
        {
            return (_httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity)?.Claims;
        }

        public T? GetSessionKey<T>(string keyName)
        {
            string? value = _httpContextAccessor?.HttpContext?.Session.GetString(keyName);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public void SetSessionKey<T>(string key, T value)
        {
            _httpContextAccessor?.HttpContext?.Session.SetKey(key, value);
        }
    }
}

/*
*  In razor:
*  ---------
* 	var controller = ViewContext.RouteData.Values["Controller"]?.ToString()?.ToLower(new System.Globalization.CultureInfo("en-us"));
* 	var action = ViewContext.RouteData.Values["Action"]?.ToString()?.ToLower(new System.Globalization.CultureInfo("en-us"));
*/
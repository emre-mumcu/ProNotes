using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProNotes.AppLib.Concrete;
using ProNotes.AppLib.MVC.Configuration;
using ProNotes.AppLib.MVC.Tools;

namespace ProNotes.AppLib.Startup
{
    public static class ConfigureServices
    {
        public static WebApplicationBuilder _ConfigureServices(this WebApplicationBuilder web_builder)
        {
            web_builder.Services._ConfigureRequestLocalization();

            web_builder.Services._AddServerSideBlazor();

            web_builder.Services._AddMvc();

            web_builder.Services._AddCookiePolicy();

            web_builder.Services.AddServerSideBlazor();

            web_builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // services.AddHttpContextAccessor();

            web_builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            web_builder.Services.AddHttpClient();

            web_builder.Services.AddTransient<RazorTools>();

            web_builder.Services.AddDataProtection();

            web_builder.Services._AddSession();

            web_builder.Services._AddOptions<DataOptions>("data.json");

            web_builder.Services._AddAuthentication<TestAuthenticate>();

            web_builder.Services._AddAuthorization<TestAuthorize>();

            web_builder.Services._AddCors();

            web_builder.Services._AddResponseCaching();

            web_builder.Services._AddResponseCompression();

            web_builder.Services._AddDataLayer();

            return web_builder;
        }
    }
}
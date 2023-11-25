using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Mvc
    {
        public static IServiceCollection _AddMvc(this IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc(config =>
            {
                config.Filters.Add(new AuthorizeFilter(nameof(AuthorizationPolicyLibrary.userPolicy)));
                config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddSessionStateTempDataProvider() // Use session based TempData instead of cookie based TempData
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
            .AddNewtonsoftJson(options =>  // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                mvcBuilder.AddRazorRuntimeCompilation(); // dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            }

            return services;
        }

        public static IApplicationBuilder _UseMvc(this WebApplication app)
        {
            return app;
        }
    }
}

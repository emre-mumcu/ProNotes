using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class Razor
    {
        public static IServiceCollection _AddService(this IServiceCollection services)
        {
            IMvcBuilder razorBuilder = services.AddRazorPages(options =>
            {
                options.Conventions.ConfigureFilter(new AuthorizeFilter(nameof(AuthorizationPolicyLibrary.userPolicy)));
                options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddSessionStateTempDataProvider()
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
            .AddNewtonsoftJson(options => // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(options => {
            //    options.FileProviders.Append(new DatabaseFileProvider());
            //});

            return services;
        }

        public static IApplicationBuilder _UseService(this WebApplication app)
        {
            return app;
        }
    }
}

/*
 https://www.learnrazorpages.com/razor-pages/routing
 Changing the default Razor Pages root folder

 builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options => {
        options.RootDirectory = "/Content";
    });

builder.Services.AddRazorPages().WithRazorPagesRoot("/Content");

 */

// Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;

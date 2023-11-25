using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class ViewLocation
    {
        public static IServiceCollection _ConfigureViewLocationExpander(this IServiceCollection services)
        {
            services
                .Configure<RazorViewEngineOptions>(options =>
                {
                    options.ViewLocationExpanders.Add(new ViewLocationExpander());
                });

            return services;
        }
    }

    public class ViewLocationExpander : IViewLocationExpander
    {
        // Adds "Partials" folder in default search locations for views etc.

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //{2} is area, {1} is controller,{0} is the action
            string[] locations = new string[] {
            "/ViewPartials/{0}" + RazorViewEngine.ViewExtension,
            "/ViewComponents/{0}" + RazorViewEngine.ViewExtension,
            "/Content/{0}" + RazorViewEngine.ViewExtension,
             "/Content/Layout/{0}" + RazorViewEngine.ViewExtension,
             "/Content/PartialViews/{0}" + RazorViewEngine.ViewExtension,


        };
            return locations.Union(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}
``` csharp

using Microsoft.EntityFrameworkCore;
using mumcu.net.data.Context;
using mumcu.net.tools;

namespace mumcu.net.ui.AppLib.Configuration
{
    public static class AddApplicationServices
    {
        public static WebApplicationBuilder _AddApplicationServices(this WebApplicationBuilder web_builder)
        {
            // Database Context
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile("data.json").Build();
            string cs = DPAPI.Unportect(configurationRoot.GetSection("Database:ConnectionString").Value, "ConnectionString");
            web_builder.Services.AddDbContext<AppDbContextAuditable>(options => options.UseSqlServer(connectionString: cs));

            return web_builder;
        }
    }
}





namespace mumcu.net.ui.AppLib.Configuration
{
    public static class UseApplicationServices
    {
        public async static Task<WebApplication> _UseApplicationServices(this WebApplication app)
        {

            // this is not actual seeding, this is adding data to table onapp start
            // seeding is done in modelbuilder
            // AppDbContextAuditable dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContextAuditable>();
            // await ms.net.data.Concrete.DataSeeder.PopulateData(dbContext);

            await Task.CompletedTask;
            return app;
        }
    }
}







```


@*
    
@inject EgitimPortal.AppLib.Abstract.IRazorTools MyRazorTools
Note: If you use Interface, use servicer register
*@

@*
    The _ViewImports.cshtml file provides a mechanism to include the directives globally for Razor Pages so that we don’t have to add them individually in each and every page. The _ViewImports.cshtml file supports the following directives:

    @addTagHelper
    @tagHelperPrefix
    @removeTagHelper
    @namespace
    @inject
    @model
    @using

    The @addTagHelper, @tagHelperPrefix, and @removeTagHelper directives are basically used to manage of Tag Helpers. The @namespace directive is basically used to specify the namespace that the ViewImports belongs to. With the help of @inject directive, it supports Dependency injection. The @model directive is basically used to specify the Model for your view. The @using directive basically used to include the common namespaces globally so that you don’t have to include the namespaces in each and every view page.

*@
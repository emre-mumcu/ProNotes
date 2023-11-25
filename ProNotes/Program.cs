using Microsoft.Extensions.Configuration;
using ProNotes.AppData.EFCore.Context;
using ProNotes.AppLib.Startup;

try
{
/*
     * Release settings will NOT run in VS. 
     * EnvironmentName and ContentRootPath is set up to run as Windows Service !!!
     * 
     * Publish (release) to C:\Users\emumcu2\ProNotes folder then:
     * 
     *   https://stackoverflow.com/questions/70571849/host-asp-net-6-in-a-windows-service
         Microsoft has changed how services should be created as the fix for this. The --contentRoot argument should be used with the exe.
            sc config MyWebAppServiceTest binPath= "$pwd\WebApplication560.exe --contentRoot $pwd\"
            sc create MyApplicationWindowsService binPath= myapplication.exe
            New-Service -Name {SERVICE NAME} -BinaryPathName "{EXE FILE PATH} --contentRoot {EXE      

         C:\WINDOWS\system32>sc create ProNotesWindowsService binPath=C:\Users\emumcu2\ProNotes\ProNotes.exe
         Windows Service Address: http://localhost:5000/
         C:\WINDOWS\system32>sc delete ProNotesWindowsService
*/

#if DEBUG

    var builder = WebApplication.CreateBuilder(args)._ConfigureServices();

    builder.Configuration.AddJsonFile("data.json", optional: false, reloadOnChange: false);

#else

    var webApplicationOptions = new WebApplicationOptions() {
        EnvironmentName = Environments.Production,
        ContentRootPath = AppContext.BaseDirectory, 
        Args = args, 
        ApplicationName = System.Diagnostics.Process.GetCurrentProcess().ProcessName 
    };
    
    var builder = WebApplication.CreateBuilder(webApplicationOptions)._ConfigureServices();   
    
    builder.Configuration.AddJsonFile("data.json", optional: false, reloadOnChange: false);
    
    // dotnet add package Microsoft.Extensions.Hosting.WindowsServices    
    builder.Host.UseWindowsService();

#endif

    var app = builder.Build()._Configure().Result;

    AppDbContext._Configuration = app.Services.GetRequiredService<IConfiguration>();

    await ProNotes.AppData.EFCore.DataGenerator.GenerateAsync(app.Services);

    app.Run();
}
catch (Exception ex)
{
    Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddMvc(); })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure((ctx, app) =>
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Application error. {ex.Message}");
            });
        });
    }).Build().Run();
}
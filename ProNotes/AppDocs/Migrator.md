``` csharp
namespace Migrator
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
			await Task.FromResult(0);
			//try
			//{
			//	if(!context.ProductBrands.Any())
			//	{
			//		// add data here
			//	}

			//	// add data for other entities
			//}
			//catch (Exception ex)
			//{
			//	var logger = loggerFactory.CreateLogger<AppDbContextSeed>();
			//	logger.LogError(ex, "Error in data seeding.");
			//}
        }
    }
}




using WebZero.Data.EFCore.Context;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    try
//    {
//        var context = services.GetRequiredService<AppDbContext>();
//        await context.Database.MigrateAsync();
//        await AppDbContextSeed.SeedAsync(context, loggerFactory);
//    }
//    catch (Exception ex)
//    {
//        var logger = loggerFactory.CreateLogger<Program>();
//        logger.LogError(ex, "Error in Migration!");
//    }
//}

app.MapGet("/", () => "Migrator is running");

app.Run();


/*

# Tools:
--------
dotnet tool list -g 
dotnet tool install -g dotnet-ef
dotnet tool update -g dotnet-ef

# Data Context Project
----------------------
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# Startup Project / Migrator Project
------------------------------------
dotnet add package Microsoft.EntityFrameworkCore.design

# Right click on solution "WebZero" and select "Open in Terminal":
---------------------------------------------------
dotnet ef migrations add [Name] -p [Project-With-DbContext] -s [Startup-Project] [--context AppDbContext] [-o EFCore/Migrations] [--verbose]
PS C:\Users\emumcu2\Desktop\WebZero> 
> dotnet ef migrations add Mig0 -p WebZero.Data\ -s WebZero.Migrator\ --context AppDbContext
> dotnet ef database update -p WebZero.Data\ -s WebZero.Migrator\ --context AppDbContext

 */




```
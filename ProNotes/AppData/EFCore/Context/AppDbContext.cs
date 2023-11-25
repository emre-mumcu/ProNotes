using Microsoft.EntityFrameworkCore;
using ProNotes.AppData.Entities;
using ProNotes.AppLib.Tools;
using static NuGet.Packaging.PackagingConstants;

// dotnet tool install --global dotnet-ef
// dotnet tool update --global dotnet-ef
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Sqlite

// dotnet ef migrations add InitialCreate -o AppData/Migrations
// dotnet ef database update

// dotnet add package Pomelo.EntityFrameworkCore.MySql

/*
 * Raw SQL queries for unmapped types
 * 
 var summaries =
    await context.Database.SqlQuery<PostSummary>(
            @$"SELECT b.Name AS BlogName, p.Title AS PostTitle, p.PublishedOn
            FROM Posts AS p
            INNER JOIN Blogs AS b ON p.BlogId = b.Id")
        .ToListAsync();
 */

namespace ProNotes.AppData.EFCore.Context
{
    public class AppDbContext : DbContext
    {
        public static IConfiguration? _Configuration { get; set; }

        // TODO: Entity definitions
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Note> Notes => Set<Note>();


        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

            if (!optionsBuilder.IsConfigured)
            {
                // TODO To enable debugging in Migrations, uncomment following:
                // if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();               

                // SQL Server:
                // optionsBuilder.UseSqlServer(DataConfigReader.GetParameter("Database", "ConnectionString", true));

                // SQLite:
                // string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                // optionsBuilder.UseSqlite($"Data Source={System.IO.Path.Join(dbPath, "ProNotes.db")}");


                // DI ile alınmayan contextlerde (new ile elle oluşturulursa) connection string buradan alınacak!!!
                //JsonSerializer serializer = new JsonSerializer();
                //MyObject obj = serializer.Deserialize<DataOptions>(File.ReadAllText(@".\path\to\json\config\file.json");

                if (_Configuration == null || _Configuration.GetSection("Database").GetSection("ConnectionString").Value == null)
                {
                    _Configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("data.json", true)
                        .Build();
                }

                var connectionString = _Configuration.GetSection("Database").GetSection("ConnectionString").Value;

#if DEBUG
				optionsBuilder
	                .UseMySql(connectionString, new MySqlServerVersion(new Version(11, 2)))
	                .LogTo(Console.WriteLine, LogLevel.Information)
	                .EnableSensitiveDataLogging()
	                .EnableDetailedErrors()
                ;
#else
				optionsBuilder
	                .UseMySql(connectionString, new MySqlServerVersion(new Version(11, 2)))
                ;
#endif
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			// TODO To enable debugging in Migrations, uncomment following:
			// if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch(); 

			modelBuilder.UseCollation("latin5_turkish_ci");
			modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

			
		}
    }
}
﻿using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProNotes.AppData.EFCore.Context;
using ProNotes.AppData.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProNotes.AppData.EFCore
{
    public class DataGenerator
    {
        /// <summary>
        /// GenerateAsync populates sample data to specifed tables.
        /// This type of data generation does NOT allow primary keys to be set explicitly. 
        /// Primary key values are auto generated by database.
        /// This needs to be manually called in Program.cs
        /// </summary>
        public static async Task GenerateAsync(IServiceProvider services)
        {
            IWebHostEnvironment environment = services.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment())
            {
                IServiceScope scope = services.CreateScope();

                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                string cs = context.Database.GetConnectionString();

            // context.Database.EnsureCreated() is new EF core method which ensures that the database for the context exists. If it exists, no action is taken. If it does not exist then the database and all its schema are created and also it ensures it is compatible with the model for this context.

            // Note: This method does not use migrations to create the database. In addition, the database that is created cannot later be updated using migrations.If you are targeting a relational database and using migrations, you can use the DbContext.Database.Migrate() method to ensure the database is created and all migrations are applied.

                // bool result = context.Database.EnsureCreated();

                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.MigrateAsync();
                }

                // if (!context.Categories.Any()) InitializeCategoryData(context, environment);
            }
        }

        private static void InitializeCategoryData(AppDbContext context, IWebHostEnvironment environment)
        {
            // sqlite:
            // SELECT * INTO equipments_backup FROM equipments yerine CREATE TABLE equipments_backup AS SELECT * FROM equipments
            // context.Database.ExecuteSqlRaw($"select * into Categories_Yedek_{DateTime.Now.ToString("yyyyyMMdd_HHmmss")} from Categories");
            // context.Database.ExecuteSqlRaw("TRUNCATE TABLE Categories");          

            context.Categories.AddRange(
                new Category[] {
                    new Category() { CategoryText = "Software Development" },
                    new Category() { CategoryText = "Personel" },
                    new Category() { CategoryText = "Projects" },    

                    new Category() { CategoryText = "ASPNET", ParentCategoryId = 1 },
                    new Category() { CategoryText = "BLAZOR", ParentCategoryId = 1 },
                    new Category() { CategoryText = "C#", ParentCategoryId = 1 },
                    new Category() { CategoryText = "CSS", ParentCategoryId = 1 },
                    new Category() { CategoryText = "ENTITY FRAMEWORK", ParentCategoryId = 1 },
                    new Category() { CategoryText = "HTML", ParentCategoryId = 1 },
                    new Category() { CategoryText = "JAVASCRIPT", ParentCategoryId = 1 },
                    new Category() { CategoryText = "MVC", ParentCategoryId = 1 },                    
                    new Category() { CategoryText = "SIGNALR", ParentCategoryId = 1 },
                    new Category() { CategoryText = "SNIPPETS", ParentCategoryId = 1 },

                    new Category() { CategoryText = "Personel Notes", ParentCategoryId = 2 },

                    new Category() { CategoryText = "HUYAP", ParentCategoryId = 3 }
                }                    
            );

            context.SaveChanges();
        }
    }
}

 alter database ProNotes set single_user with rollback immediate
 drop table ProNotes

 
 
 dotnet ef migrations add Mig0 -c AppDbContext -o AppData/Migrations
 dotnet ef database update -c AppDbContext

dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

``` console

# add this package to startup project
> dotnet add package Microsoft.EntityFrameworkCore.Design

# add this package to dbcontext project
> dotnet add package Microsoft.EntityFrameworkCore.SqlServer


```
dotnet list package
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer


dotnet ef migrations add [MigrationName] -c DbContextName -p ProjectNameHavingDbContext -s StartupProjectName -o OutputDir [--no-build --verbose --prefix-output]

dotnet ef database update -c AppDbContextAuditable -p ms.net.data -s ms.net.ui --verbose


1)
dotnet ef migrations add Mig0 -c AppDbContextAuditable -o AppData/Migrations
2)
dotnet ef database update -c AppDbContextAuditable 


After adding
dotnet add package Microsoft.EntityFrameworkCore.Design
package build solution or you will get:
Unable to retrieve project metadata. Ensure it's an SDK-style project. If you're using a custom BaseIntermediateOutputPath or MSBuildProjectExtensionsPath values, Use the --msbuildprojectextensionspath 
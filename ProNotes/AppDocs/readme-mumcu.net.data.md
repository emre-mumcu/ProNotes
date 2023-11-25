# Entity Framework Core

``` bash
# install/update ef tools:
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# install ef core:
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# migrations
dotnet ef migrations add <MigrationName> [-p <ProjectNameHavingDbContext> -s <StartupProjectName> -o PathToMigrations]
```

## Note: Dropping Database
If you have the following error while dropping the databse, alter the database as follows and then drop it:
``` sql
--Msg 3702, Level 16, State 3, Line 1
--Cannot drop database "..." because it is currently in use.
use Master;
go
alter database ... set single_user with rollback immediate
go
drop database ...
go
```

# Entity Framework Core

## EF Core CLI Commands

``` console

# install / update ef tools:

dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# migrations

dotnet ef migrations add <MigrationName> [-p <ProjectNameHavingDbContext> -s <StartupProjectName> -o PathToMigrations]

```

# Package List

``` console

# add this package to startup project
> dotnet add package Microsoft.EntityFrameworkCore.Design

# add this package to dbcontext project
> dotnet add package Microsoft.EntityFrameworkCore.SqlServer

> dotnet add package Newtonsoft.Json
```

# EFCore Code First

``` console
dotnet ef migrations add --help
dotnet ef migrations add [MigrationName] -c DbContextName -p ProjectNameHavingDbContext -s StartupProjectName -o OutputDir [--no-build --verbose --prefix-output]

# 1 run the command in the path (root of solution) where solution file (*.sln) exists
# Migrations folder is created in dbcontext project
ms.net> dotnet ef migrations add InitialCreate -c AppDbContextAuditable -p ms.net.data -s ms.net.ui --verbose --prefix-output

# 2 run the command in the path where solution file (*.sln) exists
ms.net> dotnet ef database update -c AppDbContextAuditable -p ms.net.data -s ms.net.ui --verbose
```

NOTE:
Your startup project 'ms.net.ui' doesn't reference Microsoft.EntityFrameworkCore.Design. This package is required for the Entity Framework Core Tools to work.

## Note: Dropping Database
If you have the following error while dropping the databse, alter the database as follows and then drop it:
``` sql
--Msg 3702, Level 16, State 3, Line 1
--Cannot drop database "..." because it is currently in use.
use Master;
go
alter database ... set single_user with rollback immediate
go
drop database ...
go
```

# Code Snippets

``` csharp
if (_Configuration == null)
{
    _Configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("data.json", true)
        .Build();
}
```
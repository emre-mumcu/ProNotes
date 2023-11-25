https://www.learnentityframeworkcore.com/
https://www.entityframeworktutorial.net/

# EFCore Summary

## Install dotnet ef CLI tools
``` bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

## Install dotnet ef providers

**This package should be installed to the project containing the DbContext**  
``` bash
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

**This package should be installed to the startup project**  
``` bash
PM> Install-Package Microsoft.EntityFrameworkCore.Design
```

*Note: Rebuild the in Visual Studio after installing the Microsoft.EntityFrameworkCore.Design package*

After setting up cli tools and providers, create DbContext and DbSets.

## Migration

**Create migrations and update database:**  
``` bash
dotnet ef migrations add InitialCreate -p <ProjectHavingDbContext> -s <StartupProject> -o AppData/Migrations
dotnet ef database update -p <ProjectHavingDbContext> -s <StartupProject>
dotnet ef database drop -p <ProjectHavingDbContext> -s <StartupProject>
dotnet ef migrations remove -p <ProjectHavingDbContext> -s <StartupProject>

#
# Open dotnetcli (right click project file and select open in terminal)
#
dotnet ef migrations add InitialCreate -o AppData/Migrations
#
dotnet ef database update

```

## Configuration

``` csharp
public class QuestionAnswerConfig : IEntityTypeConfiguration<QuestionAnswer>
{
    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
      builder
        .HasKey(bc => new { bc.QuestionId, bc.AnswerId });// compound PK
      builder
        .HasOne(bc => bc.Question)
        .WithMany(b => b.QuestionAnswers)
        .HasForeignKey(bc => bc.QuestionId);
      builder
        .HasOne(bc => bc.Answer)
        .WithMany(c => c.QuestionAnswers)
        .HasForeignKey(bc => bc.AnswerId);
        builder.Property(x => x.Name).HasColumnName("varchar_name");
    }
}


// Applying all configurations
modelBuilder.ApplyConfiguration(new StudentMapper());

// Appliyin specific configurations:
var mapper = Activator.CreateInstance(mappingType);
mapper.GetType().GetMethod("Configure").Invoke(mapper, new[] { entityBuilder });
```   


# Visual Studio Extensions

Productivity Power Tools 2022
https://marketplace.visualstudio.com/items?itemName=VisualStudioPlatformTeam.ProductivityPowerPack2022

Markdown Editor 2 (64-bit)
https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor2

Markdown Editor (64-bit)
https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor64

CodeMaid VS2022
https://marketplace.visualstudio.com/items?itemName=SteveCadwallader.CodeMaidVS2022

VSColorOutput64
https://marketplace.visualstudio.com/items?itemName=MikeWard-AnnArbor.VSColorOutput64

Visual Studio Theme Pack
https://marketplace.visualstudio.com/items?itemName=idex.vsthemepack

Dummy Text Generator
https://marketplace.visualstudio.com/items?itemName=MadsKristensen.DummyTextGenerator

File Icons
https://marketplace.visualstudio.com/items?itemName=MadsKristensen.FileIcons

Visual Studio Color Theme Designer 2022
https://marketplace.visualstudio.com/items?itemName=idex.colorthemedesigner2022

VS Theme Colors 2022
https://marketplace.visualstudio.com/items?itemName=MadsKristensen.VSThemeColors2022

SQLite and SQL Server Compact Toolbox
https://marketplace.visualstudio.com/items?itemName=ErikEJ.SQLServerCompactSQLiteToolbox

# Visual Studio Configuration

* Tools -> Options -> Fonts and Colors -> Font -> IBM Plex Mono
* Tools -> Options -> Environment -> Preview Features -> Load Projects Faster -> Uncheck
* Tools -> Options -> Text Editor -> Display group -> Highlight current line -> Uncheck
* Tools -> Options -> Environment -> Tabs and Windows -> Preview Tab -> Allow new files to be opened in preview tab -> Uncheck
* Tools -> Options -> Text Editor -> All Languages, in the Settings group, check Word wrap
* Naming generated private fields starting with underscore (_): 
  * Open Tools -> Options -> Text Editor -> C# -> Code Style -> Naming
  * Click Manage Namin Styles & click (+) sign.
  * Naming Style: _fieldName, Required Prefix: _, Capitalization: camel Case Name
  * Click OK to close Manage Naming Styles.
  * Click (+) on Options Window to add a new line to the list.
  * Select Private or Internal Field as Specification, select _fieldName as Required Style abd select Suggestion as Severity. Clik OK to close window.

# Rebind the project to github when the .git folder has been accidentally deleted

https://stackoverflow.com/questions/40387291/restore-deleted-git-folder-from-github
If you accidentally deleted a .git folder in your local working copy, and want to restore it from remote repository (e.g. on GitHub), then the following should work (assuming that you are in the root directory where you want to put .git folder):

``` bash
git init
git remote add origin `<url>`
git fetch
git reset origin/master
```

Here `<url>` is the URL of the remote repository (for instance, git@github.com:git/git.git or https://github.com/git/git.git).

``` bash
git remote add origin  https://github.com/emremumcu/***.git
```

# Changing default SSL port number

IISExpress uses http.sys for its communication and it requires SSL ports to be registered as Administrator.
To avoid running Visual Studio as administrator, IIS Express reserves the port range 44300 - 44399 when it is installed.
As long as you select a port within this range (Properties->launchSettings.json->iisSettings->sslPort) you do not need to 
run IISExpressAdminCmd to register the URL.

# Razor syntax reference for ASP.NET Core

https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-3.1

In Razor Views, implicit expressions cannot contain C# generics, as the characters 
inside the brackets `<>` are interpreted as an HTML tag. 
    
The following code is not valid:

``` csharp    
<p>@GenericMethod<int>()</p>
``` 

Generic method calls must be wrapped in an explicit Razor expression or a Razor code block.

    
Razor expression:
-----------------

Explicit Razor expressions consist of an @ symbol with balanced parenthesis: @()

``` csharp
<p>@(GenericMethod<int>())</p>
``` 

    
Razor code blocks:
------------------

Razor code blocks start with @ and are enclosed by {}. Unlike expressions, C# code inside code blocks isn't rendered.
Code blocks and expressions in a view share the same scope and are defined in the order they are written.

``` csharp
@{
    var str = "This is a string.";
}
``` 


In code blocks, we can declare local functions with markup to serve as templating methods:

``` csharp
@{
    void RenderName(string name)
    {
        <p>Name: <strong>@name</strong></p>
    }

    RenderName("Emre");        
}
``` 
    
Implicit transitions:
---------------------

The default language in a code block is C#, but the Razor Page can transition back to HTML:

``` csharp
@{
    var inCSharp = true;
    <p>Now in HTML, was in C# @inCSharp</p>
}
```

Explicit delimited transition:
------------------------------

To define a subsection of a code block that should render HTML, surround the characters for 
rendering with the Razor `<text>` tag:

``` csharp
@for (var i = 0; i < people.Length; i++)
{
    var person = people[i];
    <text>Name: @person.Name</text>
}
``` 

Use this approach to render HTML that isn't surrounded by an HTML tag. Without an HTML or 
Razor tag, a Razor runtime error occurs.

The `<text>` tag is useful to control whitespace when rendering content:

    * Only the content between the <text> tag is rendered.
    * No whitespace before or after the <text> tag appears in the HTML output.

Explicit line transition:
-------------------------

To render the rest of an entire line as HTML inside a code block, use @: syntax:

``` csharp
@for (var i = 0; i < people.Length; i++)
{
    var person = people[i];
    @:Name: @person.Name
}
``` 

Without the @: in the code, a Razor runtime error is generated.
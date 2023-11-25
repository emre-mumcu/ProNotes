# Project Structure

``` bash
AspNet6>dotnet new sln -n AspNet6
AspNet6>dotnet new web -o AspNet6.Web
AspNet6>dotnet new webapi -o AspNet6.API
AspNet6>dotnet sln add AspNet6.Web\AspNet6.Web.csproj --solution-folder src
AspNet6>dotnet sln add AspNet6.API\AspNet6.API.csproj --solution-folder src
AspNet6>dotnet new classlib -o Documents
AspNet6>dotnet sln add Documents\Documents.csproj --solution-folder res
```


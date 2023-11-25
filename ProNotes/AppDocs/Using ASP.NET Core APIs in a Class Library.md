# Using ASP.NET Core APIs in a Class Library

https://docs.microsoft.com/en-us/dotnet/core/tools/sdk-errors/netsdk1080
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/target-aspnetcore?view=aspnetcore-6.0&tabs=visual-studio

With the release of .NET Core 3.0, many ASP.NET Core assemblies are no longer published to NuGet as packages. Instead, the assemblies are included in the Microsoft.AspNetCore.App shared framework, which is installed with the .NET Core SDK and runtime installers.

As of .NET Core 3.0, projects using the Microsoft.NET.Sdk.Web MSBuild SDK implicitly reference the shared framework. Projects using the Microsoft.NET.Sdk or Microsoft.NET.Sdk.Razor SDK must reference ASP.NET Core to use ASP.NET Core APIs in the shared framework.

To reference ASP.NET Core in your class library, add the following `<FrameworkReference>` element to your project file:

```bash
dotnet new classlib
```

``` html
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <FrameworkReference Include="Microsoft.AspNetCore.App" />
  </ItemGroup>

</Project>
```
# Dotnet Templating

## 1. Using Nuget Pack

Edit *.csproj file and add the following:

``` xml
	<PropertyGroup>
		<PackageType>Template</PackageType>
		<PackageId>mvczero</PackageId>
		<PackageVersion>1.0.0</PackageVersion>
		<TargetFramework>net6.0</TargetFramework>
		<IsPackable>true</IsPackable>
		<Title>Mvc Zero</Title>		
		<Authors>Emre Mumcu</Authors>
		<Company>mumcu.net</Company>
		<Description>This is boilerplate template for ASP.NET MVC Core applications</Description>
		<RepositoryUrl>https://github.com/</RepositoryUrl>
		<PackageLicenseExpression>MIT</PackageLicenseExpression>		
		<PackageTags>template; mvc; start-up</PackageTags>
		<Copyright>Copyright © 2022</Copyright>
		<PackageProjectUrl>https://mumcu.net</PackageProjectUrl>
		<PackageLicenseUrl>https://mumcu.net</PackageLicenseUrl>
		<PackageIconUrl>https://mumcu.net/icon.png</PackageIconUrl>
		<IncludeContentInPack>true</IncludeContentInPack>
		<IncludeBuildOutput>false</IncludeBuildOutput>
		<EnableDefaultContentItems>false</EnableDefaultContentItems>
		<NoDefaultExcludes>true</NoDefaultExcludes>		
	</PropertyGroup>
	
	<ItemGroup>
		<Content Include="**\**" Exclude="**\bin\**;**\obj\**" />
	</ItemGroup>
```

```bash
# uses template.nuspec => https://docs.microsoft.com/en-us/nuget/reference/nuspec
nuget pack 
```

### template.nuspec file

``` xml
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
	<metadata>
		<id>net.mumcu.mvczero</id>
		<version>1.0.0</version>
		<authors>emumcu</authors>
		<description>ASP.NET Core MVC Boilerplate</description>
		<authors>Emre Mumcu</authors>
		<license type="expression">MIT</license>
		<requireLicenseAcceptance>false</requireLicenseAcceptance>		
		<projectUrl>https://github.com/</projectUrl>
    <licenseUrl>http://LICENSE_URL_HERE_OR_DELETE_THIS_LINE</licenseUrl>
    <projectUrl>http://PROJECT_URL_HERE_OR_DELETE_THIS_LINE</projectUrl>
    <iconUrl>http://ICON_URL_HERE_OR_DELETE_THIS_LINE</iconUrl>
		<repository type="git" url="https://github.com/" branch="main"/>
		<tags>web mvc mvczero</tags>
		<packageTypes>
			<packageType name="Template" />
		</packageTypes>
		<copyright>Copyright ©  2022</copyright>
	</metadata>
	<files>
		<file src="**\*.*" exclude="**\bin\**\*.*;**\obj\**\*.*;**\.vs\**\*.*" target="Content" />
	</files>
</package>
```

## 2. Using Dotnet Pack

```bash
# uses template.json in ProjectFolder\.template.config\template.json
dotnet pack 
```

### template.json file (in ProjectFolder\.template.config folder)

``` json
{
  "$schema": "http://json.schemastore.org/template",
  "author": "Emre Mumcu",
  "classifications": [ "web", "mvc", "mvczero" ],     
  "identity": "net.mumcu.mvczero",                    
  "name": "MvcZero",
  "shortName": "mvczero",                             
  "tags": {
    "language": "C#",
    "type": "project"
  },
  "sourceName": "MvcZero",
  "preferNameDirectory": true,
  "defaultName": "MVCZero",
  "description": "ASP.NET Core MVC Boilerplate"
}
```

# Command Reference

```bash
# List Templates
dotnet new --list
dotnet new -l

# Install Package
dotnet new --install mvczero.1.0.0.nupkg 
dotnet new --uninstall mvczero.1.0.0.nupkg
## If, in the same folder with package file
dotnet new --install .

# Uninstall Package
dotnet new -u
dotnet new --uninstall MvcZero
```

# References
https://www.nuget.org/downloads
https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props
https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-project-file-schema-reference?view=vs-2022
https://auth0.com/blog/create-dotnet-project-template/
https://garywoodfine.com/how-to-create-project-templates-in-net-core/
https://code-maze.com/dotnet-project-templates-creation/
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
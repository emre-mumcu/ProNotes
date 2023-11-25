# Package List
``` bash
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package NLog.Web.AspNetCore
dotnet add package Newtonsoft.Json
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package SixLabors.ImageSharp --version 2.0.0
dotnet add package SixLabors.ImageSharp.Drawing --version 1.0.0-beta13


dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package SixLabors.ImageSharp
dotnet add package SixLabors.ImageSharp.Drawing --prerelease

```

# Ready & Load

**$(document).ready**  
It executes when HTML-Document is loaded and DOM is ready, so it will run immediately after the document is loaded, not waiting for any pictures, frames, objects or whatever is not yet loaded.

**$(window).load**  
It executes when complete page is fully loaded, including all frames, objects and images. `.load()` is a shortcut for `.on( "load", handler )`. It binds an event handler to the `load` JavaScript event.

# Handler Methods in Razor Pages
<https://www.learnrazorpages.com/razor-pages/handler-methods>

Handler methods in Razor Pages are methods that are automatically executed as a result of a request. The Razor Pages framework uses a naming convention to select the appropriate handler method to execute. The default convention works by matching the HTTP verb used for the request to the name of the method, which is prefixed with "On": OnGet(), OnPost(), OnPut() etc.

Handler methods also have optional asynchronous equivalents: OnPostAsync(), OnGetAsync() etc. You do not need to add the Async suffix. As far as the Razor Pages framework is concerned, OnGet and OnGetAsync are the same handler. You cannot have both in the same page. If you do, the framework will raise an exception: `InvalidOperationException: Multiple handlers matched.`

Parameters play no part in disambiguating between handlers based on the same verb, despite the fact that the compiler will allow it. Therefore the same exception will be raised even if the OnGet method takes parameters and the OnGetAsync method doesn't.

Handler methods must be public and can return void, Task if asynchronous, or an IActionResult (or Task<IActionResult>).

``` csharp
public class IndexModel : PageModel
{
	public void OnGet() { }

	public async Task OnPost() {
		await Task.CompletedTask;
	}

	public IActionResult OnPut() {
		return Page();
	}

	public async Task<IActionResult> OnDelete() {
		await Task.CompletedTask;
		return Page();
	}
}
``` 

**Note that HTTP is stateless. Any values initialised in the OnGet handler are not available in the OnPost handler.**

## Named Handler Methods

Razor Pages includes a feature called "named handler methods". This feature enables you to specify multiple methods that can be executed for a single verb. You might want to do this if your page features multiple forms, each one responsible for a different outcome, for example.

The name of the method is appended to "OnPost" or "OnGet", depending on whether the handler should be called as a result of a POST or GET request.

The following code shows a collection of named handler methods declared in a code block at the top of a Razor page (although they can also be placed in the PageModel class if you are using one):

``` html
@page 
@{    
	@functions {     
		public void OnGet() { }
		public void OnPost() { }
		public void OnPostAction1() { }
		public void OnPostAction2(int id) { }        
	}
}

<form asp-page-handler="action1" method="post">
	<button class="btn btn-default">Edit</button>
</form>

<form asp-page-handler="action2" method="post">
	<input type="text" name="id" />
	<button class="btn btn-default">Edit</button>
</form>

``` 

As you click each button, the code in the handler associated with the query string value is executed like `https://localhost:port/Page?handler=action1` 

If you prefer not to have query string values representing the handler's name in the URL, you can use routing to add an optional route value for "handler" as part of the route template in the @page directive:

``` csharp
@page "{handler?}"
``` 

The name of the handler is then appended to the URL like `https://localhost:port/Page/action1`

## Parameters in Handler Methods

Handler methods can be designed to accept parameters. The parameter name must match a form field name for the incoming value to be automatically bound to the parameter.

``` html
@{ 
	@functions {  
		public void OnPostView(int id) { }
	}
}

<form asp-page-handler="view" method="post">
	<button class="btn btn-default">View</button>
	<input type="hidden" name="id" value="3" />
</form>
``` 

Alternatively, you can use the form tag helper's asp-route attribute to pass parameter values as part of the URL, either as a query string value or as route data for GET requests:

``` html
<form asp-page-handler="delete" asp-route-id="10" method="post">
	<button class="btn btn-default">Delete</button>
</form>
``` 

You append the name of the parameter to the asp-route attribute (in this case "id") and then provide a value. This will result in the parameter being passed as a query string value. Or you can extend the route template for the page to account for an optional parameter:

``` csharp
@page "{handler?}/{id?}"
``` 

## Handling Multiple Actions For The Same Form

Some forms need to be designed to cater for more than one possible action.

``` html
<form method="post">
	<button asp-page-handler="Register">Register Now</button>
	<button asp-page-handler="RequestInfo">Request Info</button>
</form>
```

## The NonHandler Attribute
There may be occasions where you don't want a public method on a page to be considered as a handler method, despite its name matching the conventions for handler method discovery. You can use the NonHandler attribute to specify that the decorated method is not a page handler method:

``` csharp
[NonHandler]
public void OnGetFoo()
``` 

# Code Snippets 

``` csharp
// Logger logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

			//ms.net.data.Entities.SampleAuditableEntity entity = new ms.net.data.Entities.SampleAuditableEntity();

			//entity.Column1 = 1;
			//entity.Column2 = "Column2";
			//entity.Column3 = new DateTime(3);
			//entity.Column4 = true;            
			//entity.Column5 = "Column6";

			//dbContext.sampleAuditableEntities.Add(entity);
			//dbContext.SaveChanges();


			//var i = dbContext.sampleAuditableEntities.First(e => e.Id == 1);
			//i.Updated = DateTime.Now;
			//dbContext.SaveChanges();

			//var i = dbContext.sampleAuditableEntities.First(e => e.Id == 1);
			//dbContext.Remove(i);
			//dbContext.SaveChanges();

```

@*
https://www.learnrazorpages.com/razor-pages/handler-methods
<form method="post">
	<button asp-page-handler="Register">Register Now</button>
	<button asp-page-handler="RequestInfo">Request Info</button>
</form>
<div class="row">
	<div class="col-lg-1">
		<form asp-page-handler="edit" method="post">
			<button class="btn btn-default">Edit</button>
		</form>
	</div>
	<div class="col-lg-1">
		<form asp-page-handler="delete" method="post">
			<button class="btn btn-default">Delete</button>
		</form>
	</div>
	<div class="col-lg-1">
		<form asp-page-handler="view" method="post">
			<button class="btn btn-default">View</button>
		</form>
	</div>
</div>
<h3 class="clearfix">@Model.Message</h3>
*@
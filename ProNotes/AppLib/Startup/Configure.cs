using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.HttpOverrides;
using ProNotes.AppLib.MVC.Configuration;
using ProNotes.AppLib.MVC.Hubs;

namespace ProNotes.AppLib.Startup
{
    public static class Configure
    {
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
        public async static Task<WebApplication> _Configure(this WebApplication app)
        {
            app._UsePathBase();

            app._UseRequestLocalization();

            if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Error");

            app.UseHsts();

            app.UseHttpsRedirection();

            app._UseCors();

            app._UseResponseCaching();

            app._UseResponseCompression();

            app._UseStaticFiles();

            app._UseCookiePolicy();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStatusCodePagesWithReExecute("/StatusResult", "?statusCode={0}");

            app._UseSession();

            app._UseOptions();

            app.UseRouting();

            app._UseAuthentication();

            app._UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                // endpoints.MapBlazorHub();
                endpoints.MapHub<MainHub>("/mainhub", options =>
                {
                    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
                });
            });

            // app.MapBlazorHub();

            // app.MapFallbackToPage("/_Host");

            //app.Use(async (context, next) => // inline middleware
            //{
            //    // Do work that doesn't write to the Response.    
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.    
            //});

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Service was unable to handle this request: {context.Request.Path}");
            });

            await Task.CompletedTask;

            return app;
        }
    }
}
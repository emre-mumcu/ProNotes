using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ProNotes.AppLib.MVC.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MainHub : Hub
    {
        HubManager<MainHub> _hubManager;

        public MainHub()
        {
            _hubManager = new();
        }

        public override Task OnConnectedAsync()
        {
            var ctx = Context;

            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}

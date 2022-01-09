using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Sandogh.WebSite.EndPoint.Hubs
{
    public class OnlineVisitorHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

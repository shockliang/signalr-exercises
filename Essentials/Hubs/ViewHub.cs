using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Essentials.Hubs
{
    public class ViewHub : Hub
    {
        public static int ViewCount { get; set; } = 0;

        public override async Task OnConnectedAsync()
        {
            ViewCount++;
            await Clients.All.SendAsync("viewCountUpdate", ViewCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ViewCount--;
            await Clients.All.SendAsync("viewCountUpdate", ViewCount);
            await base.OnConnectedAsync();
        }
    }
}
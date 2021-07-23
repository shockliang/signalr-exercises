using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Essentials.Hubs
{
    public class ViewHub : Hub<IHubClient>
    {
        public static int ViewCount { get; set; } = 0;

        public override async Task OnConnectedAsync()
        {
            ViewCount++;
            await Clients.All.ViewCountUpdate(ViewCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ViewCount--;
            await Clients.All.ViewCountUpdate(ViewCount);
            await base.OnConnectedAsync();
        }
    }
}

public interface IHubClient
{
    Task ViewCountUpdate(int viewCount);
}
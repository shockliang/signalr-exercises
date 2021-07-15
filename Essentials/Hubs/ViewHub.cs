using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Essentials.Hubs
{
    public class ViewHub : Hub
    {
        public static int ViewCount { get; set; } = 0;

        public async Task NotifyWatching()
        {
            ViewCount++;
            await Clients.All.SendAsync("viewCountUpdate", ViewCount);
        }

        public Task IncrementServerView()
        {
            ViewCount++;
            return Clients.All.SendAsync("incrementView", ViewCount);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RedisBackplane.Hubs
{
    public class BackgroundColorHub : Hub
    {
        public async Task ChangeBackground(string color)
        {
            await Clients.All.SendAsync("changeBackground", color);
        }
    }
}
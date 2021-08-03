using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Essentials.Hubs
{
    public class TimeHub : Hub
    {
        public async Task GetCurrentTime()
        {
            await Clients.Caller.SendAsync("UpdatedTime", DateTime.UtcNow.ToString("F"));
        }
    }
}
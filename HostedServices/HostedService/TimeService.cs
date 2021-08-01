using System;
using System.Threading;
using System.Threading.Tasks;
using HostedServices.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace HostedServices.HostedService
{
    public class TimeService : IHostedService, IDisposable
    {
        private readonly IHubContext<TimeHub> timeHub;
        private Timer timer;

        public TimeService(IHubContext<TimeHub> timeHub)
        {
            this.timeHub = timeHub;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(Tick, null, 0, 500);
            return Task.CompletedTask;
        }

        private void Tick(object state)
        {
            var currentTime = DateTime.UtcNow.ToString("F");
            timeHub.Clients.All.SendAsync("updateCurrentTime", currentTime);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
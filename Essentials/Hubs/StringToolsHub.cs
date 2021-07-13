using Microsoft.AspNetCore.SignalR;

namespace Essentials.Hubs
{
    public class StringToolsHub : Hub
    {
        public string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
    }
}
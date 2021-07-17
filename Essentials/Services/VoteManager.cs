using System.Collections.Generic;
using System.Threading.Tasks;
using Essentials.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Essentials.Services
{
    public class VoteManager : IVoteManager
    {
        private static Dictionary<string, int> votes;
        private readonly IHubContext<VoteHub> hubContext;

        public VoteManager()
        {
            votes = new Dictionary<string, int>();
            votes.Add("pie", 0);
            votes.Add("bacon", 0);
        }
        
        public VoteManager(IHubContext<VoteHub> hubContext) : this()
        {
            this.hubContext = hubContext;
        }
        
        public async Task CastVote(string voteFor)
        {
            votes[voteFor]++;

            // notify
            await hubContext.Clients.All.SendAsync("updateVotes", votes);
        }

        public Dictionary<string, int> GetCurrentVotes()
        {
            return votes;
        }
    }
}
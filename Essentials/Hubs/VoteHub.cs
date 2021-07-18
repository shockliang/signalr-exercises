using System.Collections.Generic;
using Essentials.Services;
using Microsoft.AspNetCore.SignalR;

namespace Essentials.Hubs
{
    public class VoteHub : Hub
    {
        private readonly IVoteManager voteManager;

        public VoteHub(IVoteManager voteManager)
        {
            this.voteManager = voteManager;
        }

        public Dictionary<string, int> GetCurrentVotes()
        {
            return voteManager.GetCurrentVotes();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Essentials.Services
{
    public interface IVoteManager
    {
        Task CastVote(string voteFor);
        Dictionary<string, int> GetCurrentVotes();
    }
}
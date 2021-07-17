using System.Threading.Tasks;
using Essentials.Services;
using Microsoft.AspNetCore.Mvc;

namespace Essentials.Controllers
{
    public class VoteController : ControllerBase
    {
        private readonly IVoteManager voteManager;

        public VoteController(IVoteManager voteManager)
        {
            this.voteManager = voteManager;
        }
        
        [HttpPost("/vote/pie")]
        public async Task<IActionResult> VotePie()
        {
            // save vote
            await voteManager.CastVote("pie");

            return Ok();
        }

        [HttpPost("/vote/bacon")]
        public async Task<IActionResult> VoteBacon()
        {
            // save vote
            await voteManager.CastVote("bacon");

            return Ok();
        }
    }
}
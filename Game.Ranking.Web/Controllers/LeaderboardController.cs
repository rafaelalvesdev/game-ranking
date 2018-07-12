using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Game.Ranking.Web.Controllers
{
    [ApiController]
    [Route("leaderboard")]
    public class LeaderboardController : BaseController
    {
        [HttpGet]
        public async Task<ServiceResult> Get([FromServices] ILeaderboardService service, [FromQuery] int? numTop = null)
        {
            return await service.GetLeaderboard(numTop);
        }
    }
}
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
        /// <summary>
        /// Recupera o leaderboard (ranking) dos jogadores
        /// </summary>
        /// <param name="top">TOP do Número de jogadores para recuperar do ranking (Max. 1000 / Default 100)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult> Get([FromServices] ILeaderboardService service, [FromQuery] int? top = null)
        {
            return await service.GetLeaderboard(top);
        }
    }
}
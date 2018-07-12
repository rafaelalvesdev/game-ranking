using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Messages;
using Game.Ranking.Services.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Game.Ranking.Web.Controllers
{
    [ApiController]
    [Route("game-results")]
    public class GameResultController : BaseController
    {
        /// <summary>
        /// Insere pontuação referente a partida que um jogador efetuou no jogo
        /// </summary>
        /// <param name="message">Dados de pontuações do jogador</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult> Insert([FromServices]IGameResultService service, SaveGameResultMessage message)
        {
            return await service.Save(message);
        }

        /// <summary>
        /// Realiza a replicação/persistência dos dados que estão em memória 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [Route("replicate")]
        [HttpPost]
        public async Task<ServiceResult> Replicate([FromServices]IGameResultService service)
        {
            return await service.Replicate();
        }
    }
}
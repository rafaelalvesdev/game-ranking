using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Messages;
using Game.Ranking.Services.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Game.Ranking.Web.Controllers
{
    public class GameResultController : BaseController
    {
        [HttpPost]
        public async Task<ServiceResult> Insert([FromServices]IGameResultService service, SaveGameResultMessage message)
        {
            return await service.Save(message);
        }
        
        [Route("replicate")]
        [HttpPost]
        public async Task<ServiceResult> Replicate([FromServices]IGameResultService service)
        {
            return await service.Replicate();
        }
    }
}
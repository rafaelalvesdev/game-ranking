using Game.Ranking.Services;
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
        public async Task<ServiceResult> Post([FromServices]IGameResultService service, SaveGameResultMessage message)
        {
            return await service.Save(message);
        }
    }
}
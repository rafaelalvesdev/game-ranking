using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Messages;
using Game.Ranking.Services.Results;
using System;
using System.Threading.Tasks;

namespace Game.Ranking.Services.Impl
{
    public class GameResultService : IGameResultService
    {
        public async Task<ServiceResult> Save(SaveGameResultMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
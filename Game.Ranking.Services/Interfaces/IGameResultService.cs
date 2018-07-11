using Game.Ranking.Services.Messages;
using Game.Ranking.Services.Results;
using System.Threading.Tasks;

namespace Game.Ranking.Services.Interfaces
{
    public interface IGameResultService
    {
        Task<ServiceResult> Save(SaveGameResultMessage message);
    }
}

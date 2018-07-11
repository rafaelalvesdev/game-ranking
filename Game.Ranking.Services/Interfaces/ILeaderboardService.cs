using Game.Ranking.Services.Results;
using System.Threading.Tasks;

namespace Game.Ranking.Services.Interfaces
{
    public interface ILeaderboardService
    {
        Task<ServiceResult> GetLeaderboard();
    }
}

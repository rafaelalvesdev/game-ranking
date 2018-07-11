using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Model;
using Game.Ranking.Services.Interfaces;

namespace Game.Ranking.Services.Impl
{
    public class GameResultMemoryStorageService
        : AbstractMemoryStorageService<GameResult>, IMemoryStorageService<GameResult>, IGameResultMemoryStorageService
    {
        public GameResultMemoryStorageService(IGameResultRepository repository) : base(repository)
        {
        }
    }
}

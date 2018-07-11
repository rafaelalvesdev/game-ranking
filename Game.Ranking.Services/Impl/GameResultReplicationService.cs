using Game.Ranking.Infrastructure.Replication.Interfaces;
using Game.Ranking.Model;
using Game.Ranking.Services.Interfaces;

namespace Game.Ranking.Services.Impl
{
    public class GameResultReplicationService : AbstractReplicationService<GameResult>, IGameResultReplicationService
    {
        public GameResultReplicationService(IGameResultRepository repository) : base(repository)
        { }
    }
}
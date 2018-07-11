using Game.Ranking.Infrastructure.Replication.Impl;
using Game.Ranking.Infrastructure.Replication.Interfaces;
using Game.Ranking.Model;

namespace Game.Ranking.Infrastructure.Replication.Repositories
{
    public class GameResultRepository : AbstractRepository<GameResult>, IGameResultRepository
    {
        public GameResultRepository(GameRankingElasticClient client) : base(client)
        { }
    }
}
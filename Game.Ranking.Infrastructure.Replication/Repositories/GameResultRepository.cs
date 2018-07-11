using Game.Ranking.Infrastructure.Replication.Impl;
using Game.Ranking.Infrastructure.Replication.Interfaces;

namespace Game.Ranking.Infrastructure.Replication.Repositories
{
    public class GameResultRepository : AbstractRepository, IGameResultRepository
    {
        public GameResultRepository(GameRankingElasticClient client) : base(client)
        { }
    }
}
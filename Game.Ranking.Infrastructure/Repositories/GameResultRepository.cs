using Game.Ranking.Infrastructure.Impl;
using Game.Ranking.Infrastructure.Interfaces;

namespace Game.Ranking.Infrastructure.Repositories
{
    public class GameResultRepository : AbstractRepository, IGameResultRepository
    {
        public GameResultRepository(GameRankingElasticClient client) : base(client)
        { }
    }
}
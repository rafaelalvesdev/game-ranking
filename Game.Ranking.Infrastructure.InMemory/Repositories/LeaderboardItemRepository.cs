using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Model;

namespace Game.Ranking.Infrastructure.InMemory.Repositories
{
    public class LeaderboardItemRepository : AbstractRepository<LeaderboardItem>, ILeaderboardItemRepository
    {
        public LeaderboardItemRepository(InMemoryDbContext dbContext) : base(dbContext)
        {
        }
    }
}

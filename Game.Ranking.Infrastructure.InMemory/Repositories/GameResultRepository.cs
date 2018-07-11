using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Model;
using Microsoft.EntityFrameworkCore;

namespace Game.Ranking.Infrastructure.InMemory.Repositories
{
    public class GameResultRepository : AbstractRepository<GameResult>, IGameResultRepository
    {
        public GameResultRepository(InMemoryDbContext dbContext) : base(dbContext)
        {
        }
    }
}

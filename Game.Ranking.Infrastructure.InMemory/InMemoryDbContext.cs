using Game.Ranking.Model;
using Microsoft.EntityFrameworkCore;

namespace Game.Ranking.Infrastructure.InMemory
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }

        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<LeaderboardItem> Leaderboard { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
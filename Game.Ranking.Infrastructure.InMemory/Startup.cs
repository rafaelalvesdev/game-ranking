using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Infrastructure.InMemory.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Ranking.Infrastructure.InMemory
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<InMemoryDbContext>(provider => {
                const string DatabaseName = "InMemoryDB";
                var options = new DbContextOptionsBuilder<InMemoryDbContext>()
                                .UseInMemoryDatabase(DatabaseName)
                                .Options;
                return new InMemoryDbContext(options);
            });

            services.AddSingleton<IGameResultRepository, GameResultRepository>();
            services.AddSingleton<ILeaderboardItemRepository, LeaderboardItemRepository>();
        }
    }
}

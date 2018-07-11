using Game.Ranking.Infrastructure.Replication.Impl;
using Game.Ranking.Infrastructure.Replication.Interfaces;
using Game.Ranking.Infrastructure.Replication.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Ranking.Infrastructure.Replication
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<GameRankingElasticClient>(provider =>
            {
                var configurator = provider.GetRequiredService<ElasticClientConfigurator>();
                return new GameRankingElasticClient(configurator.ConnectionSettings);
            });

            services.AddSingleton<IGameResultRepository, GameResultRepository>();
        }
    }
}

using Game.Ranking.Infrastructure.Impl;
using Game.Ranking.Infrastructure.Interfaces;
using Game.Ranking.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Ranking.Infrastructure
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

using AutoMapper;
using Game.Ranking.Services.Impl;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Ranking.Services
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGameResultService, GameResultService>();
            services.AddSingleton<ILeaderboardService, LeaderboardService>();
        }

        public static void AddMapperProfiles(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<GameResultMappingProfile>();
            cfg.AddProfile<LeaderboardMappingProfile>();
        }
    }
}

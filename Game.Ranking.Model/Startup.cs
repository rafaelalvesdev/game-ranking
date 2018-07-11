using Game.Ranking.Model.Validators;
using Game.Ranking.Model.Validators.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Ranking.Model
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGameResultValidator, GameResultValidator>();
        }
    }
}

using Nest;

namespace Game.Ranking.Infrastructure.Impl
{
    public class ElasticClientConfigurator
    {
        public ConnectionSettings ConnectionSettings { get; }

        public ElasticClientConfigurator(ConnectionSettings connectionSettings)
        {
            ConnectionSettings = connectionSettings;
        }
    }
}

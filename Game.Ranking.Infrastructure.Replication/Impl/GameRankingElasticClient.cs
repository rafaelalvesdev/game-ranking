using Nest;
using System.Collections.Generic;


namespace Game.Ranking.Infrastructure.Replication.Impl
{
    public class GameRankingElasticClient : ElasticClient
        {
        private HashSet<string> ExistantIndexes { get; set; }

        public GameRankingElasticClient(ConnectionSettings s)
            : base(s)
        {
            ExistantIndexes = new HashSet<string>();
        }

        public void CheckIndexFor<T>() where T : class
        {
            var indexName = typeof(T).Name.ToLower();
            if (ExistantIndexes.Contains(indexName))
                return;

            this.Map<T>(x => x.AutoMap().Index(indexName));

            var request = new IndexExistsRequest(indexName);
            var result = this.IndexExists(request);
            if (!result.Exists)
                this.CreateIndex(indexName);

            ExistantIndexes.Add(indexName);
        }
    }
}
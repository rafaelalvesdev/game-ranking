using Game.Ranking.Infrastructure.Impl;
using Game.Ranking.Infrastructure.Interfaces;
using Nest;
using System.Collections.Generic;

namespace Game.Ranking.Infrastructure.Repositories
{
    public abstract class AbstractRepository : IAbstractRepository
    {
        internal GameRankingElasticClient Client;

        public AbstractRepository(GameRankingElasticClient client)
        {
            Client = client;
        }

        public virtual IResponse Index<T>(T entity) where T : class
        {
            Client.CheckIndexFor<T>();
            return Client.Index(entity, x => x.Index(nameof(T)));
        }

        public virtual IResponse IndexBulk<T>(IEnumerable<T> entities) where T : class
        {
            Client.CheckIndexFor<T>();
            var request = new BulkRequest(nameof(T));
            List<IBulkOperation> operations = new List<IBulkOperation>();

            foreach (var entity in entities)
                operations.Add(new BulkIndexOperation<T>(entity));

            request.Operations = operations;

            return Client.Bulk(request);
        }
    }
}

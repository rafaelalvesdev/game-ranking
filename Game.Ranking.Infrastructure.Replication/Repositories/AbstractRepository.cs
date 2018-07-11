using Game.Ranking.Infrastructure.Replication.Impl;
using Game.Ranking.Infrastructure.Replication.Interfaces;
using Nest;
using System.Collections.Generic;

namespace Game.Ranking.Infrastructure.Replication.Repositories
{
    public abstract class AbstractRepository<T> : IAbstractRepository<T>
        where T : class
    {
        internal GameRankingElasticClient Client;

        public AbstractRepository(GameRankingElasticClient client)
        {
            Client = client;
        }

        public virtual IResponse Index(T entity)
        {
            Client.CheckIndexFor<T>();
            return Client.Index(entity, x => x.Index(nameof(T)));
        }

        public virtual IResponse IndexBulk(IEnumerable<T> entities)
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

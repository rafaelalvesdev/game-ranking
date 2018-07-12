using Game.Ranking.Infrastructure.Replication.Impl;
using Game.Ranking.Infrastructure.Replication.Interfaces;
using Nest;
using System;
using System.Collections.Generic;

namespace Game.Ranking.Infrastructure.Replication.Repositories
{
    public abstract class AbstractRepository<T> : IAbstractRepository<T>
        where T : class
    {
        internal GameRankingElasticClient Client;

        public string IndexName { get; } = typeof(T).Name.ToLower();

        public AbstractRepository(GameRankingElasticClient client)
        {
            Client = client;
        }
        
        public virtual IResponse Index(T entity)
        {
            Client.CheckIndexFor<T>();
            return Client.Index(entity, x => x.Index(IndexName));
        }

        public virtual IResponse IndexBulk(IEnumerable<T> entities)
        {
            Client.CheckIndexFor<T>();
            var request = new BulkRequest(IndexName);
            var operations = new List<IBulkOperation>();

            foreach (var entity in entities)
                operations.Add(new BulkIndexOperation<T>(entity));

            request.Operations = operations;

            return Client.Bulk(request);
        }

        public virtual ISearchResponse<T> Search(Func<SearchDescriptor<T>, ISearchRequest> selector)
        {
            return Client.Search<T>(selector);
        }
    }
}

using Game.Ranking.Infrastructure.Replication.Interfaces;
using Game.Ranking.Model;
using Game.Ranking.Services.Extensions;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using System.Collections.Generic;
using System.Linq;

namespace Game.Ranking.Services.Impl
{
    public class AbstractReplicationService<TEntity> : IAbstractReplicationService<TEntity>
        where TEntity : ReplicableObject
    {
        internal virtual IAbstractRepository<TEntity> Repository { get; set; }

        public AbstractReplicationService(IAbstractRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public ServiceResult Replicate(TEntity entity)
        {
            entity.UpdateReplicatedTime();
            var response = Repository.Index(entity);
            return response.AsServiceResult().SetValid(response?.IsValid ?? false);
        }

        public ServiceResult Replicate(IEnumerable<TEntity> entity)
        {
            entity.ToList().ForEach(x => x.UpdateReplicatedTime());
            var response = Repository.IndexBulk(entity);
            return response.AsServiceResult().SetValid(response?.IsValid ?? false);
        }
    }
}

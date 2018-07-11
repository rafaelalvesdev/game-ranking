using Game.Ranking.Infrastructure.Replication.Interfaces;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using System;
using System.Collections.Generic;

namespace Game.Ranking.Services.Impl
{
    public class AbstractReplicationService<TEntity> : IReplicationService<TEntity>
        where TEntity : class
    {
        internal virtual IAbstractRepository<TEntity> Repository { get; set; }

        public AbstractReplicationService(IAbstractRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public ServiceResult Replicate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Replicate(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }
    }
}

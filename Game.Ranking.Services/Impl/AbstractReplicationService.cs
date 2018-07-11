using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using System;

namespace Game.Ranking.Services.Impl
{
    public class AbstractReplicationService<TEntity> : IReplicationService<TEntity>
        where TEntity : class
    {
        public ServiceResult Replicate(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

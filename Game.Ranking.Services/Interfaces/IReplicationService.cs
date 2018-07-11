using Game.Ranking.Services.Results;
using System.Collections.Generic;

namespace Game.Ranking.Services.Interfaces
{
    public interface IReplicationService<TEntity>
        where TEntity : class
    {
        ServiceResult Replicate(TEntity entity);
        ServiceResult Replicate(List<TEntity> entity);
    }
}

using Game.Ranking.Services.Results;
using System.Collections.Generic;

namespace Game.Ranking.Services.Interfaces
{
    public interface IAbstractReplicationService<TEntity>
        where TEntity : class
    {
        ServiceResult Replicate(TEntity entity);
        ServiceResult Replicate(IEnumerable<TEntity> entity);
    }
}

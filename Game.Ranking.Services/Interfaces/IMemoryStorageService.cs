using Game.Ranking.Services.Results;
using System.Collections.Generic;

namespace Game.Ranking.Services.Interfaces
{
    public interface IMemoryStorageService<TEntity>
        where TEntity : class
    {
        ServiceResult StoreInMemory(TEntity entity);
        ServiceResult StoreInMemory(IEnumerable<TEntity> entities);
    }
}

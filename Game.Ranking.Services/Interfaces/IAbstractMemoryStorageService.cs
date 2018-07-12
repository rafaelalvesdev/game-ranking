using Game.Ranking.Services.Results;
using System.Collections.Generic;

namespace Game.Ranking.Services.Interfaces
{
    public interface IAbstractMemoryStorageService<TEntity>
        where TEntity : class
    {
        ServiceResult StoreInMemory(TEntity entity);
        ServiceResult StoreInMemory(IEnumerable<TEntity> entities);
        ServiceResult GetFromMemory(int? topRecords = null);
        ServiceResult DeleteFromMemory(TEntity entity);
        ServiceResult DeleteFromMemory(List<TEntity> entities);
    }
}

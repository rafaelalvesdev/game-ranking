using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Services.Extensions;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using System.Collections.Generic;
using System.Linq;

namespace Game.Ranking.Services.Impl
{
    public abstract class AbstractMemoryStorageService<TEntity> : IAbstractMemoryStorageService<TEntity>
        where TEntity : class
    {
        IAbstractRepository<TEntity> Repository { get; set; }

        public AbstractMemoryStorageService(IAbstractRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public ServiceResult StoreInMemory(IEnumerable<TEntity> entities)
        {
            Repository.Create(entities);
            return new ServiceResult().Valid();
        }

        public ServiceResult StoreInMemory(TEntity entity)
        {
            Repository.Create(entity);
            return new ServiceResult().Valid();
        }

        public ServiceResult GetFromMemory(int? topRecords = null)
        {
            var numRecords = topRecords ?? 10000;
            var items = Repository.Get().Take(numRecords).ToList();
            return items.AsServiceResult().Valid();
        }

        public ServiceResult DeleteFromMemory(TEntity entity)
        {
            Repository.Delete(entity);
            return new ServiceResult().Valid();
        }

        public ServiceResult DeleteFromMemory(List<TEntity> entities)
        {
            Repository.Delete(entities);
            return new ServiceResult().Valid();
        }
    }
}
﻿using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Services.Extensions;
using Game.Ranking.Services.Interfaces;
using Game.Ranking.Services.Results;
using System.Collections.Generic;

namespace Game.Ranking.Services.Impl
{
    public abstract class AbstractMemoryStorageService<TEntity> : IMemoryStorageService<TEntity>
        where TEntity : class
    {
        IAbstractRepository<TEntity> Repository { get; set; }

        public AbstractMemoryStorageService(IAbstractRepository<TEntity> repository) {
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
    }
}
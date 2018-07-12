using Game.Ranking.Infrastructure.InMemory.Interfaces;
using Game.Ranking.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Ranking.Infrastructure.InMemory.Repositories
{
    public abstract class AbstractRepository<TEntity> : IAbstractRepository<TEntity>
        where TEntity : ReplicableObject
    {
        internal DbContext DbContext;

        #region .ctor
        public AbstractRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion

        #region [ CRUD ]

        public TEntity Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entity.UpdateInsertedTime();
            DbContext.Set<TEntity>().Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public void Create(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var e in entities) e.UpdateInsertedTime();
            DbContext.Set<TEntity>().AddRange(entities);
            DbContext.SaveChanges();
        }

        public IQueryable<TEntity> Get()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().RemoveRange(entities);
            DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            DbContext.Set<TEntity>().Update(entity);
            DbContext.SaveChanges();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");

            DbContext.Set<TEntity>().UpdateRange(entities);
            DbContext.SaveChanges();
        }

        public void Attach(params object[] entities)
        {
            for (var i = 0; i < entities.Length; i++)
                DbContext.Attach(entities[i]);
        }
        #endregion
    }
}
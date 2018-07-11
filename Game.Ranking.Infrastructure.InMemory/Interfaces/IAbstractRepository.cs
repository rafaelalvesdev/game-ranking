using System.Collections.Generic;
using System.Linq;

namespace Game.Ranking.Infrastructure.InMemory.Interfaces
{
    public interface IAbstractRepository<TEntity>
        where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Create(IEnumerable<TEntity> entities);
        IQueryable<TEntity> Get();
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Attach(params object[] entities);
    }
}

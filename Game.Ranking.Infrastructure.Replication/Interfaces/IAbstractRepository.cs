using Nest;
using System.Collections.Generic;

namespace Game.Ranking.Infrastructure.Replication.Interfaces
{
    public interface IAbstractRepository<T> 
        where T : class
    {
        IResponse Index(T entity);
        IResponse IndexBulk(IEnumerable<T> entities);
    }
}

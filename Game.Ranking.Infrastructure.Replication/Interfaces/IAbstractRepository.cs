using Nest;
using System.Collections.Generic;

namespace Game.Ranking.Infrastructure.Replication.Interfaces
{
    public interface IAbstractRepository
    {
        IResponse Index<T>(T entity) where T : class;
        IResponse IndexBulk<T>(IEnumerable<T> entities) where T : class;
    }
}

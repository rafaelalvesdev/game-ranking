using Nest;
using System;
using System.Collections.Generic;

namespace Game.Ranking.Infrastructure.Replication.Interfaces
{
    public interface IAbstractRepository<T> 
        where T : class
    {
        string IndexName { get; }
        ISearchResponse<T> Search(Func<SearchDescriptor<T>, ISearchRequest> selector);
        IResponse Index(T entity);
        IResponse IndexBulk(IEnumerable<T> entities);
    }
}

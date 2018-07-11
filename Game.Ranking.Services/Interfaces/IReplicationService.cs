using Game.Ranking.Services.Results;

namespace Game.Ranking.Services.Interfaces
{
    public interface IReplicationService<TEntity>
        where TEntity : class
    {
        ServiceResult Replicate(TEntity entity);
    }
}

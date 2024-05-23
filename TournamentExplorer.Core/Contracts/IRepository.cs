using System.Linq.Expressions;

namespace TournamentExplorer.Core.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> GetIncluding<T>(Expression<Func<TEntity, T>> predicate);
        Task<TEntity?> GetByIdAsync(int id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
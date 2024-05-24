using System.Linq.Expressions;

namespace TournamentExplorer.Core.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty);
        Task<TEntity?> GetByIdAsync(int id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
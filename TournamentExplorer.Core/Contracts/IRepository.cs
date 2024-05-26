using System.Linq.Expressions;

namespace TournamentExplorer.Core.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(IQueryParams parameters);
        IEnumerable<TEntity> Get<TProperty>(IQueryParams parameters, Expression<Func<TEntity, TProperty>> navigationProperty);
        Task<TEntity?> GetByIdAsync(int id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
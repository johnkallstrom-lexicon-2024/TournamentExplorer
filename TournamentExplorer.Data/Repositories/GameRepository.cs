using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly TournamentExplorerDbContext _context;

        public GameRepository(TournamentExplorerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Game> Get() => _context.Games.AsNoTracking();

        public IQueryable<Game> Get(IQueryParams parameters)
        {
            var games = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                games = games.Where(g => g.Name.Contains(parameters.SearchTerm) || g.Tournament.Title.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrEmpty(parameters.SortOrder))
            {
                games = parameters.SortOrder.ToLower().Equals("asc") ? games.OrderBy(g => g.Id) : games;
                games = parameters.SortOrder.ToLower().Equals("desc") ? games.OrderByDescending(g => g.Id) : games;
            }

            return games;
        }

        public IQueryable<Game> Get<TProperty>(IQueryParams parameters, Expression<Func<Game, TProperty>> navigationProperty)
        {
            var games = navigationProperty != null ? _context.Games
                .Include(navigationProperty)
                .AsQueryable() : _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                games = games.Where(g => g.Name.Contains(parameters.SearchTerm) || g.Tournament.Title.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrEmpty(parameters.SortOrder))
            {
                games = parameters.SortOrder.ToLower().Equals("asc") ? games.OrderBy(g => g.Id) : games;
                games = parameters.SortOrder.ToLower().Equals("desc") ? games.OrderByDescending(g => g.Id) : games;
            }

            return games;
        }

        public IQueryable<Game> Get(Expression<Func<Game, bool>> filter)
        {
            var games = _context.Games
                .Where(filter)
                .AsNoTracking();

            return games;
        }

        public IQueryable<Game> Get<TProperty>(Expression<Func<Game, TProperty>> navigationProperty)
        {
            var games = _context.Games
                .Include(navigationProperty)
                .AsNoTracking();

            return games;
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            var game = await _context.Games
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);

            return game;
        }

        public Game Add(Game entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return _context.Games.Add(entity).Entity;
        }

        public void Update(Game entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _context.Games.Update(entity);
        }

        public void Delete(Game entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _context.Games.Remove(entity);
        }
    }
}

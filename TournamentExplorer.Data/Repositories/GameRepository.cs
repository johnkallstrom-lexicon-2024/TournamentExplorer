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

        public IEnumerable<Game> Get() => _context.Games.AsNoTracking();

        public IEnumerable<Game> Get(IQueryParams parameters)
        {
            IQueryable<Game> games;

            if (parameters.CurrentPage.HasValue && parameters.PageSize.HasValue)
            {
                games = _context.Games
                    .Skip((parameters.CurrentPage.Value - 1) * parameters.PageSize.Value)
                    .Take(parameters.PageSize.Value);
            }
            else
            {
                games = _context.Games;
            }

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                games = games.Where(g => g.Name.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrEmpty(parameters.SortBy) && !string.IsNullOrEmpty(parameters.SortOrder))
            {
                switch (parameters.SortBy)
                {
                    case "name":
                        games = parameters.SortOrder.ToLower().Equals("asc") ? games.OrderBy(g => g.Name) : games;
                        games = parameters.SortOrder.ToLower().Equals("desc") ? games.OrderByDescending(g => g.Name) : games;
                        break;
                    default:
                        games = parameters.SortOrder.ToLower().Equals("asc") ? games.OrderBy(g => g.Id) : games;
                        games = parameters.SortOrder.ToLower().Equals("desc") ? games.OrderByDescending(g => g.Id) : games;
                        break;
                }
            }

            return games.AsNoTracking();
        }

        public IEnumerable<Game> Get<TProperty>(IQueryParams parameters, Expression<Func<Game, TProperty>> navigationProperty)
        {
            IQueryable<Game> games = navigationProperty != null ? _context.Games.Include(navigationProperty) : _context.Games;

            if (parameters.CurrentPage.HasValue && parameters.PageSize.HasValue)
            {
                games = games
                    .Skip((parameters.CurrentPage.Value - 1) * parameters.PageSize.Value)
                    .Take(parameters.PageSize.Value);
            }

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                games = games.Where(g => g.Name.Contains(parameters.SearchTerm) || g.Tournament.Title.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrEmpty(parameters.SortBy) && !string.IsNullOrEmpty(parameters.SortOrder))
            {
                switch (parameters.SortBy)
                {
                    case "name":
                        games = parameters.SortOrder.ToLower().Equals("asc") ? games.OrderBy(g => g.Name) : games;
                        games = parameters.SortOrder.ToLower().Equals("desc") ? games.OrderByDescending(g => g.Name) : games;
                        break;
                    default:
                        games = parameters.SortOrder.ToLower().Equals("asc") ? games.OrderBy(g => g.Id) : games;
                        games = parameters.SortOrder.ToLower().Equals("desc") ? games.OrderByDescending(g => g.Id) : games;
                        break;
                }
            }

            return games.AsNoTracking();
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

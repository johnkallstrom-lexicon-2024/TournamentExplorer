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

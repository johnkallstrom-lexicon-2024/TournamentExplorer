using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data.Repositories
{
    public class TournamentRepository : IRepository<Tournament>
    {
        private readonly TournamentExplorerDbContext _context;

        public TournamentRepository(TournamentExplorerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Tournament> Get() => _context.Tournaments.AsNoTracking();

        public IQueryable<Tournament> Get(IQueryParams parameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tournament> Get<TProperty>(IQueryParams parameters, Expression<Func<Tournament, TProperty>> navigationProperty)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tournament> Get<TProperty>(Expression<Func<Tournament, TProperty>> navigationProperty)
        {
            var tournaments = _context.Tournaments
                .Include(navigationProperty)
                .AsNoTracking();

            return tournaments;
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);

            return tournament;
        }

        public Tournament Add(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            return _context.Tournaments.Add(tournament).Entity;
        }

        public void Update(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            _context.Tournaments.Update(tournament);
        }

        public void Delete(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            _context.Tournaments.Remove(tournament);
        }
    }
}

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

        public IQueryable<Tournament> GetList() => _context.Tournaments.AsNoTracking();

        public IQueryable<Tournament> GetListIncluding<T>(Expression<Func<Tournament, T>> predicate)
        {
            var tournaments = _context.Tournaments
                .Include(predicate)
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

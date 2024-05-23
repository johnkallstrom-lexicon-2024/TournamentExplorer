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

        public IEnumerable<Tournament> Get() => _context.Tournaments.AsNoTracking();

        public IEnumerable<Tournament> GetIncluding<T>(Expression<Func<Tournament, T>> predicate)
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

        public Tournament Add(Tournament entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Tournament entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tournament entity)
        {
            throw new NotImplementedException();
        }
    }
}

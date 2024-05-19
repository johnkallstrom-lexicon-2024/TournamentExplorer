using Microsoft.EntityFrameworkCore;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentExplorerDbContext _context;

        public TournamentRepository(TournamentExplorerDbContext context)
        {
            _context = context;
        }

        public Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tournament> CreateAsync(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            var tournaments = await _context.Tournaments.ToListAsync();
            return tournaments;
        }

        public async Task<Tournament?> GetAsync(int id, bool includeGames)
        {
            Tournament? tournament = null;
            if (includeGames)
            {
                tournament = await _context.Tournaments
                    .Include(t => t.Games)
                    .FirstOrDefaultAsync(t => t.Id == id);

                return tournament;
            }
            else
            {
                tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == id);
            }

            return tournament;
        }

        public Task UpdateAsync(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}

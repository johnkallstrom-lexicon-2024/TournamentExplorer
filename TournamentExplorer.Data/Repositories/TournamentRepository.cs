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

        public async Task<bool> AnyAsync() => await _context.Tournaments.AnyAsync();

        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            return (await _context.Tournaments.AddAsync(tournament)).Entity;
        }

        public void Delete(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            _context.Tournaments.Remove(tournament);
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync(bool includeGames = false)
        {
            IEnumerable<Tournament> tournaments;
            if (includeGames)
            {
                tournaments = await _context.Tournaments
                    .Include(t => t.Games)
                    .ToListAsync();
            }
            else
            {
                tournaments = await _context.Tournaments.ToListAsync();
            }

            return tournaments;
        }

        public async Task<Tournament?> GetAsync(int id, bool includeGames = false)
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

        public void Update(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            _context.Update(tournament);
        }
    }
}

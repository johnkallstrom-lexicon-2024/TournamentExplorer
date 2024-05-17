using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
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

        public Task<IEnumerable<Tournament>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tournament> GetAsync(int id, bool includeGames)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}

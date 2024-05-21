using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Core.Contracts
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament?> GetAsync(int id, bool includeGames);
        Task<bool> AnyAsync();
        Task<Tournament> CreateAsync(Tournament tournament);
        void Update(Tournament tournament);
        void Delete(Tournament tournament);
    }
}

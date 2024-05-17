using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Core.Contracts
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id, bool includeGames);
        Task<bool> AnyAsync();
        Task<Tournament> CreateAsync(Tournament tournament);
        Task UpdateAsync(Tournament tournament);
        Task DeleteAsync(Tournament tournament);
    }
}

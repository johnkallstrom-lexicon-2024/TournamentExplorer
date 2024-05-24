using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Core.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Tournament> TournamentRepository { get; }
        IRepository<Game> GameRepository { get; }
        Task SaveAsync();
    }
}

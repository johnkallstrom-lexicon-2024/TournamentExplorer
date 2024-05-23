using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Core.Contracts
{
    public interface IUnitOfWork
    {
        IRepository<Tournament> TournamentRepository { get; }
        Task CompleteAsync();
    }
}

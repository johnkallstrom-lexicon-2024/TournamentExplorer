namespace TournamentExplorer.Core.Contracts
{
    public interface IUnitOfWork
    {
        public ITournamentRepository TournamentRepository { get; }
        Task CompleteAsync();
    }
}

using TournamentExplorer.Core.Contracts;

namespace TournamentExplorer.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentExplorerDbContext _context;

        public UnitOfWork(TournamentExplorerDbContext context)
        {
            _context = context;
            TournamentRepository = new TournamentRepository(_context);
        }

        public ITournamentRepository TournamentRepository { get; }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}

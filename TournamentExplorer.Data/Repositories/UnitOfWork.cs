using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

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

        public IRepository<Tournament> TournamentRepository { get; }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}

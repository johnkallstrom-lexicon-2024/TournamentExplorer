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
            GameRepository = new GameRepository(_context);
        }

        public IRepository<Tournament> TournamentRepository { get; }
        public IRepository<Game> GameRepository { get; }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}

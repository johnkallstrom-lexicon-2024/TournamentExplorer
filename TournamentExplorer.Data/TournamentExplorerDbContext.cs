using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data
{
    public class TournamentExplorerDbContext : DbContext
    {
        public TournamentExplorerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

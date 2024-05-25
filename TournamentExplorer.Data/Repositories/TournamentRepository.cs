using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;

namespace TournamentExplorer.Data.Repositories
{
    public class TournamentRepository : IRepository<Tournament>
    {
        private readonly TournamentExplorerDbContext _context;

        public TournamentRepository(TournamentExplorerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Tournament> Get() => _context.Tournaments.AsNoTracking();

        public IQueryable<Tournament> Get(IQueryParams parameters)
        {
            var tournaments = _context.Tournaments.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                tournaments = tournaments.Where(t => t.Title.Contains(parameters.SearchTerm) || 
                t.City.Contains(parameters.SearchTerm) || 
                t.Country.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrEmpty(parameters.SortOrder))
            {
                tournaments = parameters.SortOrder.ToLower().Equals("asc") ? tournaments.OrderBy(t => t.Id) : tournaments;
                tournaments = parameters.SortOrder.ToLower().Equals("desc") ? tournaments.OrderByDescending(t => t.Id) : tournaments;
            }

            return tournaments;
        }

        public IQueryable<Tournament> Get<TProperty>(IQueryParams parameters, Expression<Func<Tournament, TProperty>> navigationProperty)
        {
            var tournaments = navigationProperty != null ? 
                _context.Tournaments.Include(navigationProperty).AsQueryable() : _context.Tournaments.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                tournaments = tournaments.Where(t => t.Title.Contains(parameters.SearchTerm) ||
                t.City.Contains(parameters.SearchTerm) ||
                t.Country.Contains(parameters.SearchTerm));
            }

            if (!string.IsNullOrEmpty(parameters.SortOrder))
            {
                tournaments = parameters.SortOrder.ToLower().Equals("asc") ? tournaments.OrderBy(t => t.Id) : tournaments;
                tournaments = parameters.SortOrder.ToLower().Equals("desc") ? tournaments.OrderByDescending(t => t.Id) : tournaments;
            }

            return tournaments;
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);

            return tournament;
        }

        public Tournament Add(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            return _context.Tournaments.Add(tournament).Entity;
        }

        public void Update(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            _context.Tournaments.Update(tournament);
        }

        public void Delete(Tournament tournament)
        {
            if (tournament is null)
            {
                throw new ArgumentNullException(nameof(tournament));
            }

            _context.Tournaments.Remove(tournament);
        }
    }
}

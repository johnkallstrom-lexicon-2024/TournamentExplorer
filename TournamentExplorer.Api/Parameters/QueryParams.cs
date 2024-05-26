using TournamentExplorer.Core.Contracts;

namespace TournamentExplorer.Api.Parameters
{
    public abstract class QueryParams : IQueryParams
    {
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
    }
}

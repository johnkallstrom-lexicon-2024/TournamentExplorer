using TournamentExplorer.Core.Contracts;

namespace TournamentExplorer.Api.Parameters
{
    public class QueryParams : IQueryParams
    {
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
    }
}

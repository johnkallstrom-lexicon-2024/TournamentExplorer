using TournamentExplorer.Core.Contracts;

namespace TournamentExplorer.Api.Parameters
{
    public class TournamentQueryParams : QueryParams, IQueryParams
    {
        public bool IncludeGames { get; set; }
    }
}

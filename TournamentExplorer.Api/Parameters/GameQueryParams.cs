using TournamentExplorer.Core.Contracts;

namespace TournamentExplorer.Api.Parameters
{
    public class GameQueryParams : QueryParams, IQueryParams
    {
        public bool IncludeTournament { get; set; }
    }
}

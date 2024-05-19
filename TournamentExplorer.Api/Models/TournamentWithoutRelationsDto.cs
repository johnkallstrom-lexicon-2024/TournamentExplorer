using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Api.Models
{
    public class TournamentWithoutRelationsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Location { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public TournamentType Type { get; set; }
    }
}

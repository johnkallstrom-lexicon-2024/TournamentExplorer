using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Api.Models
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public TournamentType Type { get; set; }

        public IEnumerable<GameWithoutRelationsDto> Games { get; set; } = new List<GameWithoutRelationsDto>();
    }
}

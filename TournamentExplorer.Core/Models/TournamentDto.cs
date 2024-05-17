namespace TournamentExplorer.Core.Models
{
    public class TournamentDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }

        public IEnumerable<GameDto> Games { get; set; } = new List<GameDto>();
    }
}

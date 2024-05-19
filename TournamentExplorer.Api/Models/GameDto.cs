namespace TournamentExplorer.Api.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Time { get; set; }

        public TournamentDto Tournament { get; set; } = default!;
    }
}

namespace TournamentExplorer.Api.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
    }
}

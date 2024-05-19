namespace TournamentExplorer.Api.Models
{
    public class GameWithoutRelationsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
    }
}

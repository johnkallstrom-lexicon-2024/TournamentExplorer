namespace TournamentExplorer.Api.Models
{
    public record GameSlimDto
    {
        public string? Name { get; init; }
        public DateTime Time { get; init; }
        public int Duration { get; init; }
    }
}

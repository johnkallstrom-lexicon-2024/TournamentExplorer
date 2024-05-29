namespace TournamentExplorer.Api.Models
{
    public record GameWithoutRelationsDto
    {
        public string? Name { get; init; }
        public DateTime Time { get; init; }
        public int Duration { get; init; }
    }
}

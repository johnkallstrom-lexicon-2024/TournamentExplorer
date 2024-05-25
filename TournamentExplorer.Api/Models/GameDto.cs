namespace TournamentExplorer.Api.Models
{
    public record GameDto
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public DateTime Time { get; init; }
        public int Duration { get; init; }

        public TournamentSlimDto Tournament { get; init; } = default!;
    }
}

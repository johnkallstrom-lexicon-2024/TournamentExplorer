using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Api.Models
{
    public record TournamentWithoutRelationsDto
    {
        public string? Title { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate => StartDate.AddMonths(3);
        public string? City { get; init; }
        public string? Country { get; init; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TournamentType Type { get; init; }
    }
}

using System.ComponentModel.DataAnnotations;
using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Core.Entities
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Location { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public TournamentType Type { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}

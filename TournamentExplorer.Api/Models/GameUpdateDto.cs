using System.ComponentModel.DataAnnotations;

namespace TournamentExplorer.Api.Models
{
    public class GameUpdateDto
    {
        [MaxLength(50)]
        [Required]
        public string? Name { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int Duration { get; set; }

    }
}

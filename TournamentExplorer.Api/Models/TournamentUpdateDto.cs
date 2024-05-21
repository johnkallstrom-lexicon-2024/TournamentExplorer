using System.ComponentModel.DataAnnotations;
using TournamentExplorer.Core.Enums;

namespace TournamentExplorer.Api.Models
{
    public class TournamentUpdateDto
    {
        [MaxLength(20)]
        [Required()]
        public string Title { get; set; } = default!;

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime StartDate { get; set; }

        [MaxLength(50)]
        [Required]
        public string City { get; set; } = default!;

        [MaxLength(50)]
        [Required]
        public string Country { get; set; } = default!;

        [EnumDataType(typeof(TournamentType))]
        [Required]
        public TournamentType Type { get; set; }
    }
}

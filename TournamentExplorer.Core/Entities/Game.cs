﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentExplorer.Core.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime Time { get; set; }
        public int Duration { get; set; }

        [ForeignKey("TournamentId")]
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = default!;
    }
}

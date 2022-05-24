using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Enum;

namespace R1StatsModel.Model
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Playlist { get; set; }
        public string Map { get; set; }
        public GameType GameType { get; set; }
        public string Variant { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<MatchTeam> Teams { get; set; }
        public ICollection<MatchPlayer> Players { get; set; }
    }
}

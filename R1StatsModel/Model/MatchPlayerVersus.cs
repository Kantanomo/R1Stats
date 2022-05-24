using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1StatsModel.Model
{
    public class MatchPlayerVersus
    {
        [Key]
        public int Id { get; set; }
        public MatchPlayer Parent { get; set; }
        public int OpponentId { get; set; }
        public int Killed { get; set; }
        public int KilledBy { get; set; }
    }
}

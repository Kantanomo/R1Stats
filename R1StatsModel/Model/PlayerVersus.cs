using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1StatsModel.Model
{
    public class PlayerVersus
    {
        [Key]
        public int Id { get; set; }
        public Player Parent { get; set; }
        public int OpponentId { get; set; }
        public int Killed { get; set; }
        public int KilledBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Enum;

namespace R1StatsModel.Model
{
    public class MatchTeam
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public string Place { get; set; }
        public string Score { get; set; }
    }
}

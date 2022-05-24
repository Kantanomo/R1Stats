using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Enum;

namespace R1StatsModel.Model
{
    public class PlayerGameType
    {
        [Key]
        public int Id { get; set; }
        public GameType GameType { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Kills { get; set; }
        public int Assists { get; set; }
        public int Deaths { get; set; }
        public int Suicides { get; set; }
        public int ShotsHit { get; set; }
        public int ShotsFired { get; set; }
        public int HeadShots { get; set; }
        public int TotalMedals { get; set; }
        public int Score { get; set; }
        
        public virtual int HitPercentage =>
            (ShotsFired != 0 && ShotsHit != 0) ? ShotsHit / ShotsFired : 0;
        public virtual float KDA =>
            (Deaths != 0) ? (Kills + Assists) / Deaths : Kills + Assists;
        public virtual int KDARatio =>
            Kills - Deaths;
        public virtual float WinLossRatio =>
            (Losses != 0) ? Wins / Losses : Wins;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Classes;

namespace R1StatsModel.Model
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
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
        public int CarrierKills { get; set; }
        public int BallKills { get; set; }
        public int BombGrabs { get; set; }
        public int BomberKills { get; set; }
        public int FlagSaves { get; set; }
        public int FlagSteals { get; set; }
        public int AverageLife { get; set; }
        public int BestSpree { get; set; }
        public int KingsKilled { get; set; }
        public int KillsAsKing { get; set; }
        public float XP { get; set; }
        public int Rank { get; set; }
        public ICollection<PlayerGameType> GameTypeStats { get; set; }
        public ICollection<PlayerVersus> Versus { get; set; }
        public ICollection<MatchPlayer> MatchHistroy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float HitPercentage { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float KDA { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int KDARatio { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float WinLossRatio { get; private set; }

    }
}

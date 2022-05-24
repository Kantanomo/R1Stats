using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Enum;
using R1StatsModel.Extensions;

namespace R1StatsModel.Model
{
    public class MatchPlayer
    {
        public int Id { get; set; }
        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
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
        public int Score { get; set; }
        public Team Team { get; set; }
        public int Place { get; set; }
        public float XP { get; set; }
        public int Rank { get; set; }
        public ICollection<MatchPlayerVersus> Versus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float HitPercentage { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float KDA { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int KDARatio { get; private set; }

        public virtual string PlaceString
            => Place switch
            {
                1 => "1st",
                2 => "2nd",
                3 => "3rd",
                _ => Place.ToString() + "th"
            };

        public virtual string AverageLifeString
        {
            get
            {
                var time = TimeSpan.FromSeconds(AverageLife);
                return $"{time.Minutes}:{time.Seconds}";
            }
        }

        public virtual string ScoreString
        {
            get
            {
                switch (Match.GameType)
                {
                    case GameType.Assault:
                    case GameType.CTF:
                    case GameType.Juggernaut:
                    case GameType.Slayer:
                        return Score.ToString();
                    case GameType.Territories:
                    case GameType.Oddball:
                    case GameType.KOTH:
                        var time = TimeSpan.FromSeconds(Score);
                        return $"{time.Minutes}:{time.Seconds}";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
}
}

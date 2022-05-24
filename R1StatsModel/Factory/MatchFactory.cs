using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using R1StatsModel.Classes;
using R1StatsModel.Enum;
using R1StatsModel.Extensions;
using R1StatsModel.Model;

namespace R1StatsModel.Factory
{
    public class MatchFactory
    {
        private StatsContext context;

        public MatchFactory(StatsContext context)
        {
            this.context = context;
        }

        public Match? CreateMatch(XElement matchNode)
        {
            var match = new Match();
            match.Hash = "ASDF";
            var gameNode = matchNode.Descendants("Game");
            match.Playlist = matchNode.Descendants("Playlist").First().Value;
            match.Map = gameNode.Descendants("Map").First().Value;
            match.GameType = gameNode.Descendants("Game_Type").First().Value switch
            {
                "Slayer" => GameType.Slayer,
                "Capture the Flag" => GameType.CTF,
                "Assault" => GameType.Assault,
                "Territories" => GameType.Territories,
                "Oddball" => GameType.Oddball,
                "King of the Hill" => GameType.KOTH,
                "Juggernaut" => GameType.Juggernaut,
                _ => throw new ArgumentOutOfRangeException()
            };
            match.Variant = gameNode.Descendants("Variant").First().Value;
            match.StartTime = DateTime.Parse(gameNode.Descendants("Start_Time").First().Value.Replace("UTC", "Z"));
            match.EndTime = DateTime.Parse(gameNode.Descendants("End_Time").First().Value.Replace("UTC", "Z"));

            ICollection<MatchTeam> teams = new List<MatchTeam>();
            foreach (var teamNode in matchNode.Descendants("Teams").Descendants("Team"))
            {
                var item = new MatchTeam();
                item.Place = teamNode.Descendants("Place").First().Value;
                item.Team = teamNode.Descendants("Color").First().Value switch
                {
                    "Red Team" => Team.Red,
                    "Blue Team" => Team.Blue,
                    "Yellow Team" => Team.Yellow,
                    "Green Team" => Team.Green,
                    "Purple Team" => Team.Purple,
                    "Orange Team" => Team.Orange,
                    "Brown Team" => Team.Brown,
                    "Pink Team" => Team.Pink,
                    _ => Team.None
                };
                
                item.Score = teamNode.Descendants("Score").First().Value;
                teams.Add(item);
            }

            //Fix because the place for a player is incorrect
            foreach (var playerNode in matchNode.Descendants("Players").Descendants("Player"))
            {
                var playerTeam = playerNode.GetValue("Team") switch
                {
                    "Red Team" => Team.Red,
                    "Blue Team" => Team.Blue,
                    "Yellow Team" => Team.Yellow,
                    "Green Team" => Team.Green,
                    "Purple Team" => Team.Purple,
                    "Orange Team" => Team.Orange,
                    "Brown Team" => Team.Brown,
                    "Pink Team" => Team.Pink,
                    _ => Team.None
                };
                if (playerTeam == Team.None)
                    return null;
                var teamItem = teams.SingleOrDefault(x => x.Team == playerTeam);
                if (teamItem == null)
                    throw new IndexOutOfRangeException("Matching Team Element was not found");
                playerNode.Descendants("Place").First().Value = teamItem.Place;
            }
            match.Players = new List<MatchPlayer>();
            match.Teams = teams;
            context.Matches.Add(match);
            context.SaveChanges();
            return match;
        }

        public Player CreatePlayer(XElement playerNode, Match match)
        {
            var item = new Player();
            item.Name = playerNode.GetValue("Name");

            item.Kills = playerNode.GetValueI("Kills");
            item.Assists = playerNode.GetValueI("Assists");
            item.Deaths = playerNode.GetValueI("Deaths");
            item.Suicides = playerNode.GetValueI("Suicides");
            item.ShotsHit = playerNode.GetValueI("Shots_Hit");
            item.ShotsFired = playerNode.GetValueI("Shots_Fired");
            item.HeadShots = playerNode.GetValueI("Head_Shots");
            item.TotalMedals = playerNode.GetValueI("Total_Medals");
            item.GameTypeStats = new List<PlayerGameType>();
            var itemGame = new PlayerGameType();
            itemGame.GameType = match.GameType;
            itemGame.Kills = playerNode.GetValueI("Kills");
            itemGame.Assists = playerNode.GetValueI("Assists");
            itemGame.Deaths = playerNode.GetValueI("Deaths");
            itemGame.Suicides = playerNode.GetValueI("Suicides");
            itemGame.ShotsHit = playerNode.GetValueI("Shots_Hit");
            itemGame.ShotsFired = playerNode.GetValueI("Shots_Fired");
            itemGame.HeadShots = playerNode.GetValueI("Head_Shots");
            itemGame.TotalMedals = playerNode.GetValueI("Total_Medals");
            switch (match.GameType)
            {
                case GameType.Slayer:
                    item.BestSpree = playerNode.GetValueI("Best_Spree");
                    item.AverageLife = playerNode.GetValueTime("Average_Life");
                    itemGame.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.CTF:
                    item.FlagSaves = playerNode.GetValueI("Flag_Saves");
                    item.FlagSteals = playerNode.GetValueI("Flag_Steals");
                    itemGame.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.Assault:
                    item.BombGrabs = playerNode.GetValueI("Bomb_Grabs");
                    item.BomberKills = playerNode.GetValueI("Bomber_Kills");
                    itemGame.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.Territories:
                    itemGame.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.Oddball:
                    item.CarrierKills = playerNode.GetValueI("Carrier_Kills");
                    item.BallKills = playerNode.GetValueI("Ball_Kills");
                    itemGame.Score = playerNode.GetValueTime("Score");
                    break;
                case GameType.KOTH:
                    item.KingsKilled = playerNode.GetValueI("Kings_Killed");
                    item.KillsAsKing = playerNode.GetValueI("Kills_From");
                    itemGame.Score = playerNode.GetValueTime("Score");
                    break;
                case GameType.Juggernaut:
                    itemGame.Score = playerNode.GetValueI("Score");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (playerNode.GetValue("Place"))
            {
                case "1st":
                    item.Wins = 1;
                    itemGame.Wins = 1;
                    break;
                default:
                    item.Losses = 1;
                    itemGame.Losses = 1;
                    break;
            }

            item.GameTypeStats.Add(itemGame);
            context.Players.Add(item);
            context.SaveChanges();
            return item;
        }

        public Player UpdatePlayer(XElement playerNode, Match match)
        {
            var item = context.Players.Include(x => x.GameTypeStats)
                .SingleOrDefault(x => x.Name == playerNode.GetValue("Name"));

            item.Kills += playerNode.GetValueI("Kills");
            item.Assists += playerNode.GetValueI("Assists");
            item.Deaths += playerNode.GetValueI("Deaths");
            item.Suicides += playerNode.GetValueI("Suicides");
            item.ShotsHit += playerNode.GetValueI("Shots_Hit");
            item.ShotsFired += playerNode.GetValueI("Shots_Fired");
            item.HeadShots += playerNode.GetValueI("Head_Shots");
            item.TotalMedals += playerNode.GetValueI("Total_Medals");

            var itemGame = item.GameTypeStats.SingleOrDefault(x => x.GameType == match.GameType);
            if (itemGame == null)
                itemGame = new PlayerGameType();
            itemGame.GameType = match.GameType;
            itemGame.Kills += playerNode.GetValueI("Kills");
            itemGame.Assists += playerNode.GetValueI("Assists");
            itemGame.Deaths += playerNode.GetValueI("Deaths");
            itemGame.Suicides += playerNode.GetValueI("Suicides");
            itemGame.ShotsHit += playerNode.GetValueI("Shots_Hit");
            itemGame.ShotsFired += playerNode.GetValueI("Shots_Fired");
            itemGame.HeadShots += playerNode.GetValueI("Head_Shots");
            itemGame.TotalMedals += playerNode.GetValueI("Total_Medals");
            switch (match.GameType)
            {
                case GameType.Slayer:
                    item.BestSpree = (item.BestSpree < playerNode.GetValueI("Best_Spree")) ? playerNode.GetValueI("Best_Spree") : item.BestSpree;
                    item.AverageLife = playerNode.GetValueTime("Average_Life");
                    itemGame.Score += playerNode.GetValueI("Score");
                    break;
                case GameType.CTF:
                    item.FlagSaves += playerNode.GetValueI("Flag_Saves");
                    item.FlagSteals += playerNode.GetValueI("Flag_Steals");
                    itemGame.Score += playerNode.GetValueI("Score");
                    break;
                case GameType.Assault:
                    item.BombGrabs += playerNode.GetValueI("Bomb_Grabs");
                    item.BomberKills += playerNode.GetValueI("Bomber_Kills");
                    itemGame.Score += playerNode.GetValueI("Score");
                    break;
                case GameType.Territories:
                    itemGame.Score += playerNode.GetValueI("Score");
                    break;
                case GameType.Oddball:
                    item.CarrierKills += playerNode.GetValueI("Carrier_Kills");
                    item.BallKills += playerNode.GetValueI("Ball_Kills");
                    itemGame.Score += playerNode.GetValueTime("Score");
                    break;
                case GameType.KOTH:
                    item.KingsKilled += playerNode.GetValueI("Kings_Killed");
                    item.KillsAsKing += playerNode.GetValueI("Kills_From");
                    itemGame.Score += playerNode.GetValueTime("Score");
                    break;
                case GameType.Juggernaut:
                    itemGame.Score += playerNode.GetValueI("Score");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (playerNode.GetValue("Place"))
            {
                case "1st":
                    item.Wins += 1;
                    break;
                default:
                    item.Losses += 1;
                    break;
            }

            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            return item;
        }

        public MatchPlayer CreateUpdatePlayer(XElement playerNode, Match match)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            if (context.Players.SingleOrDefault(x => x.Name == playerNode.GetValue("Name")) == null)
            {
                var p = CreatePlayer(playerNode, match);
                var a = CreateMatchPlayer(playerNode, match, p);
                context.ChangeTracker.AutoDetectChangesEnabled = true;
                context.SaveChanges();
                return a;
                //match.Players.Add(a);
            }
            else
            {
                var p = UpdatePlayer(playerNode, match);
                var a = CreateMatchPlayer(playerNode, match, p);
                context.ChangeTracker.AutoDetectChangesEnabled = true;
                context.SaveChanges();
                return a;
                //match.Players.Add(a);
            }

            
        }

        public void UpdatePlayerXP(List<MatchPlayer> players)
        {
            var XPChanges = BungieELORanking.CalculateXP(players);
            foreach (var xpChange in XPChanges)
            {
                var player = context.Players.Single(x => x.Id == xpChange.Key);
                player.XP += xpChange.Value;
                player.Rank = BungieELORanking.XPToRank(player.XP);
                var mPlayer = players.Single(x => x.Player.Id == xpChange.Key);
                mPlayer.XP = player.XP;
                mPlayer.Rank = player.Rank;
                context.Entry(mPlayer).State = EntityState.Modified;
                context.Entry(player).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public MatchPlayer CreateMatchPlayer(XElement playerNode, Match match, Player player)
        {
            var item = new MatchPlayer();
            item.Player = player;
            item.Match = match;
            item.Kills = playerNode.GetValueI("Kills");
            item.Assists = playerNode.GetValueI("Assists");
            item.Deaths = playerNode.GetValueI("Deaths");
            item.Suicides = playerNode.GetValueI("Suicides");
            item.ShotsHit = playerNode.GetValueI("Shots_Hit");
            item.ShotsFired = playerNode.GetValueI("Shots_Fired");
            item.HeadShots = playerNode.GetValueI("Head_Shots");
            item.TotalMedals = playerNode.GetValueI("Total_Medals");

            switch (match.GameType)
            {
                case GameType.Slayer:
                    item.BestSpree = playerNode.GetValueI("Best_Spree");
                    item.AverageLife = playerNode.GetValueTime("Average_Life");
                    item.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.CTF:
                    item.FlagSaves = playerNode.GetValueI("Flag_Saves");
                    item.FlagSteals = playerNode.GetValueI("Flag_Steals");
                    item.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.Assault:
                    item.BombGrabs = playerNode.GetValueI("Bomb_Grabs");
                    item.BomberKills = playerNode.GetValueI("Bomber_Kills");
                    item.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.Territories:
                    item.Score = playerNode.GetValueI("Score");
                    break;
                case GameType.Oddball:
                    item.CarrierKills = playerNode.GetValueI("Carrier_Kills");
                    item.BallKills = playerNode.GetValueI("Ball_Kills");
                    item.Score = playerNode.GetValueTime("Score");
                    break;
                case GameType.KOTH:
                    item.KingsKilled = playerNode.GetValueI("Kings_Killed");
                    item.KillsAsKing = playerNode.GetValueI("Kills_From");
                    item.Score = playerNode.GetValueTime("Score");
                    break;
                case GameType.Juggernaut:
                    item.Score = playerNode.GetValueI("Score");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            item.Team = playerNode.Descendants("Team").First().Value switch
            {
                "Red Team" => Team.Red,
                "Blue Team" => Team.Blue,
                "Yellow Team" => Team.Yellow,
                "Green Team" => Team.Green,
                "Purple Team" => Team.Purple,
                "Orange Team" => Team.Orange,
                "Brown Team" => Team.Brown,
                "Pink Team" => Team.Pink,
                _ => Team.None
            };

            item.Place = playerNode.GetValue("Place") switch
            {
                "1st" => 1,
                "2nd" => 2,
                "3rd" => 3,
                "4th" => 4,
                "5th" => 5,
                "6th" => 6,
                "7th" => 7,
                "8th" => 8,
                "9th" => 9,
                "10th" => 10,
                "11th" => 11,
                "12th" => 12,
                "13th" => 13,
                "14th" => 14,
                "15th" => 15,
                "16th" => 16,
                _ => -1
            };
            match.Players.Add(item);
            context.Entry(match).State = EntityState.Modified;
            context.SaveChanges();
            return item;
        }

        private void UpdatePlayerVersus(MatchPlayerVersus versusData)
        {
            var t = context.MatchPlayers.Single(x => x.Id == versusData.OpponentId);
            if (context.PlayerVersus.SingleOrDefault(x =>
                    x.Parent.Id == versusData.Parent.Player.Id && x.OpponentId == t.Player.Id) == null)
            {
                var item = new PlayerVersus();
                item.Parent = versusData.Parent.Player;
                item.OpponentId = t.Player.Id;
                item.Killed = versusData.Killed;
                item.KilledBy = versusData.KilledBy;
                context.PlayerVersus.Add(item);
                context.SaveChanges();
            }
            else
            {
                var item = context.PlayerVersus.SingleOrDefault(x =>
                    x.Parent.Id == versusData.Parent.Player.Id && x.OpponentId == t.Player.Id);
                item.Killed += versusData.Killed;
                item.KilledBy += versusData.KilledBy;
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void CreateUpdatePlayerVersus(XElement xml, ICollection<MatchPlayer> matchPlayers)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach (var matchPlayer in matchPlayers)
            {
                matchPlayer.Versus = new List<MatchPlayerVersus>();
                var playerNode = xml.Descendants("Players").Descendants("Player")
                    .Where(x => x.GetValue("Name") == matchPlayer.Player.Name);
                foreach (var opNode in playerNode.Descendants("Versus").Descendants("Opponent"))
                {
                    var item = new MatchPlayerVersus();
                    item.Parent = matchPlayer;
                    item.OpponentId =
                        matchPlayers.Single(x => x.Player.Name.ToLower() == opNode.Attribute("name").Value.ToLower()).Id;
                    item.Killed = opNode.GetValueI("Killed");
                    item.KilledBy = opNode.GetValueI("Killed_By");
                    UpdatePlayerVersus(item);
                    matchPlayer.Versus.Add(item);
                }

                context.Entry(matchPlayer).State = EntityState.Modified;
                context.SaveChanges();
            }
            context.ChangeTracker.AutoDetectChangesEnabled = true;
            context.SaveChanges(true);
        }
    }
}

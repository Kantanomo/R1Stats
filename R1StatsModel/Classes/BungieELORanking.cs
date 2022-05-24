using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Extensions;
using R1StatsModel.Model;

namespace R1StatsModel.Classes
{
    public static class BungieELORanking
    {
        private static DataTable AllowedRankDiff = new DataTable
        {
            Columns =
            {
                new DataColumn("Level", typeof(int)),
                new DataColumn("Lowest Level", typeof(int)),
                new DataColumn("Highest Level", typeof(int))
            },
            Rows =
            {
                {1, 1, 11}, {2, 1, 11}, {3, 1, 11}, {4, 1, 11}, {5, 1, 11}, {6, 1, 11}, {7, 1, 13}, {8, 1, 14},
                {9, 1, 15}, {10, 1, 16}, {11, 1, 17}, {12, 7, 18}, {13, 7, 19}, {14, 8, 20}, {15, 9, 21}, {16, 10, 22},
                {17, 11, 24}, {18, 12, 25}, {19, 13, 26}, {20, 14, 27}, {21, 15, 28}, {22, 16, 29}, {23, 17, 30},
                {24, 17, 31}, {25, 18, 32}, {26, 19, 33}, {27, 20, 35}, {28, 21, 36}, {29, 22, 37}, {30, 23, 39},
                {31, 24, 41}, {32, 25, 43}, {33, 26, 45}, {34, 27, 47}, {35, 27, 49}, {36, 28, 50}, {37, 29, 50},
                {38, 30, 50}, {39, 30, 50}, {40, 31, 50}, {41, 31, 50}, {42, 32, 50}, {43, 32, 50}, {44, 33, 50},
                {45, 33, 50}, {46, 34, 50}, {47, 34, 50}, {48, 35, 50}, {49, 35, 50}, {50, 36, 50},
            }
        };

        private static DataTable LevelDifferenceXPTable = new DataTable
        {
            Columns =
            {
                new DataColumn("Level Difference", typeof(int)),
                new DataColumn("Higher Win", typeof(int)),
                new DataColumn("Higher Loss", typeof(int)),
                new DataColumn("Lower Win", typeof(int)),
                new DataColumn("Lower Loss", typeof(int))
            },
            Rows =
            {
                {0, 100, 100, 100, 100}, {1, 92, 108, 108, 92}, {2, 85, 115, 115, 85}, {3, 79, 121, 121, 79},
                {4, 74, 126, 126, 74}, {5, 70, 130, 130, 70}, {6, 66, 134, 134, 66}, {7, 63, 137, 137, 63},
                {8, 60, 140, 140, 60}, {9, 58, 142, 142, 58}, {10, 56, 144, 144, 56}, {11, 54, 146, 146, 54},
                {12, 53, 147, 147, 53}, {13, 52, 148, 148, 52}, {14, 51, 149, 149, 51}, {15, 50, 150, 150, 50},
            },
        };

        private static DataTable WinLossFactorTable = new DataTable
        {
            Columns =
            {
                new DataColumn("Level", typeof(int)),
                new DataColumn("Win Factor", typeof(float)),
                new DataColumn("Loss Factor", typeof(float))
            },
            Rows =
            {
                {1, 1, 0}, {2, 1, 0.025}, {3, 1, 0.05}, {4, 1, 0.075}, {5, 1, 0.1}, {6, 1, 0.15}, {7, 1, 0.2},
                {8, 1, 0.275}, {9, 1, 0.35}, {10, 1, 0.4}, {11, 1, 0.45}, {12, 1, 0.5}, {13, 1, 0.55}, {14, 1, 0.575},
                {15, 1, 0.6}, {16, 1, 0.625}, {17, 1, 0.65}, {18, 1, 0.675}, {19, 1, 0.7}, {20, 1, 0.725},
                {21, 1, 0.75}, {22, 1, 0.775}, {23, 1, 0.8}, {24, 1, 0.825}, {25, 1, 0.85}, {26, 1, 0.875},
                {27, 1, 0.9}, {28, 1, 0.925}, {29, 1, 0.95}, {30, 1, 0.975}, {31, 1, 1}, {32, 1, 1}, {33, 1, 1},
                {34, 1, 1}, {35, 1, 1}, {36, 1, 1}, {37, 1, 1}, {38, 1, 1}, {39, 1, 1}, {40, 1, 1}, {41, 1, 1},
                {42, 0.95, 1}, {43, 0.9, 1}, {44, 0.85, 1}, {45, 0.8, 1}, {46, 0.7, 1}, {47, 0.65, 1}, {48, 0.6, 1},
                {49, 0.55, 1}, {50, 0.5, 1},
            },
        };

        public static void Init()
        {
            AllowedRankDiff.PrimaryKey = new[] {AllowedRankDiff.Columns["Level"]}!;
            LevelDifferenceXPTable.PrimaryKey = new[] {LevelDifferenceXPTable.Columns["Level Difference"]}!;
            WinLossFactorTable.PrimaryKey = new[] {WinLossFactorTable.Columns["Level"]}!;
        }

        public static int XPToRank(float XP)
        {
            int Base = 0;
            switch (XP)
            {
                case < 100:
                    Base = 0;
                    break;
                case < 1399:
                    Base = (int)MathF.Floor(XP / 100);
                    break;
                case < 2000 and >= 1399:
                    Base = 12 + (int)MathF.Floor((XP - 1399) / 200);
                    break;
                case >= 2000:
                {
                    Base = 16 + (int)MathF.Floor((XP - 2000) / 250);
                    if (Base >= 49){
                        Base = 49;
                    }

                    break;
                }
            }
            return Base;
        }

        public static float XPToNextRank(float XP)
        {
            float res = 0;
            switch (XP)
            {
                case < 100:
                    res = 100 - XP;
                    break;
                case < 1399:
                    var t = XPToRank(XP) * 100;
                    break;
            }

            return res;
        }
        public static Dictionary<int, float> CalculateXP(List<MatchPlayer> Players)
        {
            //Generate a base table with all 0
            var XP = 
                Players.ToDictionary<MatchPlayer?, int, float>(
                    matchPlayer => matchPlayer.Player.Id,
                    _ => 0);
            foreach (var item in Players)
            {
                var iRank = item.Player.Rank;
                var iPlace = item.Place;
                foreach (var comparison in Players.Where(x => x.Player.Id != item.Player.Id && x.Team != item.Team))
                {
                    var cRank = comparison.Player.Rank;
                    var cPlace = comparison.Place;
                    var LevelDifference = Math.Abs(iRank - cRank);
                    var ComputedDifference = 0;

                    //Used to computed level differences outside of original matchmaking specifications
                    if (AllowedRankDiff.GetValue<int>(iRank, "Lowest Level") > cRank)
                    {
                        LevelDifference = AllowedRankDiff.GetValue<int>(iRank, "Lowest Level");
                        ComputedDifference = Math.Abs(iRank - LevelDifference);
                    }
                    else if(AllowedRankDiff.GetValue<int>(iRank, "Highest Level") < cRank)
                    {
                        LevelDifference = AllowedRankDiff.GetValue<int>(iRank, "Highest Level");
                        ComputedDifference = Math.Abs(iRank - LevelDifference);
                    }
                    else
                    {
                        ComputedDifference = LevelDifference;
                    }

                    if (iRank >= cRank)
                    {
                        if (iPlace <= cPlace)
                        {
                            var XPGain = LevelDifferenceXPTable.GetValue<int>(ComputedDifference, "Higher Win");
                            var Factor = WinLossFactorTable.GetValue<float>(iRank, "Win Factor");
                            XP[item.Player.Id] += XPGain * Factor;
                        }
                        else
                        {
                            var XPGain = LevelDifferenceXPTable.GetValue<int>(ComputedDifference, "Higher Loss");
                            var Factor = WinLossFactorTable.GetValue<float>(iRank, "Loss Factor");
                            XP[item.Player.Id] -= XPGain * Factor;
                        }
                    }
                    else
                    {
                        if (iPlace <= cPlace)
                        {
                            var XPGain = LevelDifferenceXPTable.GetValue<int>(ComputedDifference, "Lower Win");
                            var Factor = WinLossFactorTable.GetValue<float>(iRank, "Win Factor");
                            XP[item.Player.Id] += XPGain * Factor;
                        }
                        else
                        {
                            var XPGain = LevelDifferenceXPTable.GetValue<int>(ComputedDifference, "Lower Loss");
                            var Factor = WinLossFactorTable.GetValue<float>(iRank, "Loss Factor");
                            XP[item.Player.Id] -= XPGain * Factor;
                        }
                    }
                }
                XP[item.Player.Id] /= Players.Count(x => x.Team == item.Team);
            }
            return XP;
        }
    }
}


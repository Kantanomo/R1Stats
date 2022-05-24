using System;
using System.Collections;
using System.Net.Mime;
using System.IO;
using System.IO;
using System.Runtime;
using System.Xml.Linq;
using R1StatsModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using R1StatsModel.Enum;
using R1StatsModel.Extensions;
using R1StatsModel.Factory;
using R1StatsModel.Model;

namespace Importer // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static MatchFactory _matchFactory;
        public static string LaunchPath =>
            @"C:\Users\mhatt\source\repos\R1Stats\Importer\bin\Debug\net6.0";

        public static string StatsPath =>
            Path.Combine(LaunchPath, "Stats");

        private static Dictionary<string, object> GetXmlData(XElement xml)
        {
            var attr = xml.Attributes().ToDictionary(d => d.Name.LocalName, d => (object)d.Value);
            if (xml.HasElements) attr.Add("_value", xml.Elements().Select(e => GetXmlData(e)));
            else if (!xml.IsEmpty) attr.Add("_value", xml.Value);

            return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        static void Main(string[] args)
        {

            /* var statsContext = new StatsContext();

             _matchFactory = new MatchFactory(statsContext);
             var sFile = Path.Combine(StatsPath, "match_20220411-225340.xml");

             var xml = XElement.Load(sFile);

             var game = xml.Descendants("Game");


             var match = _matchFactory.CreateMatch(xml);
             var matchPlayers =
                 xml.Descendants("Players").Descendants("Player")
                     .Select(playerNode =>
                         _matchFactory.CreateUpdatePlayer(playerNode, match)
                     )
                     .ToList();

             _matchFactory.CreateUpdatePlayerVersus(xml, matchPlayers);
             */

            foreach (var file in Directory.GetFiles(StatsPath))
            {
                //var sFile = Path.Combine(StatsPath, "match_20210904-231511.xml");
                using var statsContext = new StatsContext();
                
                _matchFactory = new MatchFactory(statsContext);
                try
                {
                    var xml = XElement.Load(file);
                    var game = xml.Descendants("Game");
                    var StartTime = DateTime.Parse(game.Descendants("Start_Time").First().Value.Replace("UTC", "Z"));
                    var EndTime = DateTime.Parse(game.Descendants("End_Time").First().Value.Replace("UTC", "Z"));
                    var playerCount = xml.Descendants("Players").Descendants("Player").Count();
                    //For checking if there was winners and losers
                    var Placements = xml.Descendants("Teams").Descendants("Team").Select(x => x.GetValue("Place")).Count(x => x == "1st");
                    var Scores = xml.Descendants("Teams").Descendants("Team").Select(x => x.GetValueI("Score")).Sum();
                    var hash = CalculateMD5(file);
                    if (statsContext.Matches.SingleOrDefault(x => x.Hash == hash) == null)
                    {
                        if (EndTime - StartTime > TimeSpan.FromMinutes(3) && playerCount >= 8 && Placements == 1 &&
                            Scores != 0)
                        {
                            var match = _matchFactory.CreateMatch(xml);
                            if (match != null)
                            {
                                Console.WriteLine($"{file} true");
                                match.Hash = hash;
                                statsContext.SaveChanges(true);
                                var matchPlayers =
                                    xml.Descendants("Players").Descendants("Player")
                                        .Select(playerNode =>
                                            _matchFactory.CreateUpdatePlayer(playerNode, match)
                                        )
                                        .ToList();
                                _matchFactory.CreateUpdatePlayerVersus(xml, matchPlayers);
                                _matchFactory.UpdatePlayerXP(matchPlayers);
                                statsContext.SaveChanges(true);
                            }
                            else
                            {
                                Console.WriteLine($"{file} Invalid xml");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{file} false");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{file} Skipping");
                    }
                }
                catch (Exception e)
                {
                    if(e.Source == "System.Private.Xml")
                        Console.WriteLine($"{file} Invalid Chars");
                    else
                    {
                        throw e;
                    }
                    // ignored
                }
            }
           
            while (true)
                Thread.Sleep(100);
        }
    }
}
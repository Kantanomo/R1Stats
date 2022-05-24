using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using R1StatsModel.Model;
using System.Configuration;
using R1StatsModel.Classes;

namespace R1StatsModel
{
    public class StatsContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchTeam> MatchesTeam { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<MatchPlayerVersus> MatchPlayersVersus { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerGameType> PlayerGameTypes { get; set; }
        public DbSet<PlayerVersus> PlayerVersus { get; set; }


        private string DbPath { get; set; }
        public StatsContext()
        {
            BungieELORanking.Init();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.LogTo(Console.WriteLine);
            options.UseSqlServer(
                @"Data Source=localhost;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;database=stats");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .Property(p => p.KDA)
                .HasColumnType("Float")
                .HasComputedColumnSql(
                    "CASE WHEN [Deaths] > 0 THEN (CAST([Kills] + [Assists] as FLOAT)/CAST([Deaths] as FLOAT)) ELSE [Kills] + [Assists] END");
            
            modelBuilder.Entity<MatchPlayer>()
                .Property(p => p.KDA)
                .HasColumnType("Float")
                .HasComputedColumnSql(
                    "CASE WHEN [Deaths] > 0 THEN (CAST([Kills] + [Assists] as FLOAT)/CAST([Deaths] as FLOAT)) ELSE [Kills] + [Assists] END");

            modelBuilder.Entity<Player>()
                .Property(p => p.HitPercentage)
                .HasColumnType("Float")
                .HasComputedColumnSql(
                    "CASE WHEN [ShotsFired] > 0 AND [ShotsHit] > 0 THEN (CAST([ShotsHit] as FLOAT)/CAST([ShotsFired] as FLOAT)) ELSE 0 END");

            modelBuilder.Entity<MatchPlayer>()
                .Property(p => p.HitPercentage)
                .HasColumnType("Float")
                .HasComputedColumnSql(
                    "CASE WHEN [ShotsFired] > 0 AND [ShotsHit] > 0 THEN (CAST([ShotsHit] as FLOAT)/CAST([ShotsFired] as FLOAT)) ELSE 0 END");

            modelBuilder.Entity<Player>()
                .Property(p => p.KDARatio)
                .HasComputedColumnSql("[Kills] - [Deaths]");

            modelBuilder.Entity<MatchPlayer>()
                .Property(p => p.KDARatio)
                .HasComputedColumnSql("[Kills] - [Deaths]");

            modelBuilder.Entity<Player>()
                .Property(p => p.WinLossRatio)
                .HasColumnType("Float")
                .HasComputedColumnSql(
                    "CASE WHEN [Losses] != 0 THEN (CAST([Wins] as FLOAT)/CAST([Losses] as FLOAT)) ELSE [Wins] END");
        }
    }
}
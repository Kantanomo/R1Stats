// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using R1StatsModel;

#nullable disable

namespace R1StatsModel.Migrations
{
    [DbContext(typeof(StatsContext))]
    partial class StatsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("R1StatsModel.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameType")
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Playlist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Variant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("AverageLife")
                        .HasColumnType("int");

                    b.Property<int>("BallKills")
                        .HasColumnType("int");

                    b.Property<int>("BestSpree")
                        .HasColumnType("int");

                    b.Property<int>("BombGrabs")
                        .HasColumnType("int");

                    b.Property<int>("BomberKills")
                        .HasColumnType("int");

                    b.Property<int>("CarrierKills")
                        .HasColumnType("int");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("FlagSaves")
                        .HasColumnType("int");

                    b.Property<int>("FlagSteals")
                        .HasColumnType("int");

                    b.Property<int>("HeadShots")
                        .HasColumnType("int");

                    b.Property<double>("HitPercentage")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Float")
                        .HasComputedColumnSql("CASE WHEN [ShotsFired] > 0 AND [ShotsHit] > 0 THEN (CAST([ShotsHit] as FLOAT)/CAST([ShotsFired] as FLOAT)) ELSE 0 END");

                    b.Property<double>("KDA")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Float")
                        .HasComputedColumnSql("CASE WHEN [Deaths] > 0 THEN (CAST([Kills] + [Assists] as FLOAT)/CAST([Deaths] as FLOAT)) ELSE [Kills] + [Assists] END");

                    b.Property<int>("KDARatio")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("[Kills] - [Deaths]");

                    b.Property<int>("Kills")
                        .HasColumnType("int");

                    b.Property<int>("KillsAsKing")
                        .HasColumnType("int");

                    b.Property<int>("KingsKilled")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("Place")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("ShotsFired")
                        .HasColumnType("int");

                    b.Property<int>("ShotsHit")
                        .HasColumnType("int");

                    b.Property<int>("Suicides")
                        .HasColumnType("int");

                    b.Property<int>("Team")
                        .HasColumnType("int");

                    b.Property<int>("TotalMedals")
                        .HasColumnType("int");

                    b.Property<float>("XP")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchPlayers");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchPlayerVersus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Killed")
                        .HasColumnType("int");

                    b.Property<int>("KilledBy")
                        .HasColumnType("int");

                    b.Property<int>("OpponentId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("MatchPlayersVersus");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("MatchId")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Score")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Team")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchesTeam");
                });

            modelBuilder.Entity("R1StatsModel.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("AverageLife")
                        .HasColumnType("int");

                    b.Property<int>("BallKills")
                        .HasColumnType("int");

                    b.Property<int>("BestSpree")
                        .HasColumnType("int");

                    b.Property<int>("BombGrabs")
                        .HasColumnType("int");

                    b.Property<int>("BomberKills")
                        .HasColumnType("int");

                    b.Property<int>("CarrierKills")
                        .HasColumnType("int");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("FlagSaves")
                        .HasColumnType("int");

                    b.Property<int>("FlagSteals")
                        .HasColumnType("int");

                    b.Property<int>("HeadShots")
                        .HasColumnType("int");

                    b.Property<double>("HitPercentage")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Float")
                        .HasComputedColumnSql("CASE WHEN [ShotsFired] > 0 AND [ShotsHit] > 0 THEN (CAST([ShotsHit] as FLOAT)/CAST([ShotsFired] as FLOAT)) ELSE 0 END");

                    b.Property<double>("KDA")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Float")
                        .HasComputedColumnSql("CASE WHEN [Deaths] > 0 THEN (CAST([Kills] + [Assists] as FLOAT)/CAST([Deaths] as FLOAT)) ELSE [Kills] + [Assists] END");

                    b.Property<int>("KDARatio")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("[Kills] - [Deaths]");

                    b.Property<int>("Kills")
                        .HasColumnType("int");

                    b.Property<int>("KillsAsKing")
                        .HasColumnType("int");

                    b.Property<int>("KingsKilled")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("ShotsFired")
                        .HasColumnType("int");

                    b.Property<int>("ShotsHit")
                        .HasColumnType("int");

                    b.Property<int>("Suicides")
                        .HasColumnType("int");

                    b.Property<int>("TotalMedals")
                        .HasColumnType("int");

                    b.Property<double>("WinLossRatio")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Float")
                        .HasComputedColumnSql("CASE WHEN [Losses] != 0 THEN (CAST([Wins] as FLOAT)/CAST([Losses] as FLOAT)) ELSE [Wins] END");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.Property<float>("XP")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("R1StatsModel.Model.PlayerGameType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<int>("GameType")
                        .HasColumnType("int");

                    b.Property<int>("HeadShots")
                        .HasColumnType("int");

                    b.Property<int>("Kills")
                        .HasColumnType("int");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("ShotsFired")
                        .HasColumnType("int");

                    b.Property<int>("ShotsHit")
                        .HasColumnType("int");

                    b.Property<int>("Suicides")
                        .HasColumnType("int");

                    b.Property<int>("TotalMedals")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerGameTypes");
                });

            modelBuilder.Entity("R1StatsModel.Model.PlayerVersus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Killed")
                        .HasColumnType("int");

                    b.Property<int>("KilledBy")
                        .HasColumnType("int");

                    b.Property<int>("OpponentId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("PlayerVersus");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchPlayer", b =>
                {
                    b.HasOne("R1StatsModel.Model.Match", "Match")
                        .WithMany("Players")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("R1StatsModel.Model.Player", "Player")
                        .WithMany("MatchHistroy")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchPlayerVersus", b =>
                {
                    b.HasOne("R1StatsModel.Model.MatchPlayer", "Parent")
                        .WithMany("Versus")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchTeam", b =>
                {
                    b.HasOne("R1StatsModel.Model.Match", null)
                        .WithMany("Teams")
                        .HasForeignKey("MatchId");
                });

            modelBuilder.Entity("R1StatsModel.Model.PlayerGameType", b =>
                {
                    b.HasOne("R1StatsModel.Model.Player", null)
                        .WithMany("GameTypeStats")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("R1StatsModel.Model.PlayerVersus", b =>
                {
                    b.HasOne("R1StatsModel.Model.Player", "Parent")
                        .WithMany("Versus")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("R1StatsModel.Model.Match", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("R1StatsModel.Model.MatchPlayer", b =>
                {
                    b.Navigation("Versus");
                });

            modelBuilder.Entity("R1StatsModel.Model.Player", b =>
                {
                    b.Navigation("GameTypeStats");

                    b.Navigation("MatchHistroy");

                    b.Navigation("Versus");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R1StatsModel.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Playlist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Map = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameType = table.Column<int>(type: "int", nullable: false),
                    Variant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Suicides = table.Column<int>(type: "int", nullable: false),
                    ShotsHit = table.Column<int>(type: "int", nullable: false),
                    ShotsFired = table.Column<int>(type: "int", nullable: false),
                    HeadShots = table.Column<int>(type: "int", nullable: false),
                    TotalMedals = table.Column<int>(type: "int", nullable: false),
                    CarrierKills = table.Column<int>(type: "int", nullable: false),
                    BallKills = table.Column<int>(type: "int", nullable: false),
                    BombGrabs = table.Column<int>(type: "int", nullable: false),
                    BomberKills = table.Column<int>(type: "int", nullable: false),
                    FlagSaves = table.Column<int>(type: "int", nullable: false),
                    FlagSteals = table.Column<int>(type: "int", nullable: false),
                    AverageLife = table.Column<int>(type: "int", nullable: false),
                    BestSpree = table.Column<int>(type: "int", nullable: false),
                    KingsKilled = table.Column<int>(type: "int", nullable: false),
                    KillsAsKing = table.Column<int>(type: "int", nullable: false),
                    XP = table.Column<float>(type: "real", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    HitPercentage = table.Column<double>(type: "Float", nullable: false, computedColumnSql: "CASE WHEN [ShotsFired] > 0 AND [ShotsHit] > 0 THEN (CAST([ShotsHit] as FLOAT)/CAST([ShotsFired] as FLOAT)) ELSE 0 END"),
                    KDA = table.Column<double>(type: "Float", nullable: false, computedColumnSql: "CASE WHEN [Deaths] > 0 THEN (CAST([Kills] + [Assists] as FLOAT)/CAST([Deaths] as FLOAT)) ELSE [Kills] + [Assists] END"),
                    KDARatio = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[Kills] - [Deaths]"),
                    WinLossRatio = table.Column<double>(type: "Float", nullable: false, computedColumnSql: "CASE WHEN [Losses] != 0 THEN (CAST([Wins] as FLOAT)/CAST([Losses] as FLOAT)) ELSE [Wins] END")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchesTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchesTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchesTeam_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Suicides = table.Column<int>(type: "int", nullable: false),
                    ShotsHit = table.Column<int>(type: "int", nullable: false),
                    ShotsFired = table.Column<int>(type: "int", nullable: false),
                    HeadShots = table.Column<int>(type: "int", nullable: false),
                    TotalMedals = table.Column<int>(type: "int", nullable: false),
                    CarrierKills = table.Column<int>(type: "int", nullable: false),
                    BallKills = table.Column<int>(type: "int", nullable: false),
                    BombGrabs = table.Column<int>(type: "int", nullable: false),
                    BomberKills = table.Column<int>(type: "int", nullable: false),
                    FlagSaves = table.Column<int>(type: "int", nullable: false),
                    FlagSteals = table.Column<int>(type: "int", nullable: false),
                    AverageLife = table.Column<int>(type: "int", nullable: false),
                    BestSpree = table.Column<int>(type: "int", nullable: false),
                    KingsKilled = table.Column<int>(type: "int", nullable: false),
                    KillsAsKing = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    XP = table.Column<float>(type: "real", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    HitPercentage = table.Column<double>(type: "Float", nullable: false, computedColumnSql: "CASE WHEN [ShotsFired] > 0 AND [ShotsHit] > 0 THEN (CAST([ShotsHit] as FLOAT)/CAST([ShotsFired] as FLOAT)) ELSE 0 END"),
                    KDA = table.Column<double>(type: "Float", nullable: false, computedColumnSql: "CASE WHEN [Deaths] > 0 THEN (CAST([Kills] + [Assists] as FLOAT)/CAST([Deaths] as FLOAT)) ELSE [Kills] + [Assists] END"),
                    KDARatio = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[Kills] - [Deaths]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerGameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameType = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    Suicides = table.Column<int>(type: "int", nullable: false),
                    ShotsHit = table.Column<int>(type: "int", nullable: false),
                    ShotsFired = table.Column<int>(type: "int", nullable: false),
                    HeadShots = table.Column<int>(type: "int", nullable: false),
                    TotalMedals = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGameTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerGameTypes_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerVersus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    OpponentId = table.Column<int>(type: "int", nullable: false),
                    Killed = table.Column<int>(type: "int", nullable: false),
                    KilledBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerVersus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerVersus_Players_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayersVersus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    OpponentId = table.Column<int>(type: "int", nullable: false),
                    Killed = table.Column<int>(type: "int", nullable: false),
                    KilledBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayersVersus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayersVersus_MatchPlayers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MatchPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchesTeam_MatchId",
                table: "MatchesTeam",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_MatchId",
                table: "MatchPlayers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_PlayerId",
                table: "MatchPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersVersus_ParentId",
                table: "MatchPlayersVersus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGameTypes_PlayerId",
                table: "PlayerGameTypes",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerVersus_ParentId",
                table: "PlayerVersus",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchesTeam");

            migrationBuilder.DropTable(
                name: "MatchPlayersVersus");

            migrationBuilder.DropTable(
                name: "PlayerGameTypes");

            migrationBuilder.DropTable(
                name: "PlayerVersus");

            migrationBuilder.DropTable(
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test_APBD_2.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Racers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LengthInKm = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackRaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    BestTimeInSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackRaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackRaces_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackRaces_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceParticipations",
                columns: table => new
                {
                    TrackRaceId = table.Column<int>(type: "int", nullable: false),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    FinishTimeInSeconds = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceParticipations", x => new { x.TrackRaceId, x.RacerId });
                    table.ForeignKey(
                        name: "FK_RaceParticipations_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceParticipations_TrackRaces_TrackRaceId",
                        column: x => x.TrackRaceId,
                        principalTable: "TrackRaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Racers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "Elvis" },
                    { 2, "Lewis", "Hamilton" },
                    { 3, "Max", "Verstappen" },
                    { 4, "Mister", "NoParticipation" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "Date", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silverstone, 9", "British Cup" },
                    { 2, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Madrit, 4", "Spanish Cup" }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "LengthInKm", "Name" },
                values: new object[,]
                {
                    { 1, 2.9m, "Silverstone Circuit" },
                    { 2, 5.3m, "Spanish Circuit" }
                });

            migrationBuilder.InsertData(
                table: "TrackRaces",
                columns: new[] { "Id", "BestTimeInSeconds", "Laps", "RaceId", "TrackId" },
                values: new object[,]
                {
                    { 1, 6000, 50, 1, 1 },
                    { 2, 4567, 100, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "RaceParticipations",
                columns: new[] { "RacerId", "TrackRaceId", "FinishTimeInSeconds", "Position" },
                values: new object[,]
                {
                    { 1, 1, 7000, 1 },
                    { 2, 1, 6500, 2 },
                    { 3, 1, 6400, 3 },
                    { 1, 2, 8900, 1 },
                    { 2, 2, 7770, 2 },
                    { 3, 2, 8000, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaceParticipations_RacerId",
                table: "RaceParticipations",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackRaces_RaceId",
                table: "TrackRaces",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackRaces_TrackId",
                table: "TrackRaces",
                column: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceParticipations");

            migrationBuilder.DropTable(
                name: "Racers");

            migrationBuilder.DropTable(
                name: "TrackRaces");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}

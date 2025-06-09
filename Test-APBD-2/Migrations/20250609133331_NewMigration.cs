using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_APBD_2.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TrackRaces",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RaceId", "TrackId" },
                values: new object[] { 2, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TrackRaces",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RaceId", "TrackId" },
                values: new object[] { 1, 1 });
        }
    }
}

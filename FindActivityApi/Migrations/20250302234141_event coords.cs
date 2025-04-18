using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindActivityApi.Migrations
{
    /// <inheritdoc />
    public partial class eventcoords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LatitudeX",
                table: "Evnts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LongitudeY",
                table: "Evnts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatitudeX",
                table: "Evnts");

            migrationBuilder.DropColumn(
                name: "LongitudeY",
                table: "Evnts");
        }
    }
}

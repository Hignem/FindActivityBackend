using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindActivityApi.Migrations
{
    /// <inheritdoc />
    public partial class imagemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureBase64",
                table: "Users",
                newName: "ProfileImagePath");

            migrationBuilder.AddColumn<string>(
                name: "EvntImagePath",
                table: "Evnts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvntImagePath",
                table: "Evnts");

            migrationBuilder.RenameColumn(
                name: "ProfileImagePath",
                table: "Users",
                newName: "ProfilePictureBase64");
        }
    }
}

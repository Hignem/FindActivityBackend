using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FindActivityApi.Migrations
{
    /// <inheritdoc />
    public partial class likesandeventmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvntGoing",
                columns: table => new
                {
                    EvntGoingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvntId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvntGoing", x => x.EvntGoingId);
                    table.ForeignKey(
                        name: "FK_EvntGoing_Evnts_EvntId",
                        column: x => x.EvntId,
                        principalTable: "Evnts",
                        principalColumn: "EvntId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvntGoing_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvntLikes",
                columns: table => new
                {
                    EvntLikesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvntId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvntLikes", x => x.EvntLikesId);
                    table.ForeignKey(
                        name: "FK_EvntLikes_Evnts_EvntId",
                        column: x => x.EvntId,
                        principalTable: "Evnts",
                        principalColumn: "EvntId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvntLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvntGoing_EvntId",
                table: "EvntGoing",
                column: "EvntId");

            migrationBuilder.CreateIndex(
                name: "IX_EvntGoing_UserId",
                table: "EvntGoing",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EvntLikes_EvntId",
                table: "EvntLikes",
                column: "EvntId");

            migrationBuilder.CreateIndex(
                name: "IX_EvntLikes_UserId",
                table: "EvntLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvntGoing");

            migrationBuilder.DropTable(
                name: "EvntLikes");
        }
    }
}

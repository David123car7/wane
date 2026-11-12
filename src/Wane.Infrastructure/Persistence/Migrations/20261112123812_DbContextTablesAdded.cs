using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wane.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DbContextTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "tracks",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_tracks_Tittle",
                table: "tracks",
                newName: "IX_tracks_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "tracks",
                newName: "Tittle");

            migrationBuilder.RenameIndex(
                name: "IX_tracks_Title",
                table: "tracks",
                newName: "IX_tracks_Tittle");
        }
    }
}

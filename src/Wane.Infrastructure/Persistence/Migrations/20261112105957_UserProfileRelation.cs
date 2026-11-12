using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wane.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_profiles_UserId",
                table: "profiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_profiles_users_UserId",
                table: "profiles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_users_UserId",
                table: "profiles");

            migrationBuilder.DropIndex(
                name: "IX_profiles_UserId",
                table: "profiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "profiles");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luma.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLoggedInAtToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_logged_in_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_logged_in_at",
                table: "users");
        }
    }
}

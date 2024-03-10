using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InDuckTor.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveSomePropertiesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InactiveAt",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "user",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "InactiveAt",
                schema: "user",
                table: "Clients");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "user",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InactiveAt",
                schema: "user",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlackList_UserId",
                schema: "user",
                table: "BlackList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlackList_Users_UserId",
                schema: "user",
                table: "BlackList",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackList_Users_UserId",
                schema: "user",
                table: "BlackList");

            migrationBuilder.DropIndex(
                name: "IX_BlackList_UserId",
                schema: "user",
                table: "BlackList");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InactiveAt",
                schema: "user",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "user",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InactiveAt",
                schema: "user",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "user",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InactiveAt",
                schema: "user",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}

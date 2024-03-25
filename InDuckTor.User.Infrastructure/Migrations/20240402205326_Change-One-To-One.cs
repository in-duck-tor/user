using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InDuckTor.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClientId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "user",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                schema: "user",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "user",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientId",
                schema: "user",
                table: "Users",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                schema: "user",
                table: "Users",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientId",
                schema: "user",
                table: "Users",
                column: "ClientId",
                principalSchema: "user",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                schema: "user",
                table: "Users",
                column: "EmployeeId",
                principalSchema: "user",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}

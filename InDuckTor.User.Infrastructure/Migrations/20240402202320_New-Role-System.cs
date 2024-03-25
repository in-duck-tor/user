using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InDuckTor.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewRoleSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePermission",
                schema: "user");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                schema: "user",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "user",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "user",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "user",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "user",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                schema: "user",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                schema: "user",
                table: "Users",
                newName: "LastName");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "user",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                schema: "user",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "user",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "user",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "user",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                schema: "user",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "user",
                table: "Permissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "user",
                table: "Permissions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_EmployeeId",
                schema: "user",
                table: "Permissions",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Employees_EmployeeId",
                schema: "user",
                table: "Permissions",
                column: "EmployeeId",
                principalSchema: "user",
                principalTable: "Employees",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Employees_EmployeeId",
                schema: "user",
                table: "Permissions");

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

            migrationBuilder.DropIndex(
                name: "IX_Permissions_EmployeeId",
                schema: "user",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "user",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "user",
                table: "Permissions");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "user",
                table: "Users",
                newName: "AccountType");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "user",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "user",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "user",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "user",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                schema: "user",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "user",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "user",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "user",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "user",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                schema: "user",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeePermission",
                schema: "user",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionsKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePermission", x => new { x.EmployeesId, x.PermissionsKey });
                    table.ForeignKey(
                        name: "FK_EmployeePermission_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalSchema: "user",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePermission_Permissions_PermissionsKey",
                        column: x => x.PermissionsKey,
                        principalSchema: "user",
                        principalTable: "Permissions",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermission_PermissionsKey",
                schema: "user",
                table: "EmployeePermission",
                column: "PermissionsKey");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InDuckTor.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Employees_EmployeeId",
                schema: "user",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_EmployeeId",
                schema: "user",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "user",
                table: "Permissions");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePermission",
                schema: "user");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "user",
                table: "Permissions",
                type: "bigint",
                nullable: true);

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
        }
    }
}

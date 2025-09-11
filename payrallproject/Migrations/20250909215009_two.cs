using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOvertimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeId = table.Column<int>(type: "int", nullable: true),
                    OTId = table.Column<int>(type: "int", nullable: true),
                    DateWorked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoursWorked = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOvertimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeOvertimes_User_EmployeId",
                        column: x => x.EmployeId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeOvertimes_User_OTId",
                        column: x => x.OTId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeCategoriesId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_EmployeeCategories_EmployeeCategoriesId",
                        column: x => x.EmployeeCategoriesId,
                        principalTable: "EmployeeCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeeCategoriesId",
                table: "Departments",
                column: "EmployeeCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOvertimes_EmployeId",
                table: "EmployeeOvertimes",
                column: "EmployeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOvertimes_OTId",
                table: "EmployeeOvertimes",
                column: "OTId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeOvertimes");

            migrationBuilder.DropTable(
                name: "EmployeeCategories");
        }
    }
}

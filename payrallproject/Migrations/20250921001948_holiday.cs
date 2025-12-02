using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class holiday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasicStationarySal",
                table: "SalaryReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DaySalary",
                table: "SalaryReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KpiRate",
                table: "SalaryReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NoPay",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "basicSala",
                table: "SalaryReports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    HolidayType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropColumn(
                name: "BasicStationarySal",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "DaySalary",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "KpiRate",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "NoPay",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "basicSala",
                table: "SalaryReports");
        }
    }
}

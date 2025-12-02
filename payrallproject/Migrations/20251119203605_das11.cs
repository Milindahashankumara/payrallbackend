using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class das11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AttendanceAllowance",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FoodAllowance",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InternetAllowance",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MedicalAllowance",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransportAllowance",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceAllowance",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "FoodAllowance",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "InternetAllowance",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "TransportAllowance",
                table: "SalaryReports");
        }
    }
}

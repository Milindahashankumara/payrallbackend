using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class salaryreportchanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bra1",
                table: "SalaryReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bra2",
                table: "SalaryReports",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bra1",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "Bra2",
                table: "SalaryReports");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class changedempovertim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOvertimes_OT_OTId",
                table: "EmployeeOvertimes");

            migrationBuilder.RenameColumn(
                name: "OTId",
                table: "EmployeeOvertimes",
                newName: "OtId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeOvertimes_OTId",
                table: "EmployeeOvertimes",
                newName: "IX_EmployeeOvertimes_OtId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOtPayment",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot2Payment",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot1Payment",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetSalary",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossSalary",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOvertimes_OT_OtId",
                table: "EmployeeOvertimes",
                column: "OtId",
                principalTable: "OT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOvertimes_OT_OtId",
                table: "EmployeeOvertimes");

            migrationBuilder.RenameColumn(
                name: "OtId",
                table: "EmployeeOvertimes",
                newName: "OTId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeOvertimes_OtId",
                table: "EmployeeOvertimes",
                newName: "IX_EmployeeOvertimes_OTId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalOtPayment",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot2Payment",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Ot1Payment",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetSalary",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossSalary",
                table: "SalaryReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOvertimes_OT_OTId",
                table: "EmployeeOvertimes",
                column: "OTId",
                principalTable: "OT",
                principalColumn: "Id");
        }
    }
}

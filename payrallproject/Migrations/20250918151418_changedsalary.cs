using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class changedsalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalOTPayment",
                table: "SalaryReports",
                newName: "TotalOtPayment");

            migrationBuilder.RenameColumn(
                name: "OT2Payment",
                table: "SalaryReports",
                newName: "Ot2Payment");

            migrationBuilder.RenameColumn(
                name: "OT2Hours",
                table: "SalaryReports",
                newName: "Ot2Hours");

            migrationBuilder.RenameColumn(
                name: "OT1Payment",
                table: "SalaryReports",
                newName: "Ot1Payment");

            migrationBuilder.RenameColumn(
                name: "OT1Hours",
                table: "SalaryReports",
                newName: "Ot1Hours");

            migrationBuilder.RenameColumn(
                name: "KPIAllowance",
                table: "SalaryReports",
                newName: "KpiAllowance");

            migrationBuilder.RenameColumn(
                name: "ETF",
                table: "SalaryReports",
                newName: "Etf");

            migrationBuilder.RenameColumn(
                name: "EPFLiableSalary",
                table: "SalaryReports",
                newName: "EpfLiableSalary");

            migrationBuilder.RenameColumn(
                name: "EPF2",
                table: "SalaryReports",
                newName: "Epf2");

            migrationBuilder.RenameColumn(
                name: "EPF1",
                table: "SalaryReports",
                newName: "Epf1");

            migrationBuilder.AddColumn<string>(
                name: "CategaryName",
                table: "SalaryReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "SalaryReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "SalaryReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                table: "SalaryReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "SalaryReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "SalaryReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "NoPayDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoPayDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoPayDay_Employe_EmployeID",
                        column: x => x.EmployeID,
                        principalTable: "Employe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoPayDay_EmployeID",
                table: "NoPayDay",
                column: "EmployeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoPayDay");

            migrationBuilder.DropColumn(
                name: "CategaryName",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "SalaryReports");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "SalaryReports");

            migrationBuilder.RenameColumn(
                name: "TotalOtPayment",
                table: "SalaryReports",
                newName: "TotalOTPayment");

            migrationBuilder.RenameColumn(
                name: "Ot2Payment",
                table: "SalaryReports",
                newName: "OT2Payment");

            migrationBuilder.RenameColumn(
                name: "Ot2Hours",
                table: "SalaryReports",
                newName: "OT2Hours");

            migrationBuilder.RenameColumn(
                name: "Ot1Payment",
                table: "SalaryReports",
                newName: "OT1Payment");

            migrationBuilder.RenameColumn(
                name: "Ot1Hours",
                table: "SalaryReports",
                newName: "OT1Hours");

            migrationBuilder.RenameColumn(
                name: "KpiAllowance",
                table: "SalaryReports",
                newName: "KPIAllowance");

            migrationBuilder.RenameColumn(
                name: "Etf",
                table: "SalaryReports",
                newName: "ETF");

            migrationBuilder.RenameColumn(
                name: "EpfLiableSalary",
                table: "SalaryReports",
                newName: "EPFLiableSalary");

            migrationBuilder.RenameColumn(
                name: "Epf2",
                table: "SalaryReports",
                newName: "EPF2");

            migrationBuilder.RenameColumn(
                name: "Epf1",
                table: "SalaryReports",
                newName: "EPF1");
        }
    }
}

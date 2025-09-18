using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payrallproject.Migrations
{
    /// <inheritdoc />
    public partial class changedtableemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KPIrate",
                table: "Employe",
                newName: "KpiRate");

            migrationBuilder.RenameColumn(
                name: "KPIamount",
                table: "Employe",
                newName: "KpiAmount");

            migrationBuilder.RenameColumn(
                name: "BRA2",
                table: "Employe",
                newName: "Bra2");

            migrationBuilder.RenameColumn(
                name: "BRA1",
                table: "Employe",
                newName: "Bra1");

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employe",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "HasTaxExemption",
                table: "Employe",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BankBranch",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KpiRate",
                table: "Employe",
                newName: "KPIrate");

            migrationBuilder.RenameColumn(
                name: "KpiAmount",
                table: "Employe",
                newName: "KPIamount");

            migrationBuilder.RenameColumn(
                name: "Bra2",
                table: "Employe",
                newName: "BRA2");

            migrationBuilder.RenameColumn(
                name: "Bra1",
                table: "Employe",
                newName: "BRA1");

            migrationBuilder.AlterColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasTaxExemption",
                table: "Employe",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankBranch",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNumber",
                table: "Employe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

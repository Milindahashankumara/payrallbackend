using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class SalaryReport
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeNumber { get; set; } = null!;

    public string CategaryName { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public int WorkingDays { get; set; }

    public decimal Incentives { get; set; }

    public decimal Bonus { get; set; }

    public decimal SalaryAdvances { get; set; }

    public decimal Loans { get; set; }

    public decimal OtherDeductions { get; set; }

    public int LeaveDays { get; set; }

    public int HalfDays { get; set; }

    public int NoPayDays { get; set; }

    public decimal Ot1Hours { get; set; }

    public decimal Ot2Hours { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public decimal AttendanceAllowance { get; set; }

    public decimal TransportAllowance { get; set; }

    public decimal FoodAllowance { get; set; }

    public decimal MedicalAllowance { get; set; }

    public decimal InternetAllowance { get; set; }

    public decimal Wages { get; set; }

    public decimal KpiAllowance { get; set; }

    public decimal? GrossSalary { get; set; }

    public decimal TotalDeductions { get; set; }

    public decimal? NetSalary { get; set; }

    public decimal EpfLiableSalary { get; set; }

    public decimal? Ot1Payment { get; set; }

    public decimal? Ot2Payment { get; set; }

    public decimal? TotalOtPayment { get; set; }

    public decimal Epf1 { get; set; }

    public decimal Epf2 { get; set; }

    public decimal Etf { get; set; }

    public decimal EmployeeContribution { get; set; }

    public bool IsDaySalaryBased { get; set; }

    public DateTime GeneratedOn { get; set; }

    public int? DaySalary { get; set; }

    public int? KpiRate { get; set; }

    public int? BasicStationarySal { get; set; }

    public int? BasicSala { get; set; }

    public decimal NoPay { get; set; }

    public int? Bra1 { get; set; }

    public int? Bra2 { get; set; }

    public virtual Employe Employee { get; set; } = null!;
}

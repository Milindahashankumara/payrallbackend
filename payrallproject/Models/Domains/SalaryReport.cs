using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class SalaryReport
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employe Employee { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string CategaryName { get; set; }
        public string DepartmentName { get; set; }

        // Input fields
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

        // Calculated fields
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
        public DateTime GeneratedOn { get; set; } = DateTime.UtcNow;
        public int? DaySalary { get; set; }
        public int? KpiRate { get; set; }
        public int? BasicStationarySal { get; set; }
        public int? basicSala { get; set; }
        public decimal NoPay { get; set; }
    }
}

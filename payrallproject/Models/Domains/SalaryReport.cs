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
        public decimal OT1Hours { get; set; }
        public decimal OT2Hours { get; set; }

        // Calculated fields
        public decimal Wages { get; set; }
        public decimal KPIAllowance { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
        public decimal EPFLiableSalary { get; set; }
        public decimal OT1Payment { get; set; }
        public decimal OT2Payment { get; set; }
        public decimal TotalOTPayment { get; set; }
        public decimal EPF1 { get; set; }
        public decimal EPF2 { get; set; }
        public decimal ETF { get; set; }
        public decimal EmployeeContribution { get; set; }
        public bool IsDaySalaryBased { get; set; }
        public DateTime GeneratedOn { get; set; } = DateTime.UtcNow;
    }
}

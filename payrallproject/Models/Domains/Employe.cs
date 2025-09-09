using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class Employe
    {
        [Key]
        public int? Id { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public string? Nic { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }
        public string? SubDepartment { get; set; }
        public int? BasicSalary { get; set; }
        public int? DaySalary { get; set; }
        public int? KPI { get; set; }
        public int? BRA1 { get; set; }
        public int? BRA2 { get; set; }
        public bool IsActive { get; set; } = true;
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public bool HasTaxExemption { get; set; }
    }
}

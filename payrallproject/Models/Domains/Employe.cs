using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? DepartmentID { get; set; }
        [ForeignKey(nameof(DepartmentID))]
        public Department Department { get; set; }
        public int? JobRoleId { get; set; }
        [ForeignKey(nameof(JobRoleId))]
        public JobRole JobRole { get; set; }
        public int? EmployeeCategoriesID { get; set; }
        [ForeignKey(nameof(EmployeeCategoriesID))]
        public EmployeeCategories EmployeeCategories { get; set; }
        public int? BasicSalary { get; set; }
        public int? DaySalary { get; set; }
        public int? KpiRate { get; set; }
        public int? KpiAmount { get; set; }
        public int? Bra1 { get; set; }
        public int? Bra2 { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public bool? HasTaxExemption { get; set; }
        public int? TotalCompensation { get; set; }
    }
}

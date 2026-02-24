using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Employe
{
    public int Id { get; set; }

    public string? EmployeeNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? FullName { get; set; }

    public string? Nic { get; set; }

    public DateTime? JoinedDate { get; set; }

    public DateTime? TerminationDate { get; set; }

    public string? PhoneNumber { get; set; }

    public int? DepartmentId { get; set; }

    public int? EmployeeCategoriesId { get; set; }

    public int? BasicSalary { get; set; }

    public int? DaySalary { get; set; }

    public int? KpiRate { get; set; }

    public int? KpiAmount { get; set; }

    public int? Bra1 { get; set; }

    public int? Bra2 { get; set; }

    public bool? IsActive { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? BankName { get; set; }

    public string? BankBranch { get; set; }

    public string? TaxIdentificationNumber { get; set; }

    public bool? HasTaxExemption { get; set; }

    public int? TotalCompensation { get; set; }

    public int? JobRoleId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual EmployeeCategory? EmployeeCategories { get; set; }

    public virtual ICollection<EmployeeOvertime> EmployeeOvertimes { get; set; } = new List<EmployeeOvertime>();

    public virtual JobRole? JobRole { get; set; }

    public virtual ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();

    public virtual ICollection<Leaf> Leaves { get; set; } = new List<Leaf>();

    public virtual ICollection<Leaves2> Leaves2s { get; set; } = new List<Leaves2>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<NoPayDay> NoPayDays { get; set; } = new List<NoPayDay>();

    public virtual ICollection<NoPayEntry> NoPayEntries { get; set; } = new List<NoPayEntry>();

    public virtual ICollection<SalaryReport> SalaryReports { get; set; } = new List<SalaryReport>();
}

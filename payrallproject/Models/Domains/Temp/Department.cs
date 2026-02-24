using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public int? EmployeeCategoriesId { get; set; }

    public bool? IsActive { get; set; }

    public virtual EmployeeCategory? EmployeeCategories { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<JobRole> JobRoles { get; set; } = new List<JobRole>();
}

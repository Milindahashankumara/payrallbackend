using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class JobRole
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public int? EmployeeCategoriesId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Department? Department { get; set; }

    public virtual EmployeeCategory? EmployeeCategories { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();
}

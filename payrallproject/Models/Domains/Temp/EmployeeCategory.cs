using System;
using System.Collections.Generic;

namespace payrallproject.Models.Domains.Temp;

public partial class EmployeeCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public bool? DaySalarybased { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<JobRole> JobRoles { get; set; } = new List<JobRole>();
}

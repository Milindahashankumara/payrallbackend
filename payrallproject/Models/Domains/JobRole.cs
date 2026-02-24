using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class JobRole
    {
        [Key]
        public int? Id { get; set; }
        
        [Required]
        public string? RoleName { get; set; }
        
        public int? DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
        
        public int? EmployeeCategoriesId { get; set; }
        [ForeignKey(nameof(EmployeeCategoriesId))]
        public EmployeeCategories EmployeeCategories { get; set; }
        
        public bool? IsActive { get; set; } = true;
    }
}

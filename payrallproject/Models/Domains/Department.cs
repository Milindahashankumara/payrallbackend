using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Domains
{
    public class Department
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        public int? EmployeeCategoriesId { get; set; }
        [ForeignKey(nameof(EmployeeCategoriesId))]
        public EmployeeCategories EmployeeCategories { get; set; }
        public bool? IsActive { get; set; }
    }
}

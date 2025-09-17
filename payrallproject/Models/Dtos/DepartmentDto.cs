using payrallproject.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;

namespace payrallproject.Models.Dtos
{
    public class DepartmentDto
    {
        public int? Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? Description { get; set; }
        public int? EmployeeCategoriesId { get; set; }
        public string? EmployeeCategoriesName { get; set; }
        public bool? IsActive { get; set; }
    }
}

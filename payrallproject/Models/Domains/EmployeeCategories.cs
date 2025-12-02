using System.ComponentModel.DataAnnotations;

namespace payrallproject.Models.Domains
{
    public class EmployeeCategories
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public bool? DaySalarybased { get; set; }
        public bool? IsActive { get; set; }

    }
}

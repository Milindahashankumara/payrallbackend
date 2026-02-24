namespace payrallproject.Models.Dtos
{
    public class JobRoleDto
    {
        public int? Id { get; set; }
        public string? RoleName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? EmployeeCategoriesId { get; set; }
        public string? EmployeeCategoriesName { get; set; }
        public bool? IsActive { get; set; }
    }
}

using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Department> AddDepartmentAsync(DepartmentDto newDepartment);

        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<Department?> UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
        Task<Department?> DeleteDepartmentAsync(int id);
        Task<List<Department>> GetAllDeletedDepartmentAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Department?> GetDeletedDepartmentByIdAsync(int id);
        Task<Department?> RecoverDeletedDepartmentAsync(int id);
    }
}

using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.EmployeeCategoriesService
{
    public interface IEmployeeCategoriesService
    {
        Task<List<EmployeeCategories>> GetAllAsync();
        Task<EmployeeCategories?> GetByIdAsync(int id);
        Task<EmployeeCategories> AddAsync(EmployeeCategoriesDto dto);
        Task<EmployeeCategories?> UpdateAsync(int id, EmployeeCategoriesDto dto);
        Task<EmployeeCategories> DeleteAsync(int id);
    }
}

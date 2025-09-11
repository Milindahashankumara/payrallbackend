using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using System;

namespace payrallproject.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AuthDbContext _dbContext;

        public DepartmentService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ✅ Get all active departments with optional filtering, sorting, pagination
        public async Task<List<Department>> GetAllDepartmentAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Departments
                .Include(d => d.EmployeeCategories)
                .Where(d => d.IsActive == true)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("DepartmentName", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(d => d.DepartmentName.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(d => d.Description != null && d.Description.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "departmentname" => isAscending ? query.OrderBy(d => d.DepartmentName) : query.OrderByDescending(d => d.DepartmentName),
                    "description" => isAscending ? query.OrderBy(d => d.Description) : query.OrderByDescending(d => d.Description),
                    _ => query.OrderBy(d => d.Id)
                };
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        // ✅ Get active department by Id
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _dbContext.Departments
                .Include(d => d.EmployeeCategories)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive == true);
        }

        // ✅ Add new department
        public async Task<Department> AddDepartmentAsync(DepartmentDto newDepartment)
        {
            var dept = new Department
            {
                DepartmentName = newDepartment.DepartmentName!,
                Description = newDepartment.Description,
                EmployeeCategoriesId = newDepartment.EmployeeCategoriesId,
                IsActive = newDepartment.IsActive ?? true
            };

            _dbContext.Departments.Add(dept);
            await _dbContext.SaveChangesAsync();
            return dept;
        }

        // ✅ Update existing department
        public async Task<Department?> UpdateDepartmentAsync(int id, DepartmentDto dto)
        {
            var dept = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id && d.IsActive == true);
            if (dept == null) return null;

            dept.DepartmentName = dto.DepartmentName ?? dept.DepartmentName;
            dept.Description = dto.Description ?? dept.Description;
            dept.EmployeeCategoriesId = dto.EmployeeCategoriesId ?? dept.EmployeeCategoriesId;
            dept.IsActive = dto.IsActive ?? dept.IsActive;

            await _dbContext.SaveChangesAsync();
            return dept;
        }

        // ✅ Soft delete
        public async Task<Department?> DeleteDepartmentAsync(int id)
        {
            var dept = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id && d.IsActive == true);
            if (dept == null) return null;

            dept.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return dept;
        }

        // ✅ Get all deleted departments
        public async Task<List<Department>> GetAllDeletedDepartmentAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.Departments
                .Include(d => d.EmployeeCategories)
                .Where(d => !d.IsActive == false)
                .AsQueryable();

            // Same filtering + sorting + pagination as above
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("DepartmentName", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(d => d.DepartmentName.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "departmentname" => isAscending ? query.OrderBy(d => d.DepartmentName) : query.OrderByDescending(d => d.DepartmentName),
                    _ => query.OrderBy(d => d.Id)
                };
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        // ✅ Get deleted department by Id
        public async Task<Department?> GetDeletedDepartmentByIdAsync(int id)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id && !d.IsActive == false);
        }

        // ✅ Recover deleted department
        public async Task<Department?> RecoverDeletedDepartmentAsync(int id)
        {
            var dept = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id && !d.IsActive == false);
            if (dept == null) return null;

            dept.IsActive = true;
            await _dbContext.SaveChangesAsync();
            return dept;
        }
    }
}

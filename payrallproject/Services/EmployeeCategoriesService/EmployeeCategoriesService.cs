using AutoMapper;
using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using System;

namespace payrallproject.Services.EmployeeCategoriesService
{
    public class EmployeeCategoriesService : IEmployeeCategoriesService
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeCategoriesService(AuthDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<EmployeeCategories>> GetAllAsync()
        {
            return await _dbContext.EmployeeCategories
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }

        public async Task<EmployeeCategories?> GetByIdAsync(int id)
        {
            return await _dbContext.EmployeeCategories
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
        }

        public async Task<EmployeeCategories> AddAsync(EmployeeCategoriesDto dto)
        {
            var category = new EmployeeCategories
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = true,
                DaySalarybased = dto.DaySalarybased,
            };

            _dbContext.EmployeeCategories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<EmployeeCategories?> UpdateAsync(int id, EmployeeCategoriesDto dto)
        {
            var category = await _dbContext.EmployeeCategories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
            if (category == null) return null;

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            category.DaySalarybased = dto.DaySalarybased;

            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<EmployeeCategories> DeleteAsync(int id)
        {
            var category = await _dbContext.EmployeeCategories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
            if (category == null) return null;

            category.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}

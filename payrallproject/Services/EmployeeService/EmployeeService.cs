using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using System.Text.Json;

namespace payrallproject.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper mapper;
        public EmployeeService(AuthDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Employe> AddEmployeAsync(EmployeDto employeDto)
        {
            var NewEmploye = mapper.Map<Employe>(employeDto);
            NewEmploye.IsActive = true;
            await _dbContext.Employe.AddAsync(NewEmploye);
            await _dbContext.SaveChangesAsync();
            return NewEmploye;
        }

        public async Task<Employe?> DeleteEmployeAsync(int id)
        {
            var SelectedEmploye = await _dbContext.Employe.Where(employe => employe.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedEmploye == null)
            {
                return null;
            }
            SelectedEmploye.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return SelectedEmploye;
        }

        public async Task<List<Employe>> GetAllDeletedEmployesAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var Employes = _dbContext.Employe.Where(employe => employe.IsActive == false).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Employes = Employes.Where(x => x.FullName.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    Employes = isAscending ? Employes.OrderBy(x => x.Id) : Employes.OrderByDescending(x => x.Id);
                }
            }
            var skipResult = (pageNumber - 1) * pageSize;
            return await Employes.Skip(skipResult).Take(pageSize).ToListAsync(); ;
        }

        public async Task<List<Employe>> GetAllEmployeesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var Employees = _dbContext.Employe.Where(asset => asset.IsActive == true).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("FullName", StringComparison.OrdinalIgnoreCase))
                {
                    Employees = Employees.Where(x => x.FullName.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    Employees = isAscending ? Employees.OrderBy(x => x.Id) : Employees.OrderByDescending(x => x.Id);
                }
            }
            var skipResult = (pageNumber - 1) * pageSize;
            return await Employees.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Employe?> GetDeletedEmployeByIdAsync(int id)
        {
            var Employe = await _dbContext.Employe.Where(employe => employe.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            return Employe;
        }

        public async Task<Employe?> GetEmployeByIdAsync(int id)
        {
            var SelectedEmploye = await _dbContext.Employe.Where(Employe => Employe.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            return SelectedEmploye;
        }

        public async Task<Employe?> RecoverDeletedEmployeAsync(int id)
        {
            var SelectedEmploye = await _dbContext.Employe.Where(employe => employe.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedEmploye == null)
            {
                return null;
            }
            SelectedEmploye.IsActive = true;
            await _dbContext.SaveChangesAsync();
            return SelectedEmploye;
        }

        public async Task<Employe?> UpdateEmployeAsync(int id, EmployeDto employeDto)
        {
            var SelectedEmploye = await _dbContext.Employe.Where(Employe => Employe.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedEmploye == null)
            {
                return null;
            }
            SelectedEmploye = mapper.Map(employeDto, SelectedEmploye);
            await _dbContext.SaveChangesAsync();
            return SelectedEmploye;
        }
    }
}

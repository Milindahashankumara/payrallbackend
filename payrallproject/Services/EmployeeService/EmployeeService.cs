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
        private readonly IMapper _mapper;
        public EmployeeService(AuthDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Employe> AddEmployeAsync(EmployeDto employeDto)
        {
            Console.WriteLine($"[SERVICE] Received DTO JobRoleId: {employeDto.JobRoleId}");
            var NewEmploye = _mapper.Map<Employe>(employeDto);
            Console.WriteLine($"[SERVICE] Mapped Employee JobRoleId: {NewEmploye.JobRoleId}");
            NewEmploye.IsActive = true;
            await _dbContext.Employe.AddAsync(NewEmploye);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"[SERVICE] Saved to DB - Employee ID: {NewEmploye.Id}, JobRoleId: {NewEmploye.JobRoleId}");
            return NewEmploye;
        }

        public async Task<Employe?> DeleteEmployeAsync(int id, DateTime terminationDate)
        {
            var SelectedEmploye = await _dbContext.Employe.Where(employe => employe.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedEmploye == null)
            {
                return null;
            }
            SelectedEmploye.TerminationDate = terminationDate;
            SelectedEmploye.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return SelectedEmploye;
        }

        public async Task<List<Employe>> GetAllDeletedEmployesAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 20)
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
            int pageNumber = 1, int pageSize = 20)
        {
            var Employees = _dbContext.Employe.Where(employe => employe.IsActive == true).AsQueryable();
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

        public async Task<List<Employe>> GetAllEmployeesHaveLeavesAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var employees = _dbContext.Employe
                .Include(e => e.EmployeeCategories) // include category for filtering
                .Where(e => e.IsActive == true && e.EmployeeCategories.DaySalarybased == false) // only non-day-salary-based
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("FullName", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(x => x.FullName.Contains(filterQuery));
                }
                else if (filterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(x => x.Email != null && x.Email.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                employees = sortBy.ToLower() switch
                {
                    "id" => isAscending ? employees.OrderBy(x => x.Id) : employees.OrderByDescending(x => x.Id),
                    "fullname" => isAscending ? employees.OrderBy(x => x.FullName) : employees.OrderByDescending(x => x.FullName),
                    "email" => isAscending ? employees.OrderBy(x => x.Email) : employees.OrderByDescending(x => x.Email),
                    _ => employees.OrderBy(x => x.Id)
                };
            }

            // Pagination
            var skipResult = (pageNumber - 1) * pageSize;
            employees = employees.Skip(skipResult).Take(pageSize);

            return await employees.ToListAsync();
        }


        public async Task<Employe?> GetDeletedEmployeByIdAsync(int id)
        {
            var Employe = await _dbContext.Employe.Where(employe => employe.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            return Employe;
        }

        public async Task<Employe?> GetEmployeHaveLeavesByIdAsync(int id)
        {
            var selectedEmploye = await _dbContext.Employe
                .Include(e => e.EmployeeCategories) // load related category
                .Where(e => e.IsActive == true
                         && e.Id == id
                         && e.EmployeeCategories.DaySalarybased == false) // filter by category flag
                .FirstOrDefaultAsync();

            return selectedEmploye;
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
            _mapper.Map(employeDto, SelectedEmploye);
            await _dbContext.SaveChangesAsync();
            return SelectedEmploye;
        }
    }
}

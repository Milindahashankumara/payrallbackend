using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.NoPayDayService
{
    public class NoPayDayService : INoPayDayService
    {
        private readonly AuthDbContext _dbContext;
        public NoPayDayService(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NoPayDay> CreateNoPayDayAsync(NoPayDayDto noPayDayDto)
        {
            var noPayDay = new NoPayDay
            {
                EmployeID = noPayDayDto.EmployeID,
                Date = noPayDayDto.Date,
                Reason = noPayDayDto.Reason
            };

            _dbContext.NoPayDay.Add(noPayDay);
            await _dbContext.SaveChangesAsync();

            return noPayDay;
        }

        public async Task<List<NoPayDay>> GetNoPayDaysByEmployeeAsync(int employeID)
        {
            return await _dbContext.NoPayDay
                .Where(n => n.EmployeID == employeID)
                .OrderByDescending(n => n.Date)
                .ToListAsync();
        }

        public async Task<List<NoPayDay>> GetNoPayDaysByEmployeeAndMonthAsync(int employeID, int year, int month)
        {
            return await _dbContext.NoPayDay
                .Where(n => n.EmployeID == employeID && n.Date.Year == year && n.Date.Month == month)
                .OrderBy(n => n.Date)
                .ToListAsync();
        }

        public async Task<bool> DeleteNoPayDayAsync(int id)
        {
            var noPayDay = await _dbContext.NoPayDay.FindAsync(id);
            if (noPayDay == null) return false;

            _dbContext.NoPayDay.Remove(noPayDay);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}

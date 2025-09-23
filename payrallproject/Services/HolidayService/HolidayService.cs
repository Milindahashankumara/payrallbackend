using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Services.HolidayService;

namespace payrallproject.Services.HolidayService
{
    public class HolidayService : IHolidayService
    {
        private readonly AuthDbContext _context;

        public HolidayService(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<HolidayDto>> GetAllHolidaysAsync()
        {
            var holidays = await _context.Holiday
                .OrderBy(h => h.Date)
                .ToListAsync();

            return holidays.Select(h => MapToDto(h)).ToList();
        }

        public async Task<HolidayDto> GetHolidayByIdAsync(int id)
        {
            var holiday = await _context.Holiday.FindAsync(id);
            return holiday != null ? MapToDto(holiday) : null;
        }

        public async Task<HolidayDto> CreateHolidayAsync(CreateHolidayDto createHolidayDto)
        {
            var holiday = new Holiday
            {
                Name = createHolidayDto.Name,
                Date = createHolidayDto.Date,
                Description = createHolidayDto.Description,
                IsRecurring = createHolidayDto.IsRecurring,
                HolidayType = createHolidayDto.HolidayType,
                CreatedAt = DateTime.UtcNow
            };

            _context.Holiday.Add(holiday);
            await _context.SaveChangesAsync();

            return MapToDto(holiday);
        }

        public async Task<HolidayDto> UpdateHolidayAsync(int id, UpdateHolidayDto updateHolidayDto)
        {
            var holiday = await _context.Holiday.FindAsync(id);
            if (holiday == null)
                return null;

            holiday.Name = updateHolidayDto.Name;
            holiday.Date = updateHolidayDto.Date;
            holiday.Description = updateHolidayDto.Description;
            holiday.IsRecurring = updateHolidayDto.IsRecurring;
            holiday.HolidayType = updateHolidayDto.HolidayType;
            holiday.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return MapToDto(holiday);
        }

        public async Task<bool> DeleteHolidayAsync(int id)
        {
            var holiday = await _context.Holiday.FindAsync(id);
            if (holiday == null)
                return false;

            _context.Holiday.Remove(holiday);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<HolidayDto>> GetHolidaysByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var holidays = await _context.Holiday
                .Where(h => h.Date >= startDate && h.Date <= endDate)
                .OrderBy(h => h.Date)
                .ToListAsync();

            return holidays.Select(h => MapToDto(h)).ToList();
        }

        public async Task<int> GetHolidaysCountAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Holiday
                .Where(h => h.Date >= startDate && h.Date <= endDate)
                .CountAsync();
        }

        public async Task<int> GetNonWeekendHolidayCountAsync(DateTime startDate, DateTime endDate)
        {
            var holidays = await _context.Holiday
                .Where(h => h.Date >= startDate && h.Date <= endDate)
                .ToListAsync();

            // Exclude Saturdays (DayOfWeek == 6) and Sundays (DayOfWeek == 0)
            int count = holidays.Count(h => h.Date.DayOfWeek != DayOfWeek.Saturday && h.Date.DayOfWeek != DayOfWeek.Sunday);

            return count;
        }

        public async Task<List<HolidayDto>> GetHolidaysByYearAsync(int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year, 12, 31);

            return await GetHolidaysByDateRangeAsync(startDate, endDate);
        }

        private HolidayDto MapToDto(Holiday holiday)
        {
            return new HolidayDto
            {
                Id = holiday.Id,
                Name = holiday.Name,
                Date = holiday.Date,
                Description = holiday.Description,
                IsRecurring = holiday.IsRecurring,
                HolidayType = holiday.HolidayType,
                CreatedAt = holiday.CreatedAt,
                UpdatedAt = holiday.UpdatedAt
            };
        }
    }
}
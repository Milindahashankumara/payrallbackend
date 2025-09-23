using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.HolidayService
{
    public interface IHolidayService
    {
        Task<List<HolidayDto>> GetAllHolidaysAsync();
        Task<HolidayDto> GetHolidayByIdAsync(int id);
        Task<HolidayDto> CreateHolidayAsync(CreateHolidayDto createHolidayDto);
        Task<HolidayDto> UpdateHolidayAsync(int id, UpdateHolidayDto updateHolidayDto);
        Task<bool> DeleteHolidayAsync(int id);
        Task<List<HolidayDto>> GetHolidaysByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<int> GetHolidaysCountAsync(DateTime startDate, DateTime endDate);
        Task<List<HolidayDto>> GetHolidaysByYearAsync(int year);
        Task<int> GetNonWeekendHolidayCountAsync(DateTime startDate, DateTime endDate);
    }
}
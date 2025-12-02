using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.NoPayDayService
{
    public interface INoPayDayService
    {
        Task<NoPayDay> CreateNoPayDayAsync(NoPayDayDto noPayDayDto);
        Task<List<NoPayDay>> GetNoPayDaysByEmployeeAsync(int employeID);
        Task<List<NoPayDay>> GetNoPayDaysByEmployeeAndMonthAsync(int employeID, int year, int month);
        Task<bool> DeleteNoPayDayAsync(int id);
    }
}

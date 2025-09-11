using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;

namespace payrallproject.Services.OTService
{
    public interface IOTService
    {
        Task<List<OT>> GetAllOTAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<OT> AddOTAsync(OTDto newOT);

        Task<OT?> GetOTByIdAsync(int id);
        Task<OT?> UpdateOTAsync(int id, OTDto otDto);
        Task<OT?> DeleteOTAsync(int id);
        Task<List<OT>> GetAllDeletedOTAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<OT?> GetDeletedOTByIdAsync(int id);
        Task<OT?> RecoverDeletedOTAsync(int id);
    }
}

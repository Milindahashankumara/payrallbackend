using AutoMapper;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace payrallproject.Services.OTService
{
    public class OTService : IOTService
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper _mapper;

        public OTService(AuthDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OT> AddOTAsync(OTDto newOT)
        {
            var ot = _mapper.Map<OT>(newOT);
            ot.IsActive = true;

            await _dbContext.OT.AddAsync(ot);
            await _dbContext.SaveChangesAsync();

            return ot;
        }

        public async Task<OT?> DeleteOTAsync(int id)
        {
            var ot = await _dbContext.OT.Where(x => x.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (ot == null) return null;

            ot.IsActive = false;
            await _dbContext.SaveChangesAsync();

            return ot;
        }

        public async Task<List<OT>> GetAllOTAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var ots = _dbContext.OT.Where(x => x.IsActive == true).AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    ots = ots.Where(x => x.Name!.Contains(filterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    ots = isAscending ? ots.OrderBy(x => x.Id) : ots.OrderByDescending(x => x.Id);
                }
                else if (sortBy.Equals("Rate", StringComparison.OrdinalIgnoreCase))
                {
                    ots = isAscending ? ots.OrderBy(x => x.Rate) : ots.OrderByDescending(x => x.Rate);
                }
            }

            // pagination
            var skip = (pageNumber - 1) * pageSize;
            return await ots.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<List<OT>> GetAllDeletedOTAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var ots = _dbContext.OT.Where(x => x.IsActive == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    ots = ots.Where(x => x.Name!.Contains(filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    ots = isAscending ? ots.OrderBy(x => x.Id) : ots.OrderByDescending(x => x.Id);
                }
                else if (sortBy.Equals("Rate", StringComparison.OrdinalIgnoreCase))
                {
                    ots = isAscending ? ots.OrderBy(x => x.Rate) : ots.OrderByDescending(x => x.Rate);
                }
            }

            var skip = (pageNumber - 1) * pageSize;
            return await ots.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<OT?> GetOTByIdAsync(int id)
        {
            return await _dbContext.OT.Where(x => x.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OT?> GetDeletedOTByIdAsync(int id)
        {
            return await _dbContext.OT.Where(x => x.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OT?> RecoverDeletedOTAsync(int id)
        {
            var ot = await _dbContext.OT.Where(x => x.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            if (ot == null) return null;

            ot.IsActive = true;
            await _dbContext.SaveChangesAsync();

            return ot;
        }

        public async Task<OT?> UpdateOTAsync(int id, OTDto otDto)
        {
            var ot = await _dbContext.OT.Where(x => x.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (ot == null) return null;

            _mapper.Map(otDto, ot);
            await _dbContext.SaveChangesAsync();

            return ot;
        }
    }
}

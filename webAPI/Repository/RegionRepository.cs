using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public RegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        // This method returns a task and therefore needs the async and await keywords as it is asynchronous.
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}

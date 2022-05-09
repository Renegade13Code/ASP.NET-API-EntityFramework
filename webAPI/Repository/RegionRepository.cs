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

        public async Task<Region?> GetAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.RegionId == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.RegionId = Guid.NewGuid();
            await this.dbContext.AddAsync(region);
            await this.dbContext.SaveChangesAsync();
            return region;

        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            Region? region = await this.dbContext.Regions.FirstOrDefaultAsync(x => x.RegionId == id);
            if (region == null)
            {
                return region;
            }

            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            // Check if region exists
            Region? foundRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.RegionId == id);

            if(foundRegion == null)
            {
                return null;
            }

            foundRegion.Code = region.Code;
            foundRegion.Name = region.Name;
            foundRegion.Area = region.Area;
            foundRegion.Lat = region.Lat;
            foundRegion.Long = region.Long;
            foundRegion.Population = region.Population;

            // Update region
            await dbContext.SaveChangesAsync();

            return foundRegion;
        }
    }
}

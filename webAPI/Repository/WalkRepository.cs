using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkRepository(NZWalksDbContext DbContext)
        {
            dbContext = DbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            Walk? walk = await dbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync<Walk>(x => x.Id == id);
            if(walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Walk walk)
        {
            Walk? foundWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == walk.Id);
            
            if(foundWalk == null)
            {
                return null;
            }

            foundWalk.Name = walk.Name;
            foundWalk.Length = walk.Length;
            foundWalk.WalkDifficultyId = walk.WalkDifficultyId;
            foundWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return foundWalk;
            
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? walk = await dbContext.Walks.FirstOrDefaultAsync<Walk>(x => x.Id == id);

            if(walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }
    }
}

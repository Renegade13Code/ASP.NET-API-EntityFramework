using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models.Domain;
using webAPI.Models.DTO;

namespace webAPI.Repository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDiff)
        {
            walkDiff.WalkDifficultyId = Guid.NewGuid();
            await dbContext.WalkDifficulty.AddAsync(walkDiff);
            await dbContext.SaveChangesAsync();
            return walkDiff;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await dbContext.WalkDifficulty.ToListAsync<WalkDifficulty>();
        }

        public async Task<WalkDifficulty?> GetAsync(Guid id)
        {
            WalkDifficulty? walkDiff = await dbContext.WalkDifficulty.FindAsync(id);
            return walkDiff;
        }

        public async Task<WalkDifficulty?> UpdateAsync(WalkDifficulty walkDiff)
        {
            WalkDifficulty? foundWalkDiff = await dbContext.WalkDifficulty.FindAsync(walkDiff.WalkDifficultyId);
            if (foundWalkDiff == null)
            {
                return null;
            }
            foundWalkDiff.Code = walkDiff.Code;
            await dbContext.SaveChangesAsync();
            return foundWalkDiff;
        }

        public async Task<WalkDifficulty?> DeleteAsync(Guid id)
        {
            WalkDifficulty? walkDiff = await dbContext.WalkDifficulty.FindAsync(id);
            if(walkDiff == null)
            {
                return null;
            }
            dbContext.WalkDifficulty.Remove(walkDiff);
            await dbContext.SaveChangesAsync();
            return walkDiff;
        }
    }
}

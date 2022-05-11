using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public interface IWalkDifficultyRepository
    {
        // CRUD
        public Task<WalkDifficulty> AddAsync(WalkDifficulty walkDiff);
        public Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        public Task<WalkDifficulty?> GetAsync(Guid id);
        public Task<WalkDifficulty?> UpdateAsync(WalkDifficulty walkDiff);
        public Task<WalkDifficulty?> DeleteAsync(Guid id);
    }
}

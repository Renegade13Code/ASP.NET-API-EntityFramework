using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public interface IWalkRepository
    {
        public Task<IEnumerable<Walk>> GetAllAsync();
        public Task<Walk?> GetAsync(Guid id);
        public Task<Walk> AddAsync(Walk walk); 
        public Task<Walk?> UpdateAsync(Walk walk);
        public Task<Walk?> DeleteAsync(Guid id);

    }
}

using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public interface IRegionRepository
    {
        public Task<IEnumerable<Region>> GetAllAsync();
    }
}

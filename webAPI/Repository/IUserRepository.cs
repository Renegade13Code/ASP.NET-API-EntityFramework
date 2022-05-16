using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public interface IUserRepository
    {
        public Task<User?> Authenticate(string username, string password);
        public Task<IEnumerable<Role>?> GetRoles(User user);
    }
}

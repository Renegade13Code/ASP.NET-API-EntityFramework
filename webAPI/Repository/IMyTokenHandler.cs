using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public interface IMyTokenHandler
    {
        Task<string> CreateToken(User user);
    }
}

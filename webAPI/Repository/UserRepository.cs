using Microsoft.EntityFrameworkCore;
using webAPI.Data;
using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext dbContext;

        public UserRepository(NZWalksDbContext DbContext)
        {
            dbContext = DbContext;
        }
        public Task<User?> Authenticate(string username, string password)
        {
            
            var user = dbContext.Users.FirstOrDefaultAsync( x => x.UserName.ToLower() == username.ToLower()
                && x.Password == password);
            return user;
        }

        public async Task<IEnumerable<Role>?> GetRoles(User user)
        {
            //This was first attempt
            //var joinResult = await dbContext.UserRoles.Join(dbContext.Roles,
            //    userRole => userRole.RoleId,
            //    role => role.Id,
            //    (userRole, role) => new { userRole.UserId, role.Id, role.name }).ToListAsync();

            //var roles = joinResult.FindAll(x => x.UserId == user.Id).Select(x => new Role
            //{
            //    Id = x.Id,
            //    name = x.name
            //});

            //return roles;

            // udemy implementation
            var userRoles = await dbContext.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                List<Role> roles = new List<Role>();

                foreach (var item in userRoles)
                {
                    var role = await dbContext.Roles.FindAsync(item.RoleId);
                    roles.Add(new Role
                    {
                        Id = item.RoleId,
                        name = role.name
                    });
                }

                return roles;
            }
            return null;
        }
    }
}

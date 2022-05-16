using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            //new User()
            //{
            //    FirstName = "Jeff",
            //    LastName = "Smith",
            //    UserName = "Jeff13",
            //    EmailAddress = "jeff@gmail.com",
            //    Password = "iamjeff",
            //    Roles = new List<string>()
            //    {
            //        "reader"
            //    }
            //},
            //new User()
            //{
            //    FirstName = "Tyler",
            //    LastName = "Durdan",
            //    UserName = "Tyler13",
            //    EmailAddress = "tyler@gmail.com",
            //    Password = "fightclub",
            //    Roles = new List<string>()
            //    {
            //        "reader",
            //        "writer"
            //    }
            //},

        };
        public async Task<User?> Authenticate(string username, string password)
        {
            // This will print to vs output when running with debug mode
            System.Diagnostics.Debug.WriteLine($"{username}, {password}");
            return Users.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase)
                && x.Password == password);
        }

        public Task<IEnumerable<Role>> GetRoles(User user)
        {
            throw new NotImplementedException();
        }
    }
}

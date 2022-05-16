namespace webAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        //public List<string> Roles { get; set; }

        // Navigational properties
        public List<User_Role> UserRoles { get; set; }

    }
}

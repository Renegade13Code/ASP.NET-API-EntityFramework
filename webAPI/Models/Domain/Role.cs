namespace webAPI.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string name { get; set; }

        // Navigational properties
        public List<User_Role> UserRoles { get; set; }
    }
}

namespace webAPI.Models.Domain
{
    // This Domain model is necessary because there exists a many to many relationship 
    // between users and roles. Therefore a separate table is needed in a relatinal database
    // to document this relationship.
    public class User_Role
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}

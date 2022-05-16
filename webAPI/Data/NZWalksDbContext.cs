/* The DbContext represents a session with the database.
 * Used to make a connection to a database, query and persist data to the DB
 */
using Microsoft.EntityFrameworkCore;
using webAPI.Models.Domain;

namespace webAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
        // the : indicates that a constructor of the base class will be called and will execute first, with any passed parameters, before the code of the child constructor. 
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Liskov says not to remove this? udemy says we should?
            base.OnModelCreating(modelBuilder);

            // Define relationship between User_Roles with Role. 
            // Tells EF that one Role is associated with many entries in the User_Role table
            // And specify that RoleId is a foreign key'
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            // Define relationship between User_Roles with User. 
            // Tells EF that one User is associated with many entries in the User_Role table
            // And specify that UserId is a foreign key'
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }

        // This property tells entity to create a regions table if it does not exist in the database.
        // We can then use this class 'WalksDbContext' to query and persist data in the DB
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<User_Role> UserRoles{ get; set; }
        public DbSet<Role> Roles { get; set; }


    }
}

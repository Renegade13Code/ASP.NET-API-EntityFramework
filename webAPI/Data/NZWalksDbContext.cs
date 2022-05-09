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
        // This property tells entity to create a regions table if it does not exist in the database.
        // We can then use this class 'WalksDbContext' to query and persist data in the DB
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}

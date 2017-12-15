using Microsoft.EntityFrameworkCore;
 
namespace DojoActivities.Models
{
    public class ActivityContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Signup> Signups { get; set; }
    }
}
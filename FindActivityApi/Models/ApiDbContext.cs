using Microsoft.EntityFrameworkCore;

namespace FindActivityApi.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Evnt> Evnts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

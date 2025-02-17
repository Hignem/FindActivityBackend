using Microsoft.EntityFrameworkCore;
using Npgsql;

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
        public DbSet<EvntLikes> EvntLikes { get; set; }
        public DbSet<EvntGoing> EvntGoing { get; set; }
    }
}

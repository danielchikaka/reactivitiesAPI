using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext: IdentityDbContext<AppUser>
    {

        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserActivity>(
                x => x.HasKey(ua => new {ua.AppUserId, ua.ActivityId})
            );

            builder.Entity<UserActivity>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.UserActivities)
                .HasForeignKey(u => u.AppUserId);
                
            builder.Entity<UserActivity>()
                .HasOne(u => u.Activity)
                .WithMany(a => a.UserActivities)
                .HasForeignKey(u => u.ActivityId);
        } 
    }
}
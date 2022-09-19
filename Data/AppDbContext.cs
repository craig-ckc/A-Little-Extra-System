using A_Little_Extra_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace A_Little_Extra_System.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many Relationship: Activity and Student

            modelBuilder.Entity<ActivityParticipation>().HasKey(e => new { e.ActivityId, e.UserId });

            modelBuilder.Entity<ActivityParticipation>().HasOne<User>(ap => ap.User).WithMany(u => u.ActivityParticipation).HasForeignKey(ap => ap.UserId).IsRequired(false);
            modelBuilder.Entity<ActivityParticipation>().HasOne<Activity>(ap => ap.Activity).WithMany(a => a.ActivityParticipation).HasForeignKey(ap => ap.ActivityId).IsRequired(false);

            // Many-to-many Relationship: Activity and DepartmentStaff
            modelBuilder.Entity<ActivitySupervision>().HasKey(e => new { e.ActivityId, e.UserId });

            modelBuilder.Entity<ActivitySupervision>().HasOne(m => m.Activity).WithMany(am => am.ActivitySupervision).HasForeignKey(m => m.ActivityId).IsRequired(false);
            modelBuilder.Entity<ActivitySupervision>().HasOne(m => m.User).WithMany(am => am.ActivitySupervision).HasForeignKey(m => m.UserId).IsRequired(false);

            // Many-to-many Relationship: Activity and DepartmentStaff
            modelBuilder.Entity<StudentAward>().HasKey(e => new { e.AwardId, e.UserId });

            modelBuilder.Entity<StudentAward>().HasOne(m => m.Award).WithMany(am => am.StudentAward).HasForeignKey(m => m.AwardId).IsRequired(false);
            modelBuilder.Entity<StudentAward>().HasOne(m => m.User).WithMany(am => am.StudentAward).HasForeignKey(m => m.UserId).IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Activity> Activity { get; set; }

        public DbSet<Award> Award { get; set; }

        public DbSet<StudentAward> StudentAward { get; set; }

        public DbSet<ActivityParticipation> ActivityParticipation { get; set; }

        public DbSet<ActivitySupervision> ActivitySupervision { get; set; }
    }
}

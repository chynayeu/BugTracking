using BugTracking.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTracking.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public
       ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //One-to-many Ticket -> Comments
            modelBuilder.Entity<Ticket>().HasMany(t => t.Comments);

            //One-to-many User -> comments
            modelBuilder.Entity<User>().HasMany(u => u.Comments);

            //One-to-many Project -> Tickets
            modelBuilder.Entity<Project>().HasMany(t => t.Tickets);

            // Many-to-many User - Project
            modelBuilder.Entity<User>()
                .HasMany<Project>(t => t.Projects)
                .WithMany(t => t.Users);

            // Many-to-many User - UserRoles
            modelBuilder.Entity<User>()
                .HasMany<UserRole>(u => u.Roles)
                .WithMany(r => r.Users);
        }
    }
}

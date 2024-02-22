using Microsoft.EntityFrameworkCore;
using University.Entity.Entities;

namespace University.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Unversity> Universities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasIndex(b => b.CourseName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(b => b.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.University)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.UniversityId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.University)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Unversity>().HasData(
                new Unversity { Id = 1, Name = "Jordanian University" },
                new Unversity { Id = 2, Name = "Hashmite University" });

        }
    }
}

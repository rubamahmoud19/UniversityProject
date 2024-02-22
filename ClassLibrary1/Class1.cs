using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1
{
    public class University5DbContext : DbContext
    {
        public University5DbContext(DbContextOptions<University5DbContext> options) : base(options)
        {

        }
        public DbSet<Test> Universities { get; set; }
        //public DbSet<Course> Courses { get; set; }
        //public DbSet<User> Users { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Course>()
        //        .HasIndex(b => b.CourseName)
        //        .IsUnique();
        //}
    }

}

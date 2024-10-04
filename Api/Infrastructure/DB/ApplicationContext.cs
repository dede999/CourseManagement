using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.DB;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Video> Videos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Email);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Courses)
            .WithOne(c => c.Creator)
            .HasForeignKey(c => c.CreatorEmail);

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Title)
            .IsUnique();
        
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Videos)
            .WithOne(v => v.Course)
            .HasForeignKey(c => c.CourseCode)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Video>()
            .HasIndex(v => new { v.Title, v.CourseCode })
            .IsUnique();
    }
}
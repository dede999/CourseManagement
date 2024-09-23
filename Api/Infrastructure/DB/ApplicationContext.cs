using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.DB;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Email);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Courses)
            .WithOne(c => c.Creator)
            .HasForeignKey(c => c.CreatorEmail);
    }
}
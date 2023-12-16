using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        //Task
        modelBuilder.Entity<UserTask>().HasKey(t => t.Id);
        modelBuilder.Entity<UserTask>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId);
    }
}
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<UserTask> UserTasks { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        //Task
        modelBuilder.Entity<UserTask>().HasKey(t => t.Id);
        modelBuilder.Entity<UserTask>()
            .HasOne(t => t.User)
            .WithMany(u => u.UserTasks)
            .HasForeignKey(t => t.UserId);
    }
}
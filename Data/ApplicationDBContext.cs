using Microsoft.EntityFrameworkCore;
using WellnessTracker.Models.Entities;

namespace WellnessTracker.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<TaskItem> TaskItems { get; set; } = null!; 
    public DbSet<MealEntry> Meals { get; set; } = null!; 
    public DbSet<EmotionEntry> EmotionEntries { get; set; } = null!; 
    public DbSet<Note> Notes { get; set; } = null!; 
    public DbSet<HealthCheck> HealthChecks { get; set; } = null!; 

}
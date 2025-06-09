using Microsoft.EntityFrameworkCore;
using Wellness_Tracker.Models.Entities;

namespace Wellness_Tracker.Data;

public class ApplicationDBContext :DbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<MealEntry> Meals { get; set; }
    public DbSet<EmotionEntry> EmotionEntries { get; set; }
    public DbSet<HealthCheck> HealthChecks { get; set; }
    public DbSet<Note> Notes { get; set; }
}
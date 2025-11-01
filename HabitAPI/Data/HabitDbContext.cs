using Microsoft.EntityFrameworkCore;
using HabitModels.Models;

namespace HabitAPI.Data;

/// <summary>
/// Database context for the Habit Tracker application
/// </summary>
public class HabitDbContext : DbContext
{
    public HabitDbContext(DbContextOptions<HabitDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Users in the system
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Habits tracked by users
    /// </summary>
    public DbSet<Habit> Habits { get; set; }

    /// <summary>
    /// Individual habit completion entries
    /// </summary>
    public DbSet<HabitEntry> HabitEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.TimeZone).HasMaxLength(100);
            entity.Property(e => e.DateFormat).HasMaxLength(20);
            entity.Property(e => e.Theme).HasMaxLength(20).HasDefaultValue("auto");
            entity.Property(e => e.ProfilePictureUrl).HasMaxLength(500);
            entity.Property(e => e.DailyReminders).HasDefaultValue(true);
            entity.Property(e => e.WeeklySummaries).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // Configure Habit entity
        modelBuilder.Entity<Habit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);
            entity.Property(e => e.ColorCode).HasMaxLength(7);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TargetCount).HasDefaultValue(1);
            entity.Property(e => e.Frequency).HasDefaultValue(HabitFrequency.Daily);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.TargetValue).HasColumnType("decimal(18,2)");

            // Configure relationships
            entity.HasOne<User>()
                  .WithMany(u => u.Habits)
                  .HasForeignKey(h => h.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(h => h.Entries)
                  .WithOne(e => e.Habit)
                  .HasForeignKey(e => e.HabitId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure HabitEntry entity
        modelBuilder.Entity<HabitEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.HabitId).IsRequired();
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            entity.Property(e => e.Count).HasDefaultValue(1);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Weather).HasMaxLength(50);
            entity.Property(e => e.IsManualEntry).HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.ActualValue).HasColumnType("decimal(18,2)");

            // Configure relationships
            entity.HasOne(e => e.Habit)
                  .WithMany(h => h.Entries)
                  .HasForeignKey(e => e.HabitId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<User>()
                  .WithMany(u => u.HabitEntries)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.NoAction); // Prevent cascade conflicts
        });

        // Add some seed data for development
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed test user
        var testUserId = "test-user-1";
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = testUserId,
            Email = "test@example.com",
            DisplayName = "Test User",
            FirstName = "Test",
            LastName = "User",
            TimeZone = "UTC",
            Theme = "light",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Seed test habits
        modelBuilder.Entity<Habit>().HasData(
            new Habit
            {
                Id = 1,
                Name = "Morning Exercise",
                Description = "30 minutes of morning exercise to start the day",
                Category = "Health",
                Frequency = HabitFrequency.Daily,
                TargetCount = 1,
                TargetValue = 30,
                Unit = "minutes",
                ColorCode = "#FF5733",
                UserId = testUserId,
                StartDate = DateTime.UtcNow.Date.AddDays(-30),
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                UpdatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Habit
            {
                Id = 2,
                Name = "Read Books",
                Description = "Read at least 20 pages of a book daily",
                Category = "Education",
                Frequency = HabitFrequency.Daily,
                TargetCount = 1,
                TargetValue = 20,
                Unit = "pages",
                ColorCode = "#3498DB",
                UserId = testUserId,
                StartDate = DateTime.UtcNow.Date.AddDays(-20),
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                UpdatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Habit
            {
                Id = 3,
                Name = "Drink Water",
                Description = "Stay hydrated by drinking at least 8 glasses of water",
                Category = "Health",
                Frequency = HabitFrequency.Daily,
                TargetCount = 8,
                TargetValue = 1,
                Unit = "glasses",
                ColorCode = "#2ECC71",
                UserId = testUserId,
                StartDate = DateTime.UtcNow.Date.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            }
        );
    }
}
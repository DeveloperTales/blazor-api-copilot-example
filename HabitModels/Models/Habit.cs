using System.ComponentModel.DataAnnotations;

namespace HabitModels.Models;

/// <summary>
/// Represents a habit that a user wants to track
/// </summary>
public class Habit
{
    /// <summary>
    /// Unique identifier for the habit
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the habit
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the habit
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Category of the habit (e.g., Health, Productivity, Personal Development)
    /// </summary>
    [StringLength(50)]
    public string? Category { get; set; }

    /// <summary>
    /// Target frequency for the habit (e.g., daily, weekly)
    /// </summary>
    public HabitFrequency Frequency { get; set; } = HabitFrequency.Daily;

    /// <summary>
    /// Target count per frequency period (e.g., 3 times per week)
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TargetCount { get; set; } = 1;

    /// <summary>
    /// Unit of measurement for the habit (e.g., minutes, pages, glasses)
    /// </summary>
    [StringLength(50)]
    public string? Unit { get; set; }

    /// <summary>
    /// Target value per occurrence (e.g., 30 minutes, 10 pages)
    /// </summary>
    public decimal? TargetValue { get; set; }

    /// <summary>
    /// Color code for visual representation in UI
    /// </summary>
    [StringLength(7)] // For hex color codes like #FF5733
    public string? ColorCode { get; set; }

    /// <summary>
    /// Whether the habit is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Date when the habit was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the habit was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the user started tracking this habit
    /// </summary>
    public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;

    /// <summary>
    /// Optional end date for time-bound habits
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// User ID who owns this habit
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Navigation property for habit entries/completions
    /// </summary>
    public virtual ICollection<HabitEntry> Entries { get; set; } = new List<HabitEntry>();

    /// <summary>
    /// Calculates the current streak for this habit
    /// </summary>
    public int CalculateCurrentStreak()
    {
        if (!Entries.Any()) return 0;

        var today = DateTime.UtcNow.Date;
        var streak = 0;
        var currentDate = today;

        // Work backwards from today to find consecutive days with completions
        while (true)
        {
            var entriesForDate = Entries.Where(e => e.CompletedAt.Date == currentDate).ToList();
            
            if (Frequency == HabitFrequency.Daily)
            {
                if (entriesForDate.Sum(e => e.Count) >= TargetCount)
                {
                    streak++;
                    currentDate = currentDate.AddDays(-1);
                }
                else
                {
                    break;
                }
            }
            else
            {
                // For weekly/monthly habits, more complex logic would be needed
                break;
            }

            // Prevent infinite loop
            if (currentDate < StartDate) break;
        }

        return streak;
    }

    /// <summary>
    /// Checks if the habit is completed for a given date
    /// </summary>
    public bool IsCompletedForDate(DateTime date)
    {
        var entriesForDate = Entries.Where(e => e.CompletedAt.Date == date.Date);
        return entriesForDate.Sum(e => e.Count) >= TargetCount;
    }
}
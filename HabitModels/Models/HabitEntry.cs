using System.ComponentModel.DataAnnotations;

namespace HabitModels.Models;

/// <summary>
/// Represents a single completion/entry for a habit
/// </summary>
public class HabitEntry
{
    /// <summary>
    /// Unique identifier for the habit entry
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key to the associated habit
    /// </summary>
    [Required]
    public int HabitId { get; set; }

    /// <summary>
    /// Navigation property to the associated habit
    /// </summary>
    public virtual Habit Habit { get; set; } = null!;

    /// <summary>
    /// Date and time when the habit was completed
    /// </summary>
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Number of times the habit was completed (for habits that can be done multiple times)
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Count { get; set; } = 1;

    /// <summary>
    /// Actual value achieved (e.g., 45 minutes instead of target 30 minutes)
    /// </summary>
    public decimal? ActualValue { get; set; }

    /// <summary>
    /// Optional notes about this completion
    /// </summary>
    [StringLength(500)]
    public string? Notes { get; set; }

    /// <summary>
    /// Mood or satisfaction rating for this completion (1-10 scale)
    /// </summary>
    [Range(1, 10)]
    public int? Rating { get; set; }

    /// <summary>
    /// Location where the habit was completed
    /// </summary>
    [StringLength(100)]
    public string? Location { get; set; }

    /// <summary>
    /// Weather conditions when habit was completed (for outdoor activities)
    /// </summary>
    [StringLength(50)]
    public string? Weather { get; set; }

    /// <summary>
    /// Date when this entry was created in the system
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when this entry was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User ID who created this entry
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Whether this entry was manually added or automatically tracked
    /// </summary>
    public bool IsManualEntry { get; set; } = true;

    /// <summary>
    /// Duration in minutes (for time-based habits)
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Checks if this entry meets the habit's target value
    /// </summary>
    public bool MeetsTarget()
    {
        if (Habit?.TargetValue == null) return true;
        return ActualValue >= Habit.TargetValue;
    }

    /// <summary>
    /// Calculates the percentage of target achieved
    /// </summary>
    public decimal? GetTargetPercentage()
    {
        if (Habit?.TargetValue == null || ActualValue == null) return null;
        if (Habit.TargetValue == 0) return 100;
        
        return Math.Round((ActualValue.Value / Habit.TargetValue.Value) * 100, 2);
    }
}
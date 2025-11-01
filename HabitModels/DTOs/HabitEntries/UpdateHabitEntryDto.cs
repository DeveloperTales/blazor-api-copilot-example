using System.ComponentModel.DataAnnotations;

namespace HabitModels.DTOs.HabitEntries;

/// <summary>
/// DTO for updating an existing habit entry
/// </summary>
public class UpdateHabitEntryDto
{
    /// <summary>
    /// Date and time when the habit was completed
    /// </summary>
    public DateTime CompletedAt { get; set; }

    /// <summary>
    /// Number of times the habit was completed
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Count { get; set; } = 1;

    /// <summary>
    /// Actual value achieved
    /// </summary>
    public decimal? ActualValue { get; set; }

    /// <summary>
    /// Optional notes about this completion
    /// </summary>
    [StringLength(500)]
    public string? Notes { get; set; }

    /// <summary>
    /// Mood or satisfaction rating (1-10 scale)
    /// </summary>
    [Range(1, 10)]
    public int? Rating { get; set; }

    /// <summary>
    /// Location where the habit was completed
    /// </summary>
    [StringLength(100)]
    public string? Location { get; set; }

    /// <summary>
    /// Weather conditions when habit was completed
    /// </summary>
    [StringLength(50)]
    public string? Weather { get; set; }

    /// <summary>
    /// Duration in minutes (for time-based habits)
    /// </summary>
    public int? DurationMinutes { get; set; }
}
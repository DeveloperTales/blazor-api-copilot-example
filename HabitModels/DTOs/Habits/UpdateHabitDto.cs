using System.ComponentModel.DataAnnotations;
using HabitModels.Models;

namespace HabitModels.DTOs.Habits;

/// <summary>
/// DTO for updating an existing habit
/// </summary>
public class UpdateHabitDto
{
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
    /// Category of the habit
    /// </summary>
    [StringLength(50)]
    public string? Category { get; set; }

    /// <summary>
    /// Target frequency for the habit
    /// </summary>
    public HabitFrequency Frequency { get; set; } = HabitFrequency.Daily;

    /// <summary>
    /// Target count per frequency period
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TargetCount { get; set; } = 1;

    /// <summary>
    /// Unit of measurement for the habit
    /// </summary>
    [StringLength(50)]
    public string? Unit { get; set; }

    /// <summary>
    /// Target value per occurrence
    /// </summary>
    public decimal? TargetValue { get; set; }

    /// <summary>
    /// Color code for visual representation
    /// </summary>
    [StringLength(7)]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color code must be a valid hex color (e.g., #FF5733)")]
    public string? ColorCode { get; set; }

    /// <summary>
    /// Whether the habit is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Optional end date for time-bound habits
    /// </summary>
    public DateTime? EndDate { get; set; }
}
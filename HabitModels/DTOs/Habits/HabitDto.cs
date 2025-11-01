using HabitModels.Models;

namespace HabitModels.DTOs.Habits;

/// <summary>
/// DTO for returning habit information
/// </summary>
public class HabitDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public HabitFrequency Frequency { get; set; }
    public int TargetCount { get; set; }
    public string? Unit { get; set; }
    public decimal? TargetValue { get; set; }
    public string? ColorCode { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int CurrentStreak { get; set; }
    public int TotalEntries { get; set; }
    public bool IsCompletedToday { get; set; }
}
namespace HabitModels.DTOs.HabitEntries;

/// <summary>
/// DTO for returning habit entry information
/// </summary>
public class HabitEntryDto
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public string HabitName { get; set; } = string.Empty;
    public DateTime CompletedAt { get; set; }
    public int Count { get; set; }
    public decimal? ActualValue { get; set; }
    public string? Notes { get; set; }
    public int? Rating { get; set; }
    public string? Location { get; set; }
    public string? Weather { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool IsManualEntry { get; set; }
    public int? DurationMinutes { get; set; }
    public bool MeetsTarget { get; set; }
    public decimal? TargetPercentage { get; set; }
}
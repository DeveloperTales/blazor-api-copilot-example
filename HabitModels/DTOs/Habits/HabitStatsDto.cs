namespace HabitModels.DTOs.Habits;

/// <summary>
/// DTO for habit statistics and summary
/// </summary>
public class HabitStatsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public int TotalCompletions { get; set; }
    public decimal CompletionRate { get; set; }
    public DateTime? LastCompletedAt { get; set; }
    public int DaysActive { get; set; }
    public decimal? AverageValue { get; set; }
    public decimal? BestValue { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace HabitModels.Models;

/// <summary>
/// Represents a user in the habit tracking system
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    [Required]
    [StringLength(450)] // Standard ASP.NET Identity key length
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// User's email address
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's display name
    /// </summary>
    [Required]
    [StringLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// User's first name
    /// </summary>
    [StringLength(50)]
    public string? FirstName { get; set; }

    /// <summary>
    /// User's last name
    /// </summary>
    [StringLength(50)]
    public string? LastName { get; set; }

    /// <summary>
    /// User's time zone identifier (e.g., "America/New_York")
    /// </summary>
    [StringLength(100)]
    public string? TimeZone { get; set; }

    /// <summary>
    /// User's preferred date format
    /// </summary>
    [StringLength(20)]
    public string? DateFormat { get; set; }

    /// <summary>
    /// User's preferred theme (light, dark, auto)
    /// </summary>
    [StringLength(20)]
    public string Theme { get; set; } = "auto";

    /// <summary>
    /// Whether the user wants to receive daily reminders
    /// </summary>
    public bool DailyReminders { get; set; } = true;

    /// <summary>
    /// Whether the user wants to receive weekly summaries
    /// </summary>
    public bool WeeklySummaries { get; set; } = true;

    /// <summary>
    /// Time of day for daily reminders (in user's local time)
    /// </summary>
    public TimeOnly? ReminderTime { get; set; }

    /// <summary>
    /// Date when the user account was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the user account was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date when the user last logged in
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Whether the user account is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// User's profile picture URL
    /// </summary>
    [StringLength(500)]
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Navigation property for user's habits
    /// </summary>
    public virtual ICollection<Habit> Habits { get; set; } = new List<Habit>();

    /// <summary>
    /// Navigation property for user's habit entries
    /// </summary>
    public virtual ICollection<HabitEntry> HabitEntries { get; set; } = new List<HabitEntry>();

    /// <summary>
    /// Gets the user's full name
    /// </summary>
    public string GetFullName()
    {
        if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
        {
            return $"{FirstName} {LastName}";
        }
        return DisplayName;
    }

    /// <summary>
    /// Gets the user's initials for avatar display
    /// </summary>
    public string GetInitials()
    {
        if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
        {
            return $"{FirstName[0]}{LastName[0]}".ToUpper();
        }
        
        var words = DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (words.Length >= 2)
        {
            return $"{words[0][0]}{words[1][0]}".ToUpper();
        }
        
        return DisplayName.Length > 0 ? DisplayName[0].ToString().ToUpper() : "U";
    }
}
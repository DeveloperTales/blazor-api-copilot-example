namespace HabitModels.Models;

/// <summary>
/// Represents the frequency at which a habit should be performed
/// </summary>
public enum HabitFrequency
{
    /// <summary>
    /// Habit should be performed daily
    /// </summary>
    Daily = 1,

    /// <summary>
    /// Habit should be performed weekly
    /// </summary>
    Weekly = 2,

    /// <summary>
    /// Habit should be performed monthly
    /// </summary>
    Monthly = 3,

    /// <summary>
    /// Habit should be performed yearly
    /// </summary>
    Yearly = 4,

    /// <summary>
    /// Custom frequency defined by user
    /// </summary>
    Custom = 5
}
using Microsoft.EntityFrameworkCore;
using HabitAPI.Data;
using HabitModels.DTOs.Habits;
using HabitModels.Models;

namespace HabitAPI.Services;

public interface IHabitService
{
    Task<IEnumerable<HabitDto>> GetUserHabitsAsync(string userId);
    Task<HabitDto?> GetHabitByIdAsync(int id, string userId);
    Task<HabitDto> CreateHabitAsync(CreateHabitDto createHabitDto, string userId);
    Task<HabitDto?> UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto, string userId);
    Task<bool> DeleteHabitAsync(int id, string userId);
    Task<HabitStatsDto?> GetHabitStatsAsync(int id, string userId);
    Task<IEnumerable<HabitDto>> GetActiveHabitsAsync(string userId);
}

public class HabitService : IHabitService
{
    private readonly HabitDbContext _context;

    public HabitService(HabitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HabitDto>> GetUserHabitsAsync(string userId)
    {
        var habits = await _context.Habits
            .Include(h => h.Entries)
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync();

        return habits.Select(MapToDto);
    }

    public async Task<HabitDto?> GetHabitByIdAsync(int id, string userId)
    {
        var habit = await _context.Habits
            .Include(h => h.Entries)
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        return habit != null ? MapToDto(habit) : null;
    }

    public async Task<HabitDto> CreateHabitAsync(CreateHabitDto createHabitDto, string userId)
    {
        var habit = new Habit
        {
            Name = createHabitDto.Name,
            Description = createHabitDto.Description,
            Category = createHabitDto.Category,
            Frequency = createHabitDto.Frequency,
            TargetCount = createHabitDto.TargetCount,
            Unit = createHabitDto.Unit,
            TargetValue = createHabitDto.TargetValue,
            ColorCode = createHabitDto.ColorCode,
            StartDate = createHabitDto.StartDate?.Date ?? DateTime.UtcNow.Date,
            EndDate = createHabitDto.EndDate?.Date,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        // Reload with entries for proper DTO mapping
        habit = await _context.Habits
            .Include(h => h.Entries)
            .FirstAsync(h => h.Id == habit.Id);

        return MapToDto(habit);
    }

    public async Task<HabitDto?> UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto, string userId)
    {
        var habit = await _context.Habits
            .Include(h => h.Entries)
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null) return null;

        habit.Name = updateHabitDto.Name;
        habit.Description = updateHabitDto.Description;
        habit.Category = updateHabitDto.Category;
        habit.Frequency = updateHabitDto.Frequency;
        habit.TargetCount = updateHabitDto.TargetCount;
        habit.Unit = updateHabitDto.Unit;
        habit.TargetValue = updateHabitDto.TargetValue;
        habit.ColorCode = updateHabitDto.ColorCode;
        habit.IsActive = updateHabitDto.IsActive;
        habit.EndDate = updateHabitDto.EndDate?.Date;
        habit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(habit);
    }

    public async Task<bool> DeleteHabitAsync(int id, string userId)
    {
        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null) return false;

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<HabitStatsDto?> GetHabitStatsAsync(int id, string userId)
    {
        var habit = await _context.Habits
            .Include(h => h.Entries)
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null) return null;

        var entries = habit.Entries.OrderBy(e => e.CompletedAt).ToList();
        var today = DateTime.UtcNow.Date;
        var daysActive = (int)(today - habit.StartDate).TotalDays + 1;

        // Calculate completion rate
        var completedDays = entries
            .GroupBy(e => e.CompletedAt.Date)
            .Count(g => g.Sum(e => e.Count) >= habit.TargetCount);
        
        var completionRate = daysActive > 0 ? (decimal)completedDays / daysActive * 100 : 0;

        // Calculate longest streak
        var longestStreak = CalculateLongestStreak(habit, entries);

        return new HabitStatsDto
        {
            Id = habit.Id,
            Name = habit.Name,
            CurrentStreak = habit.CalculateCurrentStreak(),
            LongestStreak = longestStreak,
            TotalCompletions = entries.Count,
            CompletionRate = Math.Round(completionRate, 2),
            LastCompletedAt = entries.LastOrDefault()?.CompletedAt,
            DaysActive = daysActive,
            AverageValue = entries.Where(e => e.ActualValue.HasValue).Any() 
                ? entries.Where(e => e.ActualValue.HasValue).Average(e => e.ActualValue!.Value) 
                : null,
            BestValue = entries.Where(e => e.ActualValue.HasValue).Any() 
                ? entries.Where(e => e.ActualValue.HasValue).Max(e => e.ActualValue!.Value) 
                : null
        };
    }

    public async Task<IEnumerable<HabitDto>> GetActiveHabitsAsync(string userId)
    {
        var habits = await _context.Habits
            .Include(h => h.Entries)
            .Where(h => h.UserId == userId && h.IsActive)
            .OrderBy(h => h.Name)
            .ToListAsync();

        return habits.Select(MapToDto);
    }

    private static HabitDto MapToDto(Habit habit)
    {
        var today = DateTime.UtcNow.Date;
        var todayEntries = habit.Entries.Where(e => e.CompletedAt.Date == today);
        var isCompletedToday = todayEntries.Sum(e => e.Count) >= habit.TargetCount;

        return new HabitDto
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            Category = habit.Category,
            Frequency = habit.Frequency,
            TargetCount = habit.TargetCount,
            Unit = habit.Unit,
            TargetValue = habit.TargetValue,
            ColorCode = habit.ColorCode,
            IsActive = habit.IsActive,
            CreatedAt = habit.CreatedAt,
            UpdatedAt = habit.UpdatedAt,
            StartDate = habit.StartDate,
            EndDate = habit.EndDate,
            UserId = habit.UserId,
            CurrentStreak = habit.CalculateCurrentStreak(),
            TotalEntries = habit.Entries.Count,
            IsCompletedToday = isCompletedToday
        };
    }

    private static int CalculateLongestStreak(Habit habit, List<HabitEntry> entries)
    {
        if (!entries.Any()) return 0;

        var completedDates = entries
            .GroupBy(e => e.CompletedAt.Date)
            .Where(g => g.Sum(e => e.Count) >= habit.TargetCount)
            .Select(g => g.Key)
            .OrderBy(d => d)
            .ToList();

        if (!completedDates.Any()) return 0;

        int longestStreak = 1;
        int currentStreak = 1;

        for (int i = 1; i < completedDates.Count; i++)
        {
            if (completedDates[i] == completedDates[i - 1].AddDays(1))
            {
                currentStreak++;
                longestStreak = Math.Max(longestStreak, currentStreak);
            }
            else
            {
                currentStreak = 1;
            }
        }

        return longestStreak;
    }
}
using Microsoft.EntityFrameworkCore;
using HabitAPI.Data;
using HabitModels.DTOs.HabitEntries;
using HabitModels.Models;

namespace HabitAPI.Services;

public interface IHabitEntryService
{
    Task<IEnumerable<HabitEntryDto>> GetHabitEntriesAsync(int habitId, string userId);
    Task<HabitEntryDto?> GetHabitEntryByIdAsync(int id, string userId);
    Task<HabitEntryDto> CreateHabitEntryAsync(CreateHabitEntryDto createHabitEntryDto, string userId);
    Task<HabitEntryDto?> UpdateHabitEntryAsync(int id, UpdateHabitEntryDto updateHabitEntryDto, string userId);
    Task<bool> DeleteHabitEntryAsync(int id, string userId);
    Task<IEnumerable<HabitEntryDto>> GetUserEntriesForDateAsync(string userId, DateTime date);
    Task<IEnumerable<HabitEntryDto>> GetRecentEntriesAsync(string userId, int days = 7);
}

public class HabitEntryService : IHabitEntryService
{
    private readonly HabitDbContext _context;

    public HabitEntryService(HabitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HabitEntryDto>> GetHabitEntriesAsync(int habitId, string userId)
    {
        var entries = await _context.HabitEntries
            .Include(e => e.Habit)
            .Where(e => e.HabitId == habitId && e.UserId == userId)
            .OrderByDescending(e => e.CompletedAt)
            .ToListAsync();

        return entries.Select(MapToDto);
    }

    public async Task<HabitEntryDto?> GetHabitEntryByIdAsync(int id, string userId)
    {
        var entry = await _context.HabitEntries
            .Include(e => e.Habit)
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        return entry != null ? MapToDto(entry) : null;
    }

    public async Task<HabitEntryDto> CreateHabitEntryAsync(CreateHabitEntryDto createHabitEntryDto, string userId)
    {
        // Verify the habit exists and belongs to the user
        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == createHabitEntryDto.HabitId && h.UserId == userId);

        if (habit == null)
        {
            throw new ArgumentException($"Habit with ID {createHabitEntryDto.HabitId} not found or does not belong to user");
        }

        var entry = new HabitEntry
        {
            HabitId = createHabitEntryDto.HabitId,
            CompletedAt = createHabitEntryDto.CompletedAt ?? DateTime.UtcNow,
            Count = createHabitEntryDto.Count,
            ActualValue = createHabitEntryDto.ActualValue,
            Notes = createHabitEntryDto.Notes,
            Rating = createHabitEntryDto.Rating,
            Location = createHabitEntryDto.Location,
            Weather = createHabitEntryDto.Weather,
            DurationMinutes = createHabitEntryDto.DurationMinutes,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.HabitEntries.Add(entry);
        await _context.SaveChangesAsync();

        // Reload with habit for proper DTO mapping
        entry = await _context.HabitEntries
            .Include(e => e.Habit)
            .FirstAsync(e => e.Id == entry.Id);

        return MapToDto(entry);
    }

    public async Task<HabitEntryDto?> UpdateHabitEntryAsync(int id, UpdateHabitEntryDto updateHabitEntryDto, string userId)
    {
        var entry = await _context.HabitEntries
            .Include(e => e.Habit)
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (entry == null) return null;

        entry.CompletedAt = updateHabitEntryDto.CompletedAt;
        entry.Count = updateHabitEntryDto.Count;
        entry.ActualValue = updateHabitEntryDto.ActualValue;
        entry.Notes = updateHabitEntryDto.Notes;
        entry.Rating = updateHabitEntryDto.Rating;
        entry.Location = updateHabitEntryDto.Location;
        entry.Weather = updateHabitEntryDto.Weather;
        entry.DurationMinutes = updateHabitEntryDto.DurationMinutes;
        entry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(entry);
    }

    public async Task<bool> DeleteHabitEntryAsync(int id, string userId)
    {
        var entry = await _context.HabitEntries
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        if (entry == null) return false;

        _context.HabitEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<HabitEntryDto>> GetUserEntriesForDateAsync(string userId, DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

        var entries = await _context.HabitEntries
            .Include(e => e.Habit)
            .Where(e => e.UserId == userId && 
                       e.CompletedAt >= startOfDay && 
                       e.CompletedAt <= endOfDay)
            .OrderByDescending(e => e.CompletedAt)
            .ToListAsync();

        return entries.Select(MapToDto);
    }

    public async Task<IEnumerable<HabitEntryDto>> GetRecentEntriesAsync(string userId, int days = 7)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-days);

        var entries = await _context.HabitEntries
            .Include(e => e.Habit)
            .Where(e => e.UserId == userId && e.CompletedAt >= cutoffDate)
            .OrderByDescending(e => e.CompletedAt)
            .ToListAsync();

        return entries.Select(MapToDto);
    }

    private static HabitEntryDto MapToDto(HabitEntry entry)
    {
        return new HabitEntryDto
        {
            Id = entry.Id,
            HabitId = entry.HabitId,
            HabitName = entry.Habit?.Name ?? "Unknown",
            CompletedAt = entry.CompletedAt,
            Count = entry.Count,
            ActualValue = entry.ActualValue,
            Notes = entry.Notes,
            Rating = entry.Rating,
            Location = entry.Location,
            Weather = entry.Weather,
            CreatedAt = entry.CreatedAt,
            UpdatedAt = entry.UpdatedAt,
            UserId = entry.UserId,
            IsManualEntry = entry.IsManualEntry,
            DurationMinutes = entry.DurationMinutes,
            MeetsTarget = entry.MeetsTarget(),
            TargetPercentage = entry.GetTargetPercentage()
        };
    }
}
using Microsoft.AspNetCore.Components;
using HabitModels.DTOs.Habits;
using HabitModels.DTOs.HabitEntries;
using HabitWeb.Services;

namespace HabitWeb.Components.Pages;

/// <summary>
/// Code-behind for the Home component (Dashboard)
/// </summary>
public partial class Home : ComponentBase
{
    [Inject] protected IHabitApiService HabitApiService { get; set; } = default!;

    private List<HabitDto> activeHabits = new();
    private bool isLoading = true;
    private int totalHabits = 0;
    private int completedToday = 0;
    private int activeStreaks = 0;
    private int weeklyCompletion = 0;

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardData();
    }

    private async Task LoadDashboardData()
    {
        isLoading = true;
        
        try
        {
            var allHabits = await HabitApiService.GetHabitsAsync();
            activeHabits = await HabitApiService.GetActiveHabitsAsync();
            
            totalHabits = allHabits.Count;
            completedToday = activeHabits.Count(h => h.IsCompletedToday);
            activeStreaks = activeHabits.Count(h => h.CurrentStreak > 0);
            
            // Calculate weekly completion rate
            var totalPossible = activeHabits.Count * 7; // Assuming daily habits for simplicity
            var completed = activeHabits.Sum(h => Math.Min(h.CurrentStreak, 7));
            weeklyCompletion = totalPossible > 0 ? (int)Math.Round((double)completed / totalPossible * 100) : 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading dashboard data: {ex.Message}");
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }

    private async Task MarkComplete(int habitId)
    {
        try
        {
            var entry = new CreateHabitEntryDto
            {
                HabitId = habitId,
                CompletedAt = DateTime.Now,
                Count = 1
            };

            var created = await HabitApiService.CreateHabitEntryAsync(entry);
            if (created != null)
            {
                await LoadDashboardData(); // Reload to update completion status
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error marking habit complete: {ex.Message}");
        }
    }
}
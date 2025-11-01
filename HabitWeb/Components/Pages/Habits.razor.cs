using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using HabitModels.DTOs.Habits;
using HabitModels.DTOs.HabitEntries;
using HabitModels.Models;
using HabitWeb.Services;

namespace HabitWeb.Components.Pages;

/// <summary>
/// Code-behind for the Habits component
/// </summary>
public partial class Habits : ComponentBase
{
    [Inject] protected IHabitApiService HabitApiService { get; set; } = default!;
    [Inject] protected IJSRuntime JSRuntime { get; set; } = default!;

    private List<HabitDto> habits = new();
    private bool isLoading = true;
    private CreateHabitDto habitForm = new();
    private HabitDto? editingHabit = null;

    protected override async Task OnInitializedAsync()
    {
        await LoadHabits();
    }

    private async Task LoadHabits()
    {
        isLoading = true;
        habits = await HabitApiService.GetHabitsAsync();
        isLoading = false;
        StateHasChanged();
    }

    private async Task OpenCreateModal()
    {
        editingHabit = null;
        habitForm = new CreateHabitDto
        {
            ColorCode = "#007bff",
            Frequency = HabitFrequency.Daily,
            TargetCount = 1
        };
        await JSRuntime.InvokeVoidAsync("new bootstrap.Modal", "#habitModal").AsTask();
        await JSRuntime.InvokeVoidAsync("document.getElementById('habitModal').querySelector('.modal').addEventListener", "shown.bs.modal", "function() { document.getElementById('name').focus(); }");
    }

    private async Task EditHabit(HabitDto habit)
    {
        editingHabit = habit;
        habitForm = new CreateHabitDto
        {
            Name = habit.Name,
            Description = habit.Description,
            Category = habit.Category,
            Frequency = habit.Frequency,
            TargetCount = habit.TargetCount,
            Unit = habit.Unit,
            TargetValue = habit.TargetValue,
            ColorCode = habit.ColorCode
        };
        await JSRuntime.InvokeVoidAsync("new bootstrap.Modal", "#habitModal").AsTask();
    }

    private async Task SaveHabit()
    {
        try
        {
            if (editingHabit == null)
            {
                // Create new habit
                var created = await HabitApiService.CreateHabitAsync(habitForm);
                if (created != null)
                {
                    await JSRuntime.InvokeVoidAsync("bootstrap.Modal.getInstance", "#habitModal").AsTask();
                    await JSRuntime.InvokeVoidAsync("document.getElementById('habitModal').querySelector('.btn-close').click");
                    await LoadHabits();
                }
            }
            else
            {
                // Update existing habit
                var updateDto = new UpdateHabitDto
                {
                    Name = habitForm.Name,
                    Description = habitForm.Description,
                    Category = habitForm.Category,
                    Frequency = habitForm.Frequency,
                    TargetCount = habitForm.TargetCount,
                    Unit = habitForm.Unit,
                    TargetValue = habitForm.TargetValue,
                    ColorCode = habitForm.ColorCode,
                    IsActive = editingHabit.IsActive
                };

                var updated = await HabitApiService.UpdateHabitAsync(editingHabit.Id, updateDto);
                if (updated != null)
                {
                    await JSRuntime.InvokeVoidAsync("bootstrap.Modal.getInstance", "#habitModal").AsTask();
                    await JSRuntime.InvokeVoidAsync("document.getElementById('habitModal').querySelector('.btn-close').click");
                    await LoadHabits();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving habit: {ex.Message}");
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
                await LoadHabits(); // Reload to update completion status
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error marking habit complete: {ex.Message}");
        }
    }

    private async Task DeleteHabit(int habitId)
    {
        var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this habit? This action cannot be undone.");
        if (confirmed)
        {
            var deleted = await HabitApiService.DeleteHabitAsync(habitId);
            if (deleted)
            {
                await LoadHabits();
            }
        }
    }

    private void ViewStats(int habitId)
    {
        // TODO: Navigate to stats page or show stats modal
        Console.WriteLine($"View stats for habit {habitId}");
    }
}
using HabitModels.DTOs.Habits;
using HabitModels.DTOs.HabitEntries;

namespace HabitWeb.Services;

public interface IHabitApiService
{
    Task<List<HabitDto>> GetHabitsAsync();
    Task<List<HabitDto>> GetActiveHabitsAsync();
    Task<HabitDto?> GetHabitAsync(int id);
    Task<HabitDto?> CreateHabitAsync(CreateHabitDto createHabitDto);
    Task<HabitDto?> UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto);
    Task<bool> DeleteHabitAsync(int id);
    Task<HabitStatsDto?> GetHabitStatsAsync(int id);
    Task<List<HabitEntryDto>> GetHabitEntriesAsync(int habitId);
    Task<HabitEntryDto?> CreateHabitEntryAsync(CreateHabitEntryDto createHabitEntryDto);
    Task<bool> DeleteHabitEntryAsync(int id);
    Task<List<HabitEntryDto>> GetEntriesForDateAsync(DateTime date);
    Task<List<HabitEntryDto>> GetRecentEntriesAsync(int days = 7);
}

public class HabitApiService : IHabitApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _userId = "test-user-1"; // Temporary until authentication is implemented

    public HabitApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5134")
        };
    }

    public async Task<List<HabitDto>> GetHabitsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<HabitDto>>($"/api/habits?userId={_userId}");
            return response ?? new List<HabitDto>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<HabitDto>();
        }
    }

    public async Task<List<HabitDto>> GetActiveHabitsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<HabitDto>>($"/api/habits/active?userId={_userId}");
            return response ?? new List<HabitDto>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<HabitDto>();
        }
    }

    public async Task<HabitDto?> GetHabitAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<HabitDto>($"/api/habits/{id}?userId={_userId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<HabitDto?> CreateHabitAsync(CreateHabitDto createHabitDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/habits?userId={_userId}", createHabitDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HabitDto>();
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<HabitDto?> UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/habits/{id}?userId={_userId}", updateHabitDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HabitDto>();
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> DeleteHabitAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/habits/{id}?userId={_userId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<HabitStatsDto?> GetHabitStatsAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<HabitStatsDto>($"/api/habits/{id}/stats?userId={_userId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<List<HabitEntryDto>> GetHabitEntriesAsync(int habitId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<HabitEntryDto>>($"/api/habitentries/habit/{habitId}?userId={_userId}");
            return response ?? new List<HabitEntryDto>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<HabitEntryDto>();
        }
    }

    public async Task<HabitEntryDto?> CreateHabitEntryAsync(CreateHabitEntryDto createHabitEntryDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/habitentries?userId={_userId}", createHabitEntryDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HabitEntryDto>();
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> DeleteHabitEntryAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/habitentries/{id}?userId={_userId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<List<HabitEntryDto>> GetEntriesForDateAsync(DateTime date)
    {
        try
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetFromJsonAsync<List<HabitEntryDto>>($"/api/habitentries/date/{dateString}?userId={_userId}");
            return response ?? new List<HabitEntryDto>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<HabitEntryDto>();
        }
    }

    public async Task<List<HabitEntryDto>> GetRecentEntriesAsync(int days = 7)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<HabitEntryDto>>($"/api/habitentries/recent?days={days}&userId={_userId}");
            return response ?? new List<HabitEntryDto>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<HabitEntryDto>();
        }
    }
}
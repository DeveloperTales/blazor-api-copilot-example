using Microsoft.AspNetCore.Mvc;
using HabitModels.DTOs.HabitEntries;
using HabitAPI.Services;

namespace HabitAPI.Controllers;

/// <summary>
/// API controller for managing habit entries
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HabitEntriesController : ControllerBase
{
    private readonly IHabitEntryService _habitEntryService;
    private readonly ILogger<HabitEntriesController> _logger;

    public HabitEntriesController(IHabitEntryService habitEntryService, ILogger<HabitEntriesController> logger)
    {
        _habitEntryService = habitEntryService;
        _logger = logger;
    }

    /// <summary>
    /// Get all entries for a specific habit
    /// </summary>
    /// <param name="habitId">Habit ID</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>List of habit entries</returns>
    [HttpGet("habit/{habitId}")]
    [ProducesResponseType(typeof(IEnumerable<HabitEntryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HabitEntryDto>>> GetHabitEntries([FromRoute] int habitId, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var entries = await _habitEntryService.GetHabitEntriesAsync(habitId, userId);
            return Ok(entries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving entries for habit {HabitId} for user {UserId}", habitId, userId);
            return StatusCode(500, "An error occurred while retrieving habit entries");
        }
    }

    /// <summary>
    /// Get a specific habit entry by ID
    /// </summary>
    /// <param name="id">Entry ID</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>The requested habit entry</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(HabitEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitEntryDto>> GetHabitEntry([FromRoute] int id, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var entry = await _habitEntryService.GetHabitEntryByIdAsync(id, userId);
            
            if (entry == null)
            {
                return NotFound($"Habit entry with ID {id} not found");
            }

            return Ok(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving habit entry {EntryId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while retrieving the habit entry");
        }
    }

    /// <summary>
    /// Create a new habit entry
    /// </summary>
    /// <param name="createHabitEntryDto">Habit entry creation data</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>The created habit entry</returns>
    [HttpPost]
    [ProducesResponseType(typeof(HabitEntryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HabitEntryDto>> CreateHabitEntry([FromBody] CreateHabitEntryDto createHabitEntryDto, [FromQuery] string userId = "test-user-1")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var entry = await _habitEntryService.CreateHabitEntryAsync(createHabitEntryDto, userId);
            return CreatedAtAction(nameof(GetHabitEntry), new { id = entry.Id, userId }, entry);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating habit entry for user {UserId}", userId);
            return StatusCode(500, "An error occurred while creating the habit entry");
        }
    }

    /// <summary>
    /// Update an existing habit entry
    /// </summary>
    /// <param name="id">Entry ID</param>
    /// <param name="updateHabitEntryDto">Habit entry update data</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>The updated habit entry</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(HabitEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitEntryDto>> UpdateHabitEntry([FromRoute] int id, [FromBody] UpdateHabitEntryDto updateHabitEntryDto, [FromQuery] string userId = "test-user-1")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var entry = await _habitEntryService.UpdateHabitEntryAsync(id, updateHabitEntryDto, userId);
            
            if (entry == null)
            {
                return NotFound($"Habit entry with ID {id} not found");
            }

            return Ok(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating habit entry {EntryId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while updating the habit entry");
        }
    }

    /// <summary>
    /// Delete a habit entry
    /// </summary>
    /// <param name="id">Entry ID</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHabitEntry([FromRoute] int id, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var deleted = await _habitEntryService.DeleteHabitEntryAsync(id, userId);
            
            if (!deleted)
            {
                return NotFound($"Habit entry with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting habit entry {EntryId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while deleting the habit entry");
        }
    }

    /// <summary>
    /// Get all habit entries for a specific date
    /// </summary>
    /// <param name="date">Date to filter by (YYYY-MM-DD format)</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>List of habit entries for the specified date</returns>
    [HttpGet("date/{date}")]
    [ProducesResponseType(typeof(IEnumerable<HabitEntryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<HabitEntryDto>>> GetEntriesForDate([FromRoute] DateTime date, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var entries = await _habitEntryService.GetUserEntriesForDateAsync(userId, date);
            return Ok(entries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving entries for date {Date} for user {UserId}", date, userId);
            return StatusCode(500, "An error occurred while retrieving entries for the specified date");
        }
    }

    /// <summary>
    /// Get recent habit entries for the user
    /// </summary>
    /// <param name="days">Number of days to look back (default: 7)</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>List of recent habit entries</returns>
    [HttpGet("recent")]
    [ProducesResponseType(typeof(IEnumerable<HabitEntryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HabitEntryDto>>> GetRecentEntries([FromQuery] int days = 7, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var entries = await _habitEntryService.GetRecentEntriesAsync(userId, days);
            return Ok(entries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving recent entries for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving recent entries");
        }
    }
}
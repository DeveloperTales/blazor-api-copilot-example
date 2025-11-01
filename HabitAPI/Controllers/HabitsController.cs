using Microsoft.AspNetCore.Mvc;
using HabitModels.DTOs.Habits;
using HabitAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace HabitAPI.Controllers;

/// <summary>
/// API controller for managing habits
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HabitsController : ControllerBase
{
    private readonly IHabitService _habitService;
    private readonly ILogger<HabitsController> _logger;

    public HabitsController(IHabitService habitService, ILogger<HabitsController> logger)
    {
        _habitService = habitService;
        _logger = logger;
    }

    /// <summary>
    /// Get all habits for the current user
    /// </summary>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>List of user's habits</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HabitDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetHabits([FromQuery] string userId = "test-user-1")
    {
        try
        {
            var habits = await _habitService.GetUserHabitsAsync(userId);
            return Ok(habits);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving habits for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving habits");
        }
    }

    /// <summary>
    /// Get active habits for the current user
    /// </summary>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>List of user's active habits</returns>
    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<HabitDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetActiveHabits([FromQuery] string userId = "test-user-1")
    {
        try
        {
            var habits = await _habitService.GetActiveHabitsAsync(userId);
            return Ok(habits);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving active habits for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving active habits");
        }
    }

    /// <summary>
    /// Get a specific habit by ID
    /// </summary>
    /// <param name="id">Habit ID</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>The requested habit</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(HabitDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitDto>> GetHabit([FromRoute] int id, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var habit = await _habitService.GetHabitByIdAsync(id, userId);
            
            if (habit == null)
            {
                return NotFound($"Habit with ID {id} not found");
            }

            return Ok(habit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving habit {HabitId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while retrieving the habit");
        }
    }

    /// <summary>
    /// Create a new habit
    /// </summary>
    /// <param name="createHabitDto">Habit creation data</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>The created habit</returns>
    [HttpPost]
    [ProducesResponseType(typeof(HabitDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HabitDto>> CreateHabit([FromBody] CreateHabitDto createHabitDto, [FromQuery] string userId = "test-user-1")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var habit = await _habitService.CreateHabitAsync(createHabitDto, userId);
            return CreatedAtAction(nameof(GetHabit), new { id = habit.Id, userId }, habit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating habit for user {UserId}", userId);
            return StatusCode(500, "An error occurred while creating the habit");
        }
    }

    /// <summary>
    /// Update an existing habit
    /// </summary>
    /// <param name="id">Habit ID</param>
    /// <param name="updateHabitDto">Habit update data</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>The updated habit</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(HabitDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitDto>> UpdateHabit([FromRoute] int id, [FromBody] UpdateHabitDto updateHabitDto, [FromQuery] string userId = "test-user-1")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var habit = await _habitService.UpdateHabitAsync(id, updateHabitDto, userId);
            
            if (habit == null)
            {
                return NotFound($"Habit with ID {id} not found");
            }

            return Ok(habit);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating habit {HabitId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while updating the habit");
        }
    }

    /// <summary>
    /// Delete a habit
    /// </summary>
    /// <param name="id">Habit ID</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHabit([FromRoute] int id, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var deleted = await _habitService.DeleteHabitAsync(id, userId);
            
            if (!deleted)
            {
                return NotFound($"Habit with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting habit {HabitId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while deleting the habit");
        }
    }

    /// <summary>
    /// Get statistics for a specific habit
    /// </summary>
    /// <param name="id">Habit ID</param>
    /// <param name="userId">User ID (temporary parameter until authentication is implemented)</param>
    /// <returns>Habit statistics</returns>
    [HttpGet("{id}/stats")]
    [ProducesResponseType(typeof(HabitStatsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HabitStatsDto>> GetHabitStats([FromRoute] int id, [FromQuery] string userId = "test-user-1")
    {
        try
        {
            var stats = await _habitService.GetHabitStatsAsync(id, userId);
            
            if (stats == null)
            {
                return NotFound($"Habit with ID {id} not found");
            }

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stats for habit {HabitId} for user {UserId}", id, userId);
            return StatusCode(500, "An error occurred while retrieving habit statistics");
        }
    }
}

---
description: 'You are a Senior Software Engineer who specializes in C# Blazor web applications and C# Web APIs. You have been tasked with creating best practice C# Blazor applications and API enhancements for the Habit Tracker project. You have deep expertise in the source code of this product and its architecture.'
---

# Habit Tracker Project - Senior Developer Guidelines

## Project Architecture Overview
You are working on a **Habit Tracker** application with three main projects:
- **HabitAPI**: ASP.NET Core Web API (RESTful services)
- **HabitWeb**: Blazor Server Application (Interactive UI)
- **HabitModels**: Shared class library (DTOs and domain models)

## Documentation Standards

### 🔍 **MANDATORY: All Public Methods Must Include XML Documentation**

Every public method, property, and class MUST include comprehensive XML documentation:

```csharp
/// <summary>
/// Creates a new habit with the specified details and returns the created habit DTO.
/// </summary>
/// <param name="createDto">The habit creation data containing name, description, and settings</param>
/// <returns>
/// A task that represents the asynchronous operation. 
/// The task result contains the created HabitDto if successful, or null if creation failed.
/// </returns>
/// <exception cref="ValidationException">Thrown when the input data is invalid</exception>
/// <exception cref="DuplicateNameException">Thrown when a habit with the same name already exists</exception>
/// <example>
/// <code>
/// var createDto = new CreateHabitDto { Name = "Daily Exercise", TargetCount = 1 };
/// var result = await habitService.CreateHabitAsync(createDto);
/// </code>
/// </example>
public async Task<HabitDto?> CreateHabitAsync(CreateHabitDto createDto)
```

### 📋 **Class Documentation Requirements**

All newly created classes MUST include:

```csharp
/// <summary>
/// Represents a habit tracking service that handles CRUD operations for habits.
/// This service acts as the business logic layer between the API controllers and data access.
/// </summary>
/// <remarks>
/// The service implements the repository pattern and includes validation, 
/// error handling, and data transformation between domain models and DTOs.
/// 
/// Key responsibilities:
/// - Habit creation, updating, and deletion
/// - Data validation and business rule enforcement
/// - DTO to domain model mapping
/// - Error handling and logging
/// </remarks>
/// <example>
/// Usage in dependency injection:
/// <code>
/// services.AddScoped&lt;IHabitService, HabitService&gt;();
/// </code>
/// </example>
public class HabitService : IHabitService
```

### 🏗️ **Interface Documentation Standards**

```csharp
/// <summary>
/// Defines the contract for habit management operations in the Habit Tracker application.
/// </summary>
/// <remarks>
/// This interface provides methods for managing habits including creation, retrieval,
/// updating, and deletion. All methods are asynchronous and return appropriate DTOs.
/// 
/// Implementations should handle:
/// - Input validation
/// - Business rule enforcement
/// - Error handling and logging
/// - Data transformation
/// </remarks>
public interface IHabitService
{
    /// <summary>
    /// Retrieves all active habits for the current user.
    /// </summary>
    /// <returns>A list of habit DTOs representing all active habits</returns>
    Task<List<HabitDto>> GetHabitsAsync();
}
```

## Code Quality Standards

### ✅ **Required Patterns for All Code:**

1. **Async/Await Pattern**:
   ```csharp
   /// <summary>
   /// Asynchronously loads dashboard statistics including completion rates and streaks.
   /// </summary>
   /// <returns>A task representing the asynchronous load operation</returns>
   private async Task LoadDashboardDataAsync()
   ```

2. **Proper Error Handling**:
   ```csharp
   /// <summary>
   /// Handles habit completion marking with comprehensive error handling.
   /// </summary>
   /// <param name="habitId">The unique identifier of the habit to mark complete</param>
   /// <returns>A task representing the asynchronous operation</returns>
   /// <exception cref="ArgumentException">Thrown when habitId is invalid</exception>
   /// <exception cref="NotFoundException">Thrown when the habit is not found</exception>
   private async Task MarkHabitCompleteAsync(int habitId)
   {
       try
       {
           // Implementation with proper validation
       }
       catch (Exception ex)
       {
           _logger.LogError(ex, "Failed to mark habit {HabitId} as complete", habitId);
           throw;
       }
   }
   ```

3. **Dependency Injection Documentation**:
   ```csharp
   /// <summary>
   /// Initializes a new instance of the HabitService class.
   /// </summary>
   /// <param name="context">The database context for habit data operations</param>
   /// <param name="logger">The logger for recording service operations and errors</param>
   /// <exception cref="ArgumentNullException">
   /// Thrown when context or logger is null
   /// </exception>
   public HabitService(HabitDbContext context, ILogger<HabitService> logger)
   ```

## Blazor Component Documentation

### 📝 **Component Class Documentation**:

```csharp
/// <summary>
/// Code-behind class for the Habits management component.
/// Handles habit CRUD operations, form validation, and UI state management.
/// </summary>
/// <remarks>
/// This component provides:
/// - Habit listing with real-time updates
/// - Create/Edit habit modal forms
/// - Habit completion tracking
/// - Delete confirmation workflows
/// - Loading states and error handling
/// 
/// The component follows the code-behind pattern for clean separation of concerns.
/// </remarks>
public partial class Habits : ComponentBase
{
    /// <summary>
    /// Gets or sets the habit API service for data operations.
    /// Injected via dependency injection container.
    /// </summary>
    [Inject] protected IHabitApiService HabitApiService { get; set; } = default!;
}
```

### 🎯 **Component Method Documentation**:

```csharp
/// <summary>
/// Initializes the component by loading all habits from the API.
/// Called automatically when the component is first rendered.
/// </summary>
/// <returns>A task representing the asynchronous initialization</returns>
protected override async Task OnInitializedAsync()

/// <summary>
/// Opens the create habit modal with a new empty form.
/// Resets any previous form data and focuses on the name input field.
/// </summary>
/// <returns>A task representing the asynchronous modal opening operation</returns>
private async Task OpenCreateModalAsync()

/// <summary>
/// Saves the current habit form data by either creating a new habit or updating an existing one.
/// Validates the form data and shows appropriate success/error messages.
/// </summary>
/// <returns>A task representing the asynchronous save operation</returns>
/// <exception cref="ValidationException">Thrown when form data is invalid</exception>
private async Task SaveHabitAsync()
```

## API Controller Documentation

### 🌐 **Controller Documentation Standards**:

```csharp
/// <summary>
/// API controller for managing habit-related operations in the Habit Tracker application.
/// Provides RESTful endpoints for habit CRUD operations with proper HTTP status codes.
/// </summary>
/// <remarks>
/// This controller handles:
/// - GET /api/habits - Retrieve all habits
/// - GET /api/habits/{id} - Retrieve specific habit
/// - POST /api/habits - Create new habit
/// - PUT /api/habits/{id} - Update existing habit
/// - DELETE /api/habits/{id} - Delete habit
/// 
/// All endpoints include proper validation, error handling, and Swagger documentation.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase

/// <summary>
/// Retrieves all habits for the current user with pagination support.
/// </summary>
/// <param name="page">The page number for pagination (default: 1)</param>
/// <param name="pageSize">The number of items per page (default: 10, max: 100)</param>
/// <returns>
/// A paginated list of habit DTOs with metadata including total count and page information
/// </returns>
/// <response code="200">Returns the paginated list of habits</response>
/// <response code="400">Bad request due to invalid pagination parameters</response>
/// <response code="500">Internal server error occurred</response>
/// <example>
/// GET /api/habits?page=1&amp;pageSize=20
/// </example>
[HttpGet]
[ProducesResponseType(typeof(PagedResult<HabitDto>), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<PagedResult<HabitDto>>> GetHabits(int page = 1, int pageSize = 10)
```

## DTO Documentation Standards

### 📊 **DTO Class Documentation**:

```csharp
/// <summary>
/// Data Transfer Object for creating new habits in the Habit Tracker application.
/// Contains all required and optional properties for habit creation with validation attributes.
/// </summary>
/// <remarks>
/// This DTO is used for:
/// - API endpoint input validation
/// - Form binding in Blazor components
/// - Data transformation to domain models
/// 
/// All properties include appropriate validation attributes for both client and server-side validation.
/// </remarks>
/// <example>
/// <code>
/// var createDto = new CreateHabitDto
/// {
///     Name = "Daily Exercise",
///     Description = "30 minutes of cardio exercise",
///     Frequency = HabitFrequency.Daily,
///     TargetCount = 1,
///     ColorCode = "#007bff"
/// };
/// </code>
/// </example>
public class CreateHabitDto
{
    /// <summary>
    /// Gets or sets the name of the habit.
    /// Must be unique per user and between 1-100 characters.
    /// </summary>
    /// <example>Daily Exercise</example>
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
}
```

## Performance and Security Documentation

### ⚡ **Performance Documentation Requirements**:

```csharp
/// <summary>
/// Efficiently retrieves habit statistics with optimized database queries.
/// Uses projection to minimize data transfer and includes caching for frequently accessed data.
/// </summary>
/// <param name="habitId">The habit identifier for statistics calculation</param>
/// <param name="dateRange">The date range for statistics (default: last 30 days)</param>
/// <returns>Comprehensive habit statistics including completion rates and streaks</returns>
/// <remarks>
/// Performance considerations:
/// - Uses database projection to limit data transfer
/// - Implements result caching with 5-minute expiration
/// - Optimized for habits with large entry datasets
/// - Average execution time: &lt;100ms for typical datasets
/// </remarks>
public async Task<HabitStatsDto> GetHabitStatisticsAsync(int habitId, DateRange dateRange = null)
```

### 🔒 **Security Documentation Requirements**:

```csharp
/// <summary>
/// Validates and sanitizes user input for habit creation to prevent security vulnerabilities.
/// Implements comprehensive input validation and sanitization.
/// </summary>
/// <param name="input">The user-provided habit data requiring validation</param>
/// <returns>Validated and sanitized habit data ready for processing</returns>
/// <remarks>
/// Security measures implemented:
/// - Input sanitization to prevent XSS attacks
/// - SQL injection prevention through parameterized queries
/// - Business rule validation (duplicate name checks, etc.)
/// - Rate limiting for habit creation (max 10 per minute per user)
/// - Audit logging for all habit modifications
/// </remarks>
/// <exception cref="SecurityException">Thrown when malicious input is detected</exception>
public async Task<ValidatedHabitData> ValidateAndSanitizeInputAsync(CreateHabitDto input)
```

## Code Review Checklist

### ✅ **Before Submitting Code, Ensure:**

1. **Documentation Complete**:
   - [ ] All public methods have XML documentation
   - [ ] All classes have comprehensive summaries
   - [ ] All interfaces are documented with contracts
   - [ ] Examples provided for complex methods
   - [ ] Performance notes included where relevant
   - [ ] Security considerations documented

2. **Code Quality**:
   - [ ] Async/await used for all I/O operations
   - [ ] Proper error handling with logging
   - [ ] Dependency injection properly implemented
   - [ ] Validation attributes on all DTOs
   - [ ] Unit tests written for new functionality

3. **Architecture Compliance**:
   - [ ] Code-behind pattern used for Blazor components
   - [ ] DTOs separated from domain models
   - [ ] Service layer implements business logic
   - [ ] Controllers are thin and focused on HTTP concerns

## Summary

Always prioritize **comprehensive documentation**, **clean architecture**, and **security best practices**. Every piece of code should be self-documenting through proper XML documentation, making it easy for other developers to understand and maintain the Habit Tracker application.

# AI Agent Instructions for Habit Tracker Project

## Project Context
This is a **Habit Tracker** application built with .NET 8, consisting of three main projects:
- **HabitAPI**: ASP.NET Core Web API (Port 5134)
- **HabitWeb**: Blazor Server Application (Port 5000)
- **HabitModels**: Shared class library for DTOs and domain models

## Core Architecture Principles

### 1. Project Structure Understanding
```
HabitProject.sln
├── HabitAPI/          # RESTful Web API
│   ├── Controllers/   # API Controllers
│   ├── Services/      # Business logic
│   └── Data/         # DbContext and configurations
├── HabitWeb/         # Blazor Server UI
│   ├── Components/   # Blazor components
│   ├── Services/     # API client services
│   └── wwwroot/      # Static assets
└── HabitModels/      # Shared models
    ├── Models/       # Domain entities
    └── DTOs/         # Data Transfer Objects
        ├── Habits/
        └── HabitEntries/
```

### 2. Code Organization Standards
- **Separation of Concerns**: Always separate business logic from presentation
- **Code-Behind Pattern**: Use `.razor.cs` files for Blazor component logic
- **Clean Architecture**: Keep DTOs separate from domain models
- **Dependency Injection**: Use constructor injection for all services

## Coding Guidelines

### C# Standards
```csharp
// ✅ PREFERRED PATTERNS
public class ExampleService
{
    private readonly IRepository _repository;
    
    public ExampleService(IRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<Result<T>> ProcessAsync(Request request)
    {
        try
        {
            // Always use async/await for I/O operations
            var result = await _repository.GetAsync(request.Id);
            return result != null ? Result.Success(result) : Result.NotFound();
        }
        catch (Exception ex)
        {
            // Proper error handling and logging
            _logger.LogError(ex, "Error processing request for {Id}", request.Id);
            return Result.Error("Processing failed");
        }
    }
}
```

### Blazor Component Standards
```csharp
// ✅ Code-behind file: Component.razor.cs
public partial class ComponentName : ComponentBase
{
    [Inject] protected IService Service { get; set; } = default!;
    
    private bool isLoading = true;
    private List<DataModel> items = new();
    
    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }
    
    private async Task LoadDataAsync()
    {
        isLoading = true;
        try
        {
            items = await Service.GetItemsAsync();
        }
        catch (Exception ex)
        {
            // Handle errors gracefully
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
}
```

### API Controller Standards
```csharp
[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly IHabitService _habitService;
    
    public HabitsController(IHabitService habitService)
    {
        _habitService = habitService;
    }
    
    /// <summary>
    /// Gets all habits for the current user
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<HabitDto>>> GetHabits()
    {
        try
        {
            var habits = await _habitService.GetHabitsAsync();
            return Ok(habits);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving habits");
            return StatusCode(500, "Internal server error");
        }
    }
}
```

## Data Transfer Object Patterns

### Naming Conventions
- **Create Operations**: `CreateHabitDto`
- **Update Operations**: `UpdateHabitDto`
- **Read Operations**: `HabitDto`
- **Statistics**: `HabitStatsDto`

### Validation Standards
```csharp
public class CreateHabitDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Target count must be positive")]
    public int TargetCount { get; set; } = 1;
    
    [Required]
    public HabitFrequency Frequency { get; set; } = HabitFrequency.Daily;
}
```

## UI/UX Standards

### Bootstrap 5 Usage
```html
<!-- ✅ PREFERRED CARD STRUCTURE -->
<div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <h5 class="mb-0">
            <i class="fas fa-target me-2"></i>Title
        </h5>
        <button class="btn btn-sm btn-primary">
            <i class="fas fa-plus me-1"></i>Add
        </button>
    </div>
    <div class="card-body">
        @if (isLoading)
        {
            <div class="text-center">
                <div class="spinner-border" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>
        }
        else
        {
            <!-- Content here -->
        }
    </div>
</div>
```

### Icon Standards (Font Awesome)
- **Add**: `fas fa-plus`
- **Edit**: `fas fa-edit`
- **Delete**: `fas fa-trash`
- **Complete**: `fas fa-check`
- **Streak**: `fas fa-fire`
- **Statistics**: `fas fa-chart-bar`
- **Calendar**: `fas fa-calendar`
- **Settings**: `fas fa-cog`

### Form Patterns
```html
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <div class="mb-3">
        <label for="field" class="form-label">Field Name</label>
        <InputText id="field" class="form-control" @bind-Value="model.Field" />
        <ValidationMessage For="() => model.Field" class="text-danger" />
    </div>
    
    <div class="d-flex justify-content-end gap-2">
        <button type="button" class="btn btn-secondary">Cancel</button>
        <button type="submit" class="btn btn-primary">Save</button>
    </div>
</EditForm>
```

## Error Handling Guidelines

### API Error Responses
```csharp
// ✅ Consistent error handling
try
{
    var result = await service.ProcessAsync(request);
    return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
}
catch (ValidationException ex)
{
    return BadRequest(ex.Message);
}
catch (NotFoundException ex)
{
    return NotFound(ex.Message);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Unexpected error in {Method}", nameof(MethodName));
    return StatusCode(500, "An unexpected error occurred");
}
```

### Blazor Error Handling
```csharp
private async Task HandleAction()
{
    try
    {
        await PerformActionAsync();
        // Show success message if needed
    }
    catch (HttpRequestException ex)
    {
        errorMessage = "Network error. Please try again.";
        Console.WriteLine($"HTTP Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        errorMessage = "An unexpected error occurred.";
        Console.WriteLine($"Error: {ex.Message}");
    }
}
```

## Testing Patterns

### Unit Test Structure
```csharp
[TestClass]
public class HabitServiceTests
{
    private Mock<IHabitRepository> _mockRepository;
    private HabitService _service;
    
    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IHabitRepository>();
        _service = new HabitService(_mockRepository.Object);
    }
    
    [TestMethod]
    public async Task GetHabitsAsync_ShouldReturnHabits_WhenHabitsExist()
    {
        // Arrange
        var expectedHabits = CreateTestHabits();
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedHabits);
        
        // Act
        var result = await _service.GetHabitsAsync();
        
        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedHabits.Count, result.Count);
    }
}
```

## Security Guidelines

### Input Validation
- **Always validate** on both client and server
- Use **data annotations** for model validation
- **Sanitize** user input to prevent XSS
- Implement **CSRF protection** for forms

### API Security
```csharp
// ✅ Proper model validation
[HttpPost]
public async Task<ActionResult<HabitDto>> CreateHabit([FromBody] CreateHabitDto dto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
    
    // Additional business validation
    if (await _service.HabitExistsAsync(dto.Name))
    {
        return Conflict("A habit with this name already exists");
    }
    
    var result = await _service.CreateHabitAsync(dto);
    return CreatedAtAction(nameof(GetHabit), new { id = result.Id }, result);
}
```

## Performance Guidelines

### Database Operations
- Use **async/await** for all database operations
- Implement **pagination** for large datasets
- Use **Include()** for eager loading when needed
- Consider **projection** to limit data transfer

### Blazor Performance
- Use **@rendermode InteractiveServer** appropriately
- Implement **StateHasChanged()** only when necessary
- Avoid **unnecessary re-renders**
- Use **virtualization** for large lists

## Agent-Specific Instructions

### When Creating New Features:
1. **Start with the domain model** in HabitModels
2. **Create DTOs** for API contracts
3. **Implement API controller** with full CRUD operations
4. **Add service layer** for business logic
5. **Create Blazor components** with code-behind separation
6. **Add comprehensive error handling**
7. **Include proper validation**
8. **Add appropriate tests**

### When Modifying Existing Code:
1. **Maintain existing patterns** and conventions
2. **Update all related files** (DTOs, controllers, services, components)
3. **Preserve error handling** patterns
4. **Update tests** accordingly
5. **Follow the same naming conventions**

### When Debugging Issues:
1. **Check console logs** for error messages
2. **Verify API endpoints** are responding correctly
3. **Ensure proper dependency injection** setup
4. **Validate data flow** between layers
5. **Test both client and server validation**

## Common Pitfalls to Avoid

### ❌ AVOID:
- Mixing business logic in Blazor components
- Using `@code` blocks in Blazor components (use code-behind)
- Inconsistent error handling patterns
- Missing validation on API endpoints
- Hardcoded connection strings or API URLs
- Synchronous database operations
- Missing nullable reference type annotations

### ✅ PREFER:
- Clean separation of concerns
- Consistent naming conventions
- Comprehensive error handling
- Proper dependency injection
- Async/await patterns
- Code-behind for component logic
- Proper validation at all layers

## File Templates

### API Controller Template
```csharp
[ApiController]
[Route("api/[controller]")]
public class {EntityName}Controller : ControllerBase
{
    private readonly I{EntityName}Service _service;
    private readonly ILogger<{EntityName}Controller> _logger;
    
    // Constructor, CRUD methods with proper error handling
}
```

### Service Template
```csharp
public interface I{EntityName}Service
{
    Task<List<{EntityName}Dto>> GetAllAsync();
    Task<{EntityName}Dto?> GetByIdAsync(int id);
    Task<{EntityName}Dto?> CreateAsync(Create{EntityName}Dto dto);
    Task<{EntityName}Dto?> UpdateAsync(int id, Update{EntityName}Dto dto);
    Task<bool> DeleteAsync(int id);
}
```

### Blazor Component Template
```csharp
public partial class {ComponentName} : ComponentBase
{
    [Inject] protected I{Service}Service Service { get; set; } = default!;
    
    // Component logic with proper error handling and loading states
}
```

## Summary
Always prioritize **clean code**, **proper separation of concerns**, **comprehensive error handling**, and **consistent patterns** throughout the application. When in doubt, follow the existing patterns established in the codebase and refer to these guidelines for consistency.

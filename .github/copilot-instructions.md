# GitHub Copilot Instructions for Habit Tracker Project

## Project Overview
This is a comprehensive Habit Tracker application built with .NET 8, featuring:
- **HabitAPI**: ASP.NET Core Web API with RESTful endpoints
- **HabitWeb**: Blazor Server application with interactive UI
- **HabitModels**: Shared class library for DTOs and domain models

## Architecture Guidelines

### Project Structure
```
HabitProject.sln
├── HabitAPI/          # ASP.NET Core Web API
├── HabitWeb/          # Blazor Server Application  
└── HabitModels/       # Shared Models and DTOs
    ├── Models/        # Domain entities
    └── DTOs/          # Data Transfer Objects
        ├── Habits/
        └── HabitEntries/
```

### Code Organization Principles
1. **Separation of Concerns**: Use code-behind files (`.razor.cs`) for Blazor components
2. **Clean Architecture**: Keep DTOs separate from domain models
3. **Dependency Injection**: Use constructor injection for services
4. **Repository Pattern**: Service layer handles data access logic

## Coding Standards

### C# Guidelines
- Use **nullable reference types** and proper null checking
- Follow **async/await** patterns for all I/O operations
- Use **record types** for DTOs when immutability is desired
- Implement **proper error handling** with try-catch blocks
- Use **meaningful variable names** and method names

### Blazor Component Guidelines
- **Code-behind pattern**: Separate logic from markup using `.razor.cs` files
- **Component structure**:
  ```csharp
  public partial class ComponentName : ComponentBase
  {
      [Inject] protected IService Service { get; set; } = default!;
      
      protected override async Task OnInitializedAsync()
      {
          // Component initialization
      }
  }
  ```
- Use **Bootstrap 5** classes for styling
- Include **Font Awesome** icons for visual elements
- Implement **loading states** and **error handling** in UI

### API Controller Guidelines
- Use **RESTful conventions** (GET, POST, PUT, DELETE)
- Return appropriate **HTTP status codes**
- Include **Swagger documentation** with XML comments
- Use **ActionResult<T>** return types
- Implement **model validation** with data annotations

## Entity Framework Patterns

### DbContext Configuration
- Use **Entity Framework Core In-Memory** for development
- Implement **seed data** for testing
- Use **fluent API** for complex configurations
- Follow **convention over configuration** when possible

### Entity Relationships
```csharp
// One-to-Many relationship example
public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<HabitEntry> Entries { get; set; } = new();
}

public class HabitEntry  
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public Habit Habit { get; set; } = null!;
}
```

## Service Layer Patterns

### API Service Implementation
```csharp
public interface IHabitApiService
{
    Task<List<HabitDto>> GetHabitsAsync();
    Task<HabitDto?> CreateHabitAsync(CreateHabitDto dto);
    Task<HabitDto?> UpdateHabitAsync(int id, UpdateHabitDto dto);
    Task<bool> DeleteHabitAsync(int id);
}
```

### Error Handling
- Use **try-catch** blocks in service methods
- Log errors to console for development
- Return **null** or **false** for failed operations
- Handle **HTTP exceptions** gracefully

## UI/UX Guidelines

### Bootstrap Components
- Use **card components** for content organization
- Implement **responsive grid system** (col-lg, col-md, col-sm)
- Use **button classes** consistently (btn-primary, btn-outline-secondary)
- Include **loading spinners** and **progress indicators**

### Icon Usage
- Use **Font Awesome** icons consistently:
  - `fas fa-plus` for add actions
  - `fas fa-edit` for edit actions  
  - `fas fa-trash` for delete actions
  - `fas fa-check` for completion actions
  - `fas fa-fire` for streak indicators

### Form Patterns
```html
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <div class="mb-3">
        <label for="field" class="form-label">Label</label>
        <InputText id="field" class="form-control" @bind-Value="model.Field" />
        <ValidationMessage For="() => model.Field" />
    </div>
</EditForm>
```

## Data Transfer Objects (DTOs)

### Naming Conventions
- **Create**: `CreateHabitDto` for creation operations
- **Update**: `UpdateHabitDto` for update operations  
- **Response**: `HabitDto` for read operations
- **Stats**: `HabitStatsDto` for calculated data

### Validation Attributes
```csharp
public class CreateHabitDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Range(1, int.MaxValue, ErrorMessage = "Target count must be positive")]
    public int TargetCount { get; set; } = 1;
}
```

## Testing Guidelines

### Component Testing
- Test component **lifecycle methods**
- Verify **data binding** works correctly
- Test **user interactions** and **event handlers**
- Mock **external dependencies** properly

### API Testing
- Test all **CRUD operations**
- Verify **HTTP status codes**
- Test **validation scenarios**
- Include **edge cases** and **error conditions**

## Development Workflow

### File Creation Order
1. Create **domain models** first
2. Add **DTOs** for API contracts
3. Implement **API controllers** and **services**
4. Build **Blazor components** with **code-behind**
5. Add **styling** and **responsive design**

### Common Patterns to Follow
- **Repository pattern** for data access
- **Service layer** for business logic
- **DTO mapping** between layers
- **Async/await** for all I/O operations
- **Dependency injection** for loose coupling

## Performance Considerations

### Blazor Optimization
- Use **@rendermode InteractiveServer** appropriately
- Implement **StateHasChanged()** when needed
- Avoid **unnecessary re-renders**
- Use **virtualization** for large lists

### API Optimization
- Implement **pagination** for large datasets
- Use **projection** to limit data transfer
- Consider **caching** for frequently accessed data
- Optimize **database queries**

## Security Best Practices

### Input Validation
- **Always validate** user input on both client and server
- Use **data annotations** for model validation
- Implement **CSRF protection** for forms
- **Sanitize** user input to prevent XSS

### API Security
- Use **HTTPS** in production
- Implement **authentication** and **authorization**
- Validate **request models** thoroughly
- Log **security events** appropriately

## Helpful Snippets

### Common Blazor Patterns
```csharp
// Loading state management
private bool isLoading = true;

protected override async Task OnInitializedAsync()
{
    isLoading = true;
    try
    {
        await LoadData();
    }
    finally
    {
        isLoading = false;
        StateHasChanged();
    }
}
```

### API Error Handling
```csharp
try
{
    var result = await service.CreateAsync(dto);
    return result != null ? Ok(result) : BadRequest("Creation failed");
}
catch (Exception ex)
{
    logger.LogError(ex, "Error creating entity");
    return StatusCode(500, "Internal server error");
}
```

Remember: This project emphasizes **clean architecture**, **separation of concerns**, and **modern .NET practices**. Always prefer **explicit over implicit**, **async over sync**, and **composition over inheritance**.

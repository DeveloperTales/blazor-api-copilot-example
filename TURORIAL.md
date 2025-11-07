# 🎓 GitHub Copilot Pair Programming Tutorial

A comprehensive hands-on tutorial for learning GitHub Copilot as a pair programmer using the Habit Tracker application. This guide walks you through real-world development scenarios demonstrating AI-assisted development workflows.

## 📚 Tutorial Overview

This tutorial covers:
- 🤖 **Using GitHub Copilot as a pair programmer** in real development scenarios
- 🐛 **Debugging and fixing issues** with AI assistance
- 🧪 **Creating unit tests** with AI-guided framework selection
- ✨ **Adding new features** from concept to implementation
- 📝 **Generating documentation** for existing code

**Estimated Time**: 60-90 minutes

## 🎯 Prerequisites

Before starting this tutorial, ensure you have:

- ✅ Completed the [Getting Started](README.md#getting-started) section
- ✅ Both HabitAPI and HabitWeb applications running
- ✅ [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) installed and activated
- ✅ Basic familiarity with C#, Blazor, and ASP.NET Core

## Fresh Start Option

If you've been experimenting with the code and want to start with a clean slate, or if you want to work with enhanced GitHub Copilot instructions, you can switch to the `added-instructions` branch:

```bash
# Switch to the branch with copilot instructions and clean code
git checkout added-instructions

# Verify you're on the correct branch
git branch

# Build and run to ensure everything works
dotnet build
```

**What you'll get in the `added-instructions` branch:**
- 📋 **Enhanced Copilot Instructions**: Comprehensive `.github/copilot-instructions.md` file with project-specific guidelines
- 🧹 **Clean Codebase**: No experimental changes or modifications
- 🎯 **Optimized for Learning**: Code structured specifically for the tutorial exercises
- 📚 **Better AI Context**: Copilot will have detailed understanding of the project architecture and coding standards

**When to use this branch:**
- ✅ You're starting the tutorial for the first time
- ✅ You've made changes to the main branch and want a fresh start
- ✅ You want to experience Copilot with enhanced project context
- ✅ You're running this tutorial in a workshop or classroom setting

**Note**: You can always switch back to `main` with `git checkout main` if needed.

## 🚀 Exercise 1: Getting Started with Copilot

### Objective
Familiarize yourself with using GitHub Copilot for basic application operations.

### Steps

1. **Launch Both Applications**
   ```bash
   # Terminal 1 - API
   cd HabitAPI
   dotnet run
   
   # Terminal 2 - Web App
   cd HabitWeb
   dotnet run
   ```

2. **Test Basic Functionality**
   - Navigate to [http://localhost:5000](http://localhost:5000)
   - Create a new habit (e.g., "Morning Jog", Daily, Target: 1)
   - Mark it as complete for today
   - Verify the habit appears on your dashboard

3. **Observe Copilot Suggestions**
   - Open `HabitWeb/Components/Pages/Habits.razor.cs`
   - Notice how Copilot suggests completions as you type
   - Try creating a new method and see how Copilot predicts your intent

**💡 Tip**: Press `Tab` to accept Copilot suggestions, `Esc` to dismiss them.

---

## 🐛 Exercise 2: Debugging with Copilot Agent

### Objective
Use GitHub Copilot Agent mode to identify and fix a bug in the habit deletion workflow.

> 💡 **Fresh Start Tip**: If you've made changes and want to start this exercise with a clean codebase, consider switching to the `added-instructions` branch as described in the [Fresh Start Option](#-fresh-start-option) section above.

### The Problem

When deleting a habit, the application removes it successfully but incorrectly navigates to the home page instead of staying on the Habits page.

### Steps

1. **Reproduce the Bug**
   - Navigate to the Habits page
   - Create a test habit
   - Click the three-dot menu (⋮) and select "Delete"
   - Confirm deletion
   - **❌ Observe**: You're redirected to Home instead of staying on Habits

2. **Open GitHub Copilot Chat**
   - Press `Ctrl+Alt+I` (Windows) or `Cmd+Shift+I` (Mac) to open Copilot Chat
   - Switch to **Agent Mode** (look for the mode selector in the chat interface)
   - Select your preferred agent (e.g., `@workspace`)

3. **Describe the Issue to Copilot**
   
   **Prompt**:
   ```
   When deleting a habit, it gets removed successfully but navigates to the 
   home page instead of staying on the current page. Can you help me fix this?
   ```

4. **Review Copilot's Analysis**
   - Copilot should identify the issue in `Habits.razor`
   - Look for the delete dropdown menu item
   - The problem is likely an incorrect `href` attribute

5. **Expected Fix Location**: `HabitWeb/Components/Pages/Habits.razor`
   
   **Before** (around line 80-85):
   ```razor
   <a class="dropdown-item text-danger" href="#" @onclick="() => DeleteHabit(habit.Id)">
      <i class="fas fa-trash me-2"></i>Delete
   </a>
   ```
   
   **After**:
   ```razor
   <a class="dropdown-item text-danger" href="#" @onclick="() => DeleteHabit(habit.Id)" @onclick:preventDefault="true">
      <i class="fas fa-trash me-2"></i>Delete
   </a>
   ```

6. **Test the Fix**
   - Rebuild and restart the application
   - Delete a habit again
   - **✅ Verify**: You stay on the Habits page

### 🎓 Learning Points

- Copilot Agent can analyze code context across multiple files
- Describing behavior issues (not just code) helps Copilot understand intent
- Always test AI-suggested fixes before committing

---

## 🧪 Exercise 3: Creating a Unit Test Project

### Objective
Use Copilot to set up a professional unit test project with appropriate frameworks and mocking libraries.

### Steps

1. **Initiate the Conversation**
   
   Open Copilot Chat in Agent Mode and use this prompt:
   
   **Prompt**:
   ```
   You are a Senior Software Engineer specializing in unit testing. We need 
   unit tests for the HabitAPI project. Help me create a new test project 
   without any tests yet. Let's decide together on the test framework and 
   mocking libraries we'll use. Please list and explain each framework option 
   with recommendations, and do the same for mocking libraries.
   ```

2. **Review Copilot's Recommendations**
   
   Copilot should present options like:
   
   **Test Frameworks**:
   - **xUnit** (Recommended) - Modern, extensible, widely adopted
   - **NUnit** - Mature, feature-rich, good for complex scenarios
   - **MSTest** - Microsoft's official framework, integrated with Visual Studio
   
   **Mocking Libraries**:
   - **Moq** (Recommended) - Simple, fluent API, most popular
   - **NSubstitute** - Clean syntax, easy to learn
   - **FakeItEasy** - Discoverable API, beginner-friendly

3. **Accept the Recommendations**
   
   When Copilot asks if you want to proceed with its recommendations, confirm:
   
   **Response**: `Yes, please create the project with xUnit and Moq`

4. **Verify Project Creation**
   
   Copilot should create:
   - New project: `HabitAPI.Tests/HabitAPI.Tests.csproj`
   - Package references for xUnit, Moq, and test infrastructure
   - Basic project structure with helpers like `TestDbContextFactory.cs`

5. **Build the Solution**
   ```bash
   dotnet build
   ```
   
   **✅ Verify**: No build errors

### 🎓 Learning Points

- Copilot can guide technology selection with pros/cons analysis
- AI can scaffold entire project structures following best practices
- Collaborative prompting (asking for options) yields better results

---

## ✅ Exercise 4: Writing Unit Tests with Copilot

### Objective
Create comprehensive unit tests for the `HabitService` class using AI assistance.

### Steps

1. **Open Relevant Files**
   - Open `HabitAPI/Services/HabitService.cs`
   - Open `HabitAPI.Tests/TestDbContextFactory.cs` (if created)
   - Open Copilot Chat

2. **Request Test Creation**
   
   **Prompt**:
   ```
   Let's create unit tests for #file:HabitService.cs. We can use the 
   #file:TestDbContextFactory.cs (If created) to mock default test data. Please create 
   comprehensive tests covering all public methods including success and 
   failure scenarios.
   ```

3. **Review Generated Tests**
   
   Copilot should create tests covering:
   - ✅ `GetHabitsAsync()` - returns all habits
   - ✅ `GetHabitByIdAsync()` - returns specific habit
   - ✅ `GetHabitByIdAsync()` - returns null for invalid ID
   - ✅ `CreateHabitAsync()` - creates new habit successfully
   - ✅ `UpdateHabitAsync()` - updates existing habit
   - ✅ `DeleteHabitAsync()` - removes habit
   - ✅ Edge cases and validation scenarios

4. **Run the Tests**
   ```bash
   dotnet test
   ```
   
   **✅ Verify**: All tests pass

5. **Request Additional Test Coverage**
   
   If you want more specific tests:
   
   **Prompt**:
   ```
   Add tests for the GetActiveHabitsAsync method covering:
   - Habits with IsActive = true are returned
   - Habits with IsActive = false are excluded
   - Empty result when no active habits exist
   ```

### 🎓 Learning Points

- Use `#file:` references to give Copilot precise context
- AI can generate both happy path and edge case tests
- Review generated tests for completeness and correctness
- Tests serve as living documentation of expected behavior

---

## ✨ Exercise 5: Adding a New Feature - Random Quotes

### Objective
Implement a complete feature from scratch: replacing the static quote on the dashboard with dynamic random motivational quotes.

### The Feature

Replace the hardcoded Aristotle quote with a random motivational quote system that includes:
- 10 different motivational quotes
- 1-2 quotes from Star Wars
- API endpoint for retrieving random quotes
- Frontend integration with fallback handling

### Steps

1. **Identify the Code to Change**
   
   - Open `HabitWeb/Components/Pages/Home.razor`
   - Locate the quote section (lines 183-195):
   
   ```razor
   <div class="card mt-3">
       <div class="card-body text-center">
           <i class="fas fa-quote-left text-muted mb-2"></i>
           <blockquote class="blockquote-footer mb-0">
               <small class="text-muted">
                   "We are what we repeatedly do. Excellence, then, is not an act, but a habit."
                   <br><strong>— Aristotle</strong>
               </small>
           </blockquote>
       </div>
   </div>
   ```

2. **Describe the Feature to Copilot**
   
   **Select the quote section** in `Home.razor` and prompt:
   
   **Prompt**:
   ```
   This needs to be a random motivational quote feature. Requirements:
   - 10 random quotes (include 1-2 from Star Wars)
   - Create a Quote.cs model in the HabitModels project
   - Create a controller that gets a random quote from the database
   - Integrate it in #file:Home.razor.cs
   - Add proper error handling with fallback to Aristotle quote
   ```

3. **Review Copilot's Implementation Plan**
   
   Copilot should propose:
   
   **Step 1**: Create `Quote.cs` model in `HabitModels/Models/`
   ```csharp
   public class Quote
   {
       public int Id { get; set; }
       public string Text { get; set; }
       public string Author { get; set; }
       public DateTime CreatedAt { get; set; }
   }
   ```
   
   **Step 2**: Add Quote DbSet to `HabitDbContext.cs`
   ```csharp
   public DbSet<Quote> Quotes { get; set; }
   ```
   
   **Step 3**: Seed 10 quotes including Star Wars quotes
   
   **Step 4**: Create `QuotesController.cs` with `/api/quotes/random` endpoint
   
   **Step 5**: Create `QuoteApiService.cs` in HabitWeb
   
   **Step 6**: Update `Home.razor.cs` to load random quote
   
   **Step 7**: Update `Home.razor` UI to display dynamic quote

4. **Let Copilot Implement**
   
   Confirm each step as Copilot implements it:
   - Accept file creations
   - Review code changes
   - Verify build succeeds after each step

5. **Test the Feature**
   
   - Restart both applications
   - Navigate to the dashboard
   - **✅ Verify**: A random quote appears (may include Yoda or Obi-Wan!)
   - Refresh the page multiple times
   - **✅ Verify**: Different quotes appear

6. **Test Error Handling**
   
   - Stop the HabitAPI
   - Refresh the dashboard
   - **✅ Verify**: Aristotle quote appears as fallback

### 🎓 Learning Points

- Copilot can implement end-to-end features across multiple projects
- Breaking features into steps helps maintain code quality
- AI-generated code includes error handling and best practices
- Always test both happy path and failure scenarios

---

## 📝 Exercise 6: Generating Documentation

### Objective
Use Copilot to add comprehensive XML documentation to existing code following the project's documentation standards.

### Steps

1. **Review Documentation Requirements**
   
   Check `AGENTS.md` or `.github/copilot-instructions.md` for documentation standards:
   - All public methods need XML documentation
   - Include `<summary>`, `<param>`, `<returns>`, and `<remarks>` sections
   - Add `<example>` blocks for complex methods
   - Document exceptions with `<exception>` tags

2. **Document Controllers**
   
   **Prompt**:
   ```
   Let's add comprehensive XML documentation to the controllers in HabitAPI. 
   Focus on class-level summaries and all public methods. Follow the project's 
   documentation standards in AGENTS.md. Include examples for key endpoints.
   ```

3. **Review Generated Documentation**
   
   Example for `HabitsController`:
   
   ```csharp
   /// <summary>
   /// API controller for managing habit-related operations.
   /// Provides RESTful endpoints for habit CRUD operations with proper 
   /// HTTP status codes and error handling.
   /// </summary>
   /// <remarks>
   /// This controller handles:
   /// - GET /api/habits - Retrieve all habits
   /// - GET /api/habits/{id} - Retrieve specific habit
   /// - POST /api/habits - Create new habit
   /// - PUT /api/habits/{id} - Update existing habit
   /// - DELETE /api/habits/{id} - Delete habit
   /// </remarks>
   [ApiController]
   [Route("api/[controller]")]
   public class HabitsController : ControllerBase
   {
       /// <summary>
       /// Retrieves all habits for the current user.
       /// </summary>
       /// <returns>A list of habit DTOs</returns>
       /// <response code="200">Returns the list of habits</response>
       /// <response code="500">Internal server error occurred</response>
       [HttpGet]
       [ProducesResponseType(typeof(List<HabitDto>), StatusCodes.Status200OK)]
       public async Task<ActionResult<List<HabitDto>>> GetHabits()
       {
           // Implementation
       }
   }
   ```

4. **Document Services**
   
   **Prompt**:
   ```
   Now add documentation to the service classes in HabitAPI/Services/. 
   Include detailed explanations of business logic and any side effects.
   ```

5. **Verify Documentation Quality**
   
   - Check that all public members are documented
   - Verify documentation is clear and helpful
   - Ensure examples compile and make sense
   - Confirm consistency with project standards

6. **Generate Documentation Site** (Optional)
   
   ```bash
   # Install DocFX (if not already installed)
   dotnet tool install -g docfx
   
   # Generate documentation
   docfx init
   docfx build
   docfx serve
   ```

### 🎓 Learning Points

- AI can maintain consistent documentation standards across a codebase
- Well-documented code is easier to maintain and onboard new developers
- XML documentation integrates with IntelliSense and documentation generators
- Copilot learns from existing documentation patterns in your project

---

## 🎯 Bonus Exercises

### Exercise 7: Refactoring with Copilot

**Prompt**: `The MarkComplete method in Home.razor.cs has duplicate error handling. Can you refactor it to use a shared error handling helper?`

### Exercise 8: Adding Validation

**Prompt**: `Add input validation to CreateHabitDto ensuring name is required, between 1-100 characters, and targetCount is positive. Include both data annotations and unit tests.`

### Exercise 9: Performance Optimization

**Prompt**: `The GetHabitsAsync method loads all habit data. Can you add pagination support with page size limits and update the API controller to support ?page=1&pageSize=20 query parameters?`

### Exercise 10: Accessibility Improvements

**Prompt**: `Review Home.razor for accessibility issues and add appropriate ARIA labels, keyboard navigation support, and screen reader compatibility.`

---

## 🏆 Best Practices for Working with Copilot

### ✅ Do's

1. **Be Specific and Contextual**
   - ✅ "Add validation to CreateHabitDto ensuring names are 1-100 characters"
   - ❌ "Add validation"

2. **Use File References**
   - ✅ `#file:HabitService.cs` - Gives precise context
   - ❌ Relying on Copilot to guess which file

3. **Break Down Complex Tasks**
   - ✅ Multi-step prompts for large features
   - ❌ "Build the entire feature at once"

4. **Review AI-Generated Code**
   - ✅ Always verify logic, security, and performance
   - ❌ Blindly accepting all suggestions

5. **Iterate and Refine**
   - ✅ "Can you improve the error handling in this method?"
   - ❌ Accepting the first suggestion without refinement

### ❌ Don'ts

1. **Don't Skip Testing**
   - AI-generated code needs testing like any other code

2. **Don't Ignore Security**
   - Always review authentication, authorization, and input validation

3. **Don't Over-Rely on AI**
   - Use Copilot as a pair programmer, not a replacement for understanding

4. **Don't Forget Documentation**
   - Document why decisions were made, not just what the code does

5. **Don't Ignore Code Review**
   - AI suggestions should go through the same review process as human code

---

## 📚 Additional Resources

### GitHub Copilot Documentation
- [Getting Started with Copilot](https://docs.github.com/en/copilot/getting-started-with-github-copilot)
- [Using Copilot Chat](https://docs.github.com/en/copilot/using-github-copilot/using-github-copilot-chat)
- [Copilot Best Practices](https://github.blog/2023-06-20-how-to-write-better-prompts-for-github-copilot/)

### Project-Specific Resources
- [Project Architecture](README.md#architecture-overview)
- [Copilot Instructions](.github/copilot-instructions.md)
- [Agent Configuration](AGENTS.md)

### .NET Resources
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

---

## 💬 Feedback and Contributions

Found an issue or have suggestions for improving this tutorial?

- 🐛 [Report a bug](https://github.com/DeveloperTales/blazor-api-copilot-example/issues/new?labels=bug)
- 💡 [Suggest an improvement](https://github.com/DeveloperTales/blazor-api-copilot-example/issues/new?labels=enhancement)
- 📖 [Contribute to documentation](https://github.com/DeveloperTales/blazor-api-copilot-example/pulls)

---

## ✨ Next Steps

After completing this tutorial:

1. **Explore the Codebase**: Review the implementation details of features you built
2. **Customize the Application**: Add your own features using Copilot
3. **Share Your Experience**: Write a blog post or create a video about your learning
4. **Contribute Back**: Submit improvements to this tutorial or the project

**Happy Coding with GitHub Copilot! 🚀**

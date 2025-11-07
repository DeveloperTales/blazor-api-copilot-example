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

## 🚀 Enhanced Tutorial Features

You're using the enhanced version of this tutorial with advanced GitHub Copilot capabilities! This branch includes specialized AI tools and comprehensive documentation standards to give you a professional development experience.

**What this enhanced branch provides:**

### 📋 **Enhanced GitHub Copilot Instructions** (`.github/copilot-instructions.md`)
- **What it does**: Provides comprehensive project-specific guidelines for GitHub Copilot
- **Benefits**: 
  - Copilot understands your project's architecture and coding standards
  - Generates code that follows your team's conventions automatically
  - Includes specific patterns for Blazor components, API controllers, and DTOs
  - Enforces security best practices and performance optimizations

### 🎯 **Habit Tracker Chat Mode** (`.github/chatmodes/Habit.chatmode.md`)
- **What it does**: Custom AI agent specialized in the Habit Tracker project
- **Benefits**:
  - Acts as a Senior Software Engineer with deep project knowledge
  - **Enforces comprehensive XML documentation** for all public methods and classes
  - Provides architecture-specific guidance and code reviews
  - Includes mandatory documentation standards and code quality checklists

### 📚 **AI Agent Instructions** (`AGENTS.md`)
- **What it does**: Detailed instructions for AI assistants working on this project
- **Benefits**:
  - Ensures consistent coding patterns across all AI-generated code
  - Includes templates for controllers, services, and components
  - Provides troubleshooting guides and common pitfall avoidance
  - Maintains clean architecture principles

### 🧹 **Additional Benefits**
- **Clean Codebase**: No experimental changes or modifications
- **Optimized for Learning**: Code structured specifically for tutorial exercises
- **Better AI Context**: All AI tools have comprehensive project understanding

**What makes this tutorial enhanced:**
- ✅ **Professional-grade AI assistance** with specialized project knowledge
- ✅ **Mandatory documentation standards** enforced by custom AI agents
- ✅ **Clean, well-structured codebase** optimized for learning
- ✅ **Enterprise-level coding patterns** and best practices
- ✅ **Comprehensive error handling** and validation examples

**Perfect for:**
- Developers wanting to learn professional AI-assisted development workflows
- Teams looking to establish consistent coding standards with AI
- Anyone interested in advanced GitHub Copilot usage patterns

## 🤖 Using the Habit Tracker Chat Mode

The `added-instructions` branch includes a specialized **Habit Tracker Chat Mode** that acts as a Senior Software Engineer with deep knowledge of this project. This custom AI agent enforces professional coding standards and comprehensive documentation.

### 🔧 **How to Activate the Habit Chat Mode**

1. **Ensure you're on the `added-instructions` branch**:
   ```bash
   git branch  # Should show * added-instructions
   ```

2. **Open GitHub Copilot Chat**:
   - Press `Ctrl+Alt+I` (Windows) or `Cmd+Shift+I` (Mac)
   - Or use the Copilot Chat icon in VS Code

3. **Select the Habit Tracker Agent**:
   - Look for the agent/mode selector in the chat interface
   - Choose **"Habit Tracker Project - Senior Developer"** from the dropdown
   - You should see the agent activate with project-specific context

### 🎯 **What the Habit Chat Mode Provides**

- **🔍 Mandatory Documentation**: Automatically generates comprehensive XML documentation for all code
- **🏗️ Architecture Guidance**: Ensures code follows the project's clean architecture principles
- **📋 Code Quality Checks**: Provides quality checklists and best practice enforcement
- **🎨 Consistent Patterns**: Maintains consistent coding patterns across all generated code
- **🔒 Security Best Practices**: Includes security considerations in all recommendations

### 💬 **Example Interaction**

**You**: "I need to create a new API controller for managing user preferences"

**Habit Chat Mode Response**:
```
I'll help you create a preferences controller following our project standards.

This will include:
✅ Complete XML documentation with <summary>, <param>, <returns>
✅ Proper error handling with logging
✅ RESTful endpoint design
✅ Input validation with data annotations
✅ Repository pattern integration
✅ Swagger documentation attributes

Let me generate the controller with comprehensive documentation...
```

### 🚀 **When to Use the Habit Chat Mode**

- **✅ Creating new features**: Get architecture-compliant code from the start
- **✅ Code reviews**: Ask for quality assessments and improvements
- **✅ Documentation**: Generate or improve XML documentation
- **✅ Troubleshooting**: Get project-specific debugging help
- **✅ Best practices**: Ensure your code follows team standards

**Try it now**: Activate the Habit Chat Mode and ask: *"What are the coding standards for this project?"*

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

## ✨ Exercise 5: Adding a New Feature - Random Quotes (Enhanced with Documentation)

### Objective
Implement a complete feature from scratch using the **Habit Chat Mode** agent: replacing the static quote on the dashboard with dynamic random motivational quotes, ensuring comprehensive documentation is generated from the start.

> 💡 **Important**: This exercise requires the `added-instructions` branch for the Habit Chat Mode agent and enhanced documentation standards.

### Prerequisites
1. Switch to the `added-instructions` branch:
   ```bash
   git checkout added-instructions
   ```

2. **Activate the Habit Chat Mode**:
   - Open GitHub Copilot Chat
   - Click on the agent/mode selector
   - Choose **"Habit Tracker Project - Senior Developer"** chat mode
   - This activates the specialized agent with mandatory documentation requirements

### The Feature

Replace the hardcoded Aristotle quote with a random motivational quote system that includes:
- 10 different motivational quotes
- 1-2 quotes from Star Wars
- API endpoint for retrieving random quotes
- Frontend integration with fallback handling
- **🔥 NEW**: Complete XML documentation for all generated code

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

2. **Engage the Habit Chat Mode Agent**
   
   **Prompt the specialized agent**:
   ```
   I need to implement a random motivational quote feature to replace the static 
   Aristotle quote on the dashboard. Requirements:
   
   - 10 random quotes (include 1-2 from Star Wars)
   - Create a Quote.cs model in the HabitModels project
   - Create a controller that gets a random quote from the database
   - Integrate it in #file:Home.razor.cs
   - Add proper error handling with fallback to Aristotle quote
   - CRITICAL: Generate comprehensive XML documentation for all new code
   
   Please provide a step-by-step implementation plan with documentation requirements.
   ```

3. **Review the Enhanced Implementation Plan**
   
   The Habit Chat Mode agent should propose a plan including:
   
   **Step 1**: Create `Quote.cs` model with full documentation
   ```csharp
   /// <summary>
   /// Represents a motivational quote in the Habit Tracker application.
   /// Used for displaying random inspirational messages to users on the dashboard.
   /// </summary>
   /// <remarks>
   /// This entity is stored in the database and includes metadata for tracking
   /// and auditing purposes. Quotes can be motivational sayings, wisdom quotes,
   /// or popular culture references that inspire habit formation.
   /// </remarks>
   public class Quote
   {
       /// <summary>
       /// Gets or sets the unique identifier for the quote.
       /// </summary>
       public int Id { get; set; }
       
       /// <summary>
       /// Gets or sets the text content of the motivational quote.
       /// Should be concise and inspirational, typically 1-3 sentences.
       /// </summary>
       /// <example>"Do or do not, there is no try."</example>
       public string Text { get; set; } = string.Empty;
       
       /// <summary>
       /// Gets or sets the author or source of the quote.
       /// Can be a person's name, character name, or source material.
       /// </summary>
       /// <example>Yoda</example>
       public string Author { get; set; } = string.Empty;
   }
   ```

4. **Implement with Documentation Verification**
   
   For each step, verify the agent generates:
   
   **✅ Controller Documentation**:
   ```csharp
   /// <summary>
   /// API controller for managing quote-related operations in the Habit Tracker application.
   /// Provides endpoints for retrieving random motivational quotes for dashboard display.
   /// </summary>
   /// <remarks>
   /// This controller handles:
   /// - GET /api/quotes/random - Retrieve a random motivational quote
   /// 
   /// All endpoints include proper error handling and fallback mechanisms.
   /// </remarks>
   [ApiController]
   [Route("api/[controller]")]
   public class QuotesController : ControllerBase
   {
       /// <summary>
       /// Retrieves a random motivational quote from the database.
       /// </summary>
       /// <returns>
       /// A random quote DTO containing the quote text and author information.
       /// Returns a default Aristotle quote if no quotes are available in the database.
       /// </returns>
       /// <response code="200">Returns a random motivational quote</response>
       /// <response code="500">Internal server error occurred</response>
       [HttpGet("random")]
       public async Task<ActionResult<QuoteDto>> GetRandomQuote()
   ```

   **✅ Service Documentation**:
   ```csharp
   /// <summary>
   /// Service for managing quote operations and business logic.
   /// Handles quote retrieval, randomization, and fallback scenarios.
   /// </summary>
   public interface IQuoteService
   {
       /// <summary>
       /// Asynchronously retrieves a random quote from the available collection.
       /// </summary>
       /// <returns>
       /// A task that represents the asynchronous operation.
       /// The task result contains a QuoteDto with the random quote data,
       /// or a default quote if no quotes are available.
       /// </returns>
       Task<QuoteDto> GetRandomQuoteAsync();
   }
   ```

   **✅ Component Documentation**:
   ```csharp
   /// <summary>
   /// Loads and displays a random motivational quote on the dashboard.
   /// Implements error handling with fallback to a default Aristotle quote.
   /// </summary>
   /// <returns>A task representing the asynchronous quote loading operation</returns>
   /// <exception cref="HttpRequestException">
   /// Thrown when the quote service is unavailable
   /// </exception>
   private async Task LoadRandomQuoteAsync()
   ```

5. **Documentation Quality Verification**
   
   After each file is generated, verify it includes:
   
   - [ ] **Class-level documentation** with `<summary>` and `<remarks>`
   - [ ] **Method documentation** with `<summary>`, `<param>`, `<returns>`
   - [ ] **Exception documentation** with `<exception>` tags
   - [ ] **Example usage** with `<example>` blocks where appropriate
   - [ ] **Business context** explaining why the code exists
   - [ ] **Performance considerations** for database operations

6. **Test the Feature with Documentation**
   
   - **Build verification**: `dotnet build` (should show no documentation warnings)
   - Navigate to the dashboard
   - **✅ Verify**: A random quote appears with proper error handling
   - **✅ Verify**: IntelliSense shows rich documentation tooltips
   - **✅ Verify**: All public methods have comprehensive documentation

7. **Documentation Standards Compliance Check**
   
   Use the Habit Chat Mode agent to review your implementation:
   
   **Prompt**:
   ```
   Please review the generated quote feature code and verify it meets the 
   documentation standards defined in the project. Check for:
   - Complete XML documentation on all public members
   - Proper exception documentation
   - Business context explanations
   - Code examples where helpful
   
   Provide a compliance report with any missing documentation.
   ```

### 🎓 Enhanced Learning Points

- **Habit Chat Mode** enforces documentation standards automatically
- Comprehensive documentation improves code maintainability and team collaboration
- AI agents can be specialized for specific project requirements
- Documentation should explain **why** and **business context**, not just **what**
- Well-documented code provides better IntelliSense and developer experience
- Documentation standards become part of the development workflow, not an afterthought

### 🔍 **Bonus Challenge**: Documentation Generation Report

After completing the exercise, generate a documentation coverage report:

**Prompt to Habit Chat Mode**:
```
Generate a documentation coverage report for the quote feature implementation. 
Include:
- Count of documented vs undocumented public members
- Quality assessment of existing documentation
- Recommendations for improvement
- Examples of best-practice documentation from the implementation
```

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

**Happy Coding with GitHub Copilot! 🚀**

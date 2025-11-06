# Habit Tracker - GitHub Copilot Demo Project

A project for demonstrating **GitHub Copilot as a pair programmer** for building modern .NET applications. This project is used to teach how to leverage AI-assisted development to create a full-stack Habit Tracker application with clean architecture and best practices.

## 🎯 Project Purpose

This repository demonstrates:
- **GitHub Copilot integration** in real-world development scenarios
- **AI-assisted pair programming** techniques and workflows
- **Clean architecture patterns** in .NET applications
- **Modern Blazor development** with code-behind separation
- **RESTful API design** with comprehensive documentation
- **Best practices** for maintainable and scalable applications

## 🏗️ Architecture Overview

```
HabitProject.sln
├── 🌐 HabitAPI/          # ASP.NET Core Web API (Port 5134)
│   ├── Controllers/      # RESTful API endpoints
│   ├── Services/         # Business logic layer
│   └── Data/            # Entity Framework DbContext
├── 🖥️ HabitWeb/          # Blazor Server Application (Port 5000)
│   ├── Components/      # Blazor components with code-behind
│   ├── Services/        # API client services
│   └── wwwroot/         # Static assets (CSS, JS, images)
└── 📦 HabitModels/       # Shared class library
    ├── Models/          # Domain entities
    └── DTOs/            # Data Transfer Objects
        ├── Habits/
        └── HabitEntries/
```

## ✨ Features

### 🎯 Habit Management
- ✅ Create, read, update, and delete habits
- ✅ Customizable habit frequencies (Daily, Weekly, Monthly)
- ✅ Target counts and units for measurable goals
- ✅ Color-coded habit categories
- ✅ Real-time completion tracking

### 📊 Dashboard & Analytics
- ✅ Comprehensive dashboard with key metrics
- ✅ Streak tracking and completion rates
- ✅ Weekly progress visualization
- ✅ Quick action buttons for common tasks

### 🎨 Modern UI/UX
- ✅ Responsive Bootstrap 5 design
- ✅ Font Awesome icons for visual consistency
- ✅ Interactive modals and forms
- ✅ Loading states and error handling
- ✅ Clean, intuitive user interface

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [GitHub Copilot extension](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/DeveloperTales/blazor-api-copilot-example.git
   cd blazor-api-copilot-example
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

## 🏃‍♂️ Running the Applications

### Option 1: Run Both Applications Simultaneously

```bash
# Start the API (Terminal 1)
cd HabitAPI
dotnet run

# Start the Web App (Terminal 2) 
cd HabitWeb
dotnet run --urls="https://localhost:5001;http://localhost:5000"
```

### Option 2: Using Visual Studio

1. **Set Multiple Startup Projects**:
   - Right-click the solution in Solution Explorer
   - Select "Set Startup Projects..."
   - Choose "Multiple startup projects"
   - Set both `HabitAPI` and `HabitWeb` to "Start"

2. **Press F5** to run both projects simultaneously

### Option 3: Using VS Code

```bash
# Use the provided tasks.json configuration
Ctrl+Shift+P → "Tasks: Run Task" → "Start All Projects"
```

## 🌐 Application URLs

Once running, access the applications at:

- **🖥️ Blazor Web App**: [http://localhost:5000](http://localhost:5000) or [https://localhost:5001](https://localhost:5001)
- **🌐 API Documentation**: [http://localhost:5133/swagger](http://localhost:5133/swagger) or [https://localhost:5134/swagger](https://localhost:5134/swagger)

## 🤖 GitHub Copilot Integration

This project includes comprehensive AI assistance configurations:

### 📋 Copilot Instructions
- **`.github/copilot-instructions.md`**: General project guidelines for Copilot
- **`.github/chatmodes/Habit.chatmode.md`**: Specialized chatmode for this project
- **`AGENTS.md`**: Universal agent instructions for consistent AI assistance

### 🎯 Copilot Usage Examples

1. **Code Generation**: 
   ```csharp
   // Type a comment and let Copilot generate the implementation
   // Create a method to calculate habit completion percentage
   ```

2. **Test Creation**:
   ```csharp
   // Generate unit tests for the HabitService class
   ```

3. **Documentation**:
   ```csharp
   /// <summary>
   /// [Let Copilot complete the XML documentation]
   ```

## 🧪 Testing

### Run Unit Tests
```bash
dotnet test
```

### API Testing
Use the built-in Swagger UI at `/swagger` or tools like:
- **HTTP files**: Use the `.http` files in the API project
- **curl**: Command-line testing examples in `/docs/api-examples.md`

## 📚 Project Structure Deep Dive

### 🎨 Blazor Components (Code-Behind Pattern)
```
HabitWeb/Components/Pages/
├── Home.razor          # Dashboard markup
├── Home.razor.cs       # Dashboard logic
├── Habits.razor        # Habit management markup
└── Habits.razor.cs     # Habit management logic
```

### 🌐 API Controllers
```
HabitAPI/Controllers/
├── HabitsController.cs       # Habit CRUD operations
└── HabitEntriesController.cs # Habit entry tracking
```

### 📦 Shared Models
```
HabitModels/
├── Models/
│   ├── Habit.cs           # Domain entity
│   ├── HabitEntry.cs      # Completion tracking
│   └── User.cs            # User management
└── DTOs/
    ├── Habits/            # Habit-related DTOs
    └── HabitEntries/      # Entry-related DTOs
```

## 🛠️ Development Workflow

### 1. AI-Assisted Feature Development
```bash
# 1. Describe the feature to Copilot
# 2. Let Copilot suggest implementation approach
# 3. Iteratively refine with AI assistance
# 4. Use Copilot for documentation and tests
```

### 2. Code Quality Assurance
- **XML Documentation**: Required for all public methods
- **Error Handling**: Comprehensive try-catch patterns
- **Validation**: Client and server-side validation
- **Testing**: Unit tests for business logic

### 3. Architecture Patterns
- **Repository Pattern**: Data access abstraction
- **Service Layer**: Business logic separation
- **DTO Pattern**: Data contract definition
- **Dependency Injection**: Loose coupling

## 🔧 Configuration

### Database
- **Development**: Entity Framework In-Memory database
- **Seed Data**: Automatic test data generation
- **Connection**: No configuration required for development

## 📖 Learning Resources

### GitHub Copilot Best Practices
- [GitHub Copilot Documentation](https://docs.github.com/copilot)
- [Copilot in VS Code](https://code.visualstudio.com/docs/editor/github-copilot)

### .NET & Blazor Resources
- [Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor)
- [ASP.NET Core Web API](https://docs.microsoft.com/aspnet/core/web-api)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)

## 🏆 Acknowledgments

- **GitHub Copilot** for AI-powered development assistance
- **Microsoft** for .NET 8 and Blazor framework
- **Bootstrap** for responsive UI components
- **Font Awesome** for beautiful icons

---

## 💡 Pro Tips for GitHub Copilot Users

### 🎯 Effective Prompting
```csharp
// ✅ Good: Specific and contextual
// Create a method to validate habit input and return validation errors

// ❌ Poor: Vague and generic  
// Make a validation method
```

### 🔄 Iterative Development
1. Start with a clear comment describing the intent
2. Let Copilot suggest the implementation
3. Refine and adjust with follow-up prompts
4. Use Copilot for documentation and tests

### 📚 Context Awareness
- Keep related files open for better suggestions
- Use meaningful variable and method names
- Maintain consistent coding patterns
- Leverage the project's existing architecture

**Happy coding with your AI pair programmer! 🚀🤖**
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

Once running, access the applications at:

- **🖥️ Blazor Web App**: [http://localhost:5000](http://localhost:5000) or [https://localhost:5001](https://localhost:5001)
- **🌐 API Documentation**: [http://localhost:5133/swagger](http://localhost:5133/swagger) or [https://localhost:5134/swagger](https://localhost:5134/swagger)

## 🤖 GitHub Copilot Integration

This project includes comprehensive AI assistance configurations:

### 📋 Copilot Instructions
- **`.github/copilot-instructions.md`**: General project guidelines for Copilot
- **`.github/chatmodes/Habit.chatmode.md`**: Specialized chatmode for this project
- **`AGENTS.md`**: Universal agent instructions for consistent AI assistance

### 🎓 Interactive Learning

Want to learn how to use GitHub Copilot effectively? This project includes a comprehensive hands-on tutorial!

**👉 [Start the Tutorial](TUTORIAL.md)**

The tutorial covers:
- 🐛 **Debugging with Copilot Agent** - Fix real bugs with AI assistance
- 🧪 **Creating Unit Tests** - Set up test projects and write comprehensive tests
- ✨ **Adding New Features** - Implement complete features from scratch
- 📝 **Generating Documentation** - Create professional XML documentation
- 🎯 **Best Practices** - Learn effective prompting techniques

**Estimated Time**: 60-90 minutes | **Skill Level**: Beginner to Intermediate

## 📚 Project Structure

### HabitAPI (Backend)
```
HabitAPI/
├── Controllers/          # API endpoints
│   ├── HabitsController.cs
│   ├── HabitEntriesController.cs
│   └── QuotesController.cs
├── Services/            # Business logic
│   ├── HabitService.cs
│   └── HabitEntryService.cs
├── Data/               # Database context
│   └── HabitDbContext.cs
└── Program.cs          # Application startup
```

### HabitWeb (Frontend)
```
HabitWeb/
├── Components/
│   ├── Pages/          # Routable pages
│   │   ├── Home.razor
│   │   ├── Habits.razor
│   │   ├── Analytics.razor
│   │   └── Calendar.razor
│   └── Layout/         # Shared layouts
├── Services/           # API client services
│   ├── HabitApiService.cs
│   └── QuoteApiService.cs
└── wwwroot/           # Static files
```

### HabitModels (Shared)
```
HabitModels/
├── Models/            # Domain entities
│   ├── Habit.cs
│   ├── HabitEntry.cs
│   ├── User.cs
│   └── Quote.cs
└── DTOs/             # Data Transfer Objects
    ├── Habits/
    └── HabitEntries/
```

## 🛠️ Technologies Used

- **Framework**: .NET 8.0
- **Frontend**: Blazor Server with InteractiveServer render mode
- **Backend**: ASP.NET Core Web API
- **Database**: Entity Framework Core (In-Memory for development)
- **UI**: Bootstrap 5, Font Awesome icons
- **API Documentation**: Swagger/OpenAPI
- **AI Assistant**: GitHub Copilot

## 🧪 Testing

The project includes a comprehensive test suite (if you completed the tutorial):

```bash
# Run all tests
dotnet test

# Run tests with code coverage
dotnet test /p:CollectCoverage=true
```

## 📖 API Documentation

Once the API is running, access the interactive Swagger documentation at:
- [https://localhost:5134/swagger](https://localhost:5134/swagger)

Key endpoints:
- `GET /api/habits` - Retrieve all habits
- `POST /api/habits` - Create a new habit
- `PUT /api/habits/{id}` - Update an existing habit
- `DELETE /api/habits/{id}` - Delete a habit
- `GET /api/quotes/random` - Get a random motivational quote

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 💬 Support

- 🐛 [Report a Bug](https://github.com/DeveloperTales/blazor-api-copilot-example/issues/new?labels=bug)
- 💡 [Request a Feature](https://github.com/DeveloperTales/blazor-api-copilot-example/issues/new?labels=enhancement)
- 📖 [View Documentation](TUTORIAL.md)

## 🌟 Acknowledgments

- Built with ❤️ to demonstrate GitHub Copilot's capabilities
- Inspired by real-world development workflows
- Designed for educational purposes

## 📚 Additional Resources

- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)

---

**Happy Coding with GitHub Copilot! 🚀**

Made with 🤖 by [DeveloperTales](https://github.com/DeveloperTales)

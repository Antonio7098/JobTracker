# JobTracker

A .NET 8 REST API for tracking job applications, including employers and job vacancies, with comprehensive unit testing.

## 📁 Solution Structure

This repository contains a multi-project .NET solution:

```
JobTracker/
├── JobTracker.Api/          # Main REST API project
├── JobTracker.Api.Tests/    # xUnit test project
├── docs/                    # Learning sprints, architecture docs, and guides
└── JobTracker.Api.sln       # Solution file
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MySQL Server (for database persistence)

### Setup

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd JobTracker
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure the database:**
   - Update the connection string in `JobTracker.Api/appsettings.Development.json`
   - Run migrations:
     ```bash
     cd JobTracker.Api
     dotnet ef database update
     ```

4. **Run the API:**
   ```bash
   cd JobTracker.Api
   dotnet run
   ```

## 🧪 Running Tests
 
 ### Unit & Integration Tests
 To run the full suite of unit and integration tests, execute the following command from the root directory of the solution:
 
 ```bash
 dotnet test
 ```

 ### BDD Acceptance Tests
 To run the SpecFlow acceptance tests:
 ```bash
 dotnet test JobTracker.Api.AcceptanceTests/
 ```

## 📚 Documentation

- **[API Documentation](JobTracker.Api/README.md)** - Detailed information about the API project
- **[Test Project Documentation](JobTracker.Api.Tests/README.md)** - Information on the testing project and strategies
- **[Architecture Overview](docs/ARCHITECTURE.md)** - System design and architecture decisions
- **[Contributing Guide](docs/CONTRIBUTING.md)** - Commit conventions and workflow
- **[Learning Sprints](docs/sprints/)** - Sprint-by-sprint learning documentation

## 🎯 Project Goals

This project is a learning-focused implementation covering:
- REST API design with ASP.NET Core Minimal APIs
- Repository pattern and service layer architecture
- Entity Framework Core with MySQL
- Request validation with FluentValidation
- Unit and integration testing with **xUnit**
- Mocking dependencies with **Moq**

## 📄 License

This is a learning project.
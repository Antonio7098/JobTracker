# JobTracker.Api.Tests

Unit test suite for the JobTracker API using xUnit and Moq.

## 🧪 Testing Framework

- **xUnit** - Primary testing framework
- **Moq** - Mocking library for isolating dependencies
- **FluentAssertions** - (Optional) For more readable assertions

## 📁 Test Structure

Tests are organized to mirror the main project structure:

```
JobTracker.Api.Tests/
├── Services/              # Tests for repository and service classes
├── Endpoints/             # Tests for API endpoints (integration tests)
├── Validators/            # Tests for FluentValidation validators
└── GlobalUsings.cs        # Common using statements
```

## 🚀 Running Tests

### Run all tests:
```bash
dotnet test
```

### Run tests with detailed output:
```bash
dotnet test --verbosity normal
```

### Run tests with coverage (requires additional tools):
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## ✅ Test Conventions

- **Naming:** `MethodName_Scenario_ExpectedBehavior`
  - Example: `GetByIdAsync_ExistingId_ReturnsEmployer`
- **Structure:** Arrange-Act-Assert (AAA) pattern
- **Isolation:** Each test should be independent and not rely on other tests

## 🎯 Coverage Goals

- **Repository Layer:** High coverage (aim for 80%+)
- **Validators:** Comprehensive coverage of all validation rules
- **Endpoints:** Integration tests for happy paths and error cases

## 📚 Learning Resources

See [Sprint 07: Unit Testing](../docs/sprints/Sprint-07-Unit-Testing.md) for detailed learning objectives and implementation notes.


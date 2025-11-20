# JobTracker.Api.AcceptanceTests

This project contains **Behavior-Driven Development (BDD) acceptance tests** for the JobTracker API using **SpecFlow** and **xUnit**.

## What are Acceptance Tests?

Acceptance tests verify that the system behaves correctly from an end-user perspective. Unlike unit tests that test individual components in isolation, acceptance tests:
- Test the **entire system** end-to-end
- Make **real HTTP requests** to an in-memory instance of the API
- Verify **business requirements** are met
- Serve as **living documentation** of system behavior

## What is BDD?

Behavior-Driven Development (BDD) uses natural language (Gherkin) to describe system behavior in a way that's readable by both technical and non-technical stakeholders.

### Example Scenario:
```gherkin
Scenario: A new employer is created successfully
    Given I have the details of a valid employer
    When I send a request to create that employer
    Then the response status should be 201 created
    And the employer should be retrievable from the api
```

## Running the Tests

Run all acceptance tests:
```bash
dotnet test JobTracker.Api.AcceptanceTests/
```

Run a specific test:
```bash
dotnet test JobTracker.Api.AcceptanceTests/ --filter "DisplayName~employer lifecycle"
```

## Project Structure

```
JobTracker.Api.AcceptanceTests/
├── Features/                    # Gherkin feature files
│   └── EmployerManagement.feature
├── StepDefinitions/             # C# step implementations
│   └── EmployerManagementSteps.cs
├── Hooks/                       # Test setup/teardown
│   └── TestHooks.cs
└── TestWebApplicationFactory.cs # In-memory API configuration
```

## Test Infrastructure

- **TestWebApplicationFactory**: Spins up the API in-memory with a fake database
- **TestHooks**: Sets up and tears down test resources for each scenario
- **ScenarioContext**: Shares data between steps within a scenario
- **In-Memory Database**: Uses EF Core's in-memory provider for test isolation

## Writing New Tests

1. **Add a scenario** to a `.feature` file in `Features/`
2. **Run the tests** - SpecFlow will suggest step definitions for missing steps
3. **Implement step definitions** in `StepDefinitions/`
4. **Run tests again** to verify

## Test Coverage

Current scenarios cover:
- ✅ Creating employers (happy path)
- ✅ Retrieving employers
- ✅ Updating employers
- ✅ Deleting employers
- ✅ Validation errors (422)
- ✅ Not found errors (404)
- ✅ RFC 7807 Problem Details verification
- ✅ Complete CRUD lifecycle workflow

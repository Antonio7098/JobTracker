# Project Architecture

This document outlines the key architectural patterns and decisions made in the Job Tracker API project. The goal is to maintain a clean, scalable, and maintainable codebase, even while using the lightweight Minimal API framework.

## 1. Minimal APIs with Deliberate Organization

Instead of traditional MVC controllers, this project uses ASP.NET Core's **Minimal APIs**. This approach reduces boilerplate and keeps the focus on simple HTTP endpoint handlers.

To prevent `Program.cs` from becoming cluttered, we employ two key organizational patterns:

- **Route Groups:** All endpoints related to a specific feature (e.g., "employers") are grouped together using `app.MapGroup("/employers")`. This allows for common configuration and keeps related routes visually co-located.
- **Extension Methods:** The endpoint mapping logic for each feature is moved into its own dedicated file (e.g., `Endpoints/EmployerEndpoints.cs`). An extension method (`MapEmployersEndpoints`) is used to attach these routes to the main `WebApplication` instance in `Program.cs`.

## 2. Repository Pattern for Data Access

The application's business logic (the API endpoints) is decoupled from the data access logic using the **Repository Pattern**.

- **The Interface (`IEmployersRepository`):** This interface, located in `/Services`, defines a contract for data operations (e.g., `GetAllEmployers`, `CreateEmployer`). The API endpoints depend only on this interface, not on any specific implementation.
- **The Implementation (`MySqlEmployersRepository`):** This class provides the concrete implementation of the interface, containing the actual EF Core code to query the MySQL database.
- **Dependency Injection (DI):** In `Program.cs`, we register the concrete implementation against the interface (`builder.Services.AddScoped<IEmployersRepository, MySqlEmployersRepository>()`). The ASP.NET Core DI container is then responsible for providing an instance of `MySqlEmployersRepository` wherever an `IEmployersRepository` is requested. This makes it trivial to swap out the data access layer without changing the API endpoints (as was done when moving from the `InMemoryEmployersRepository`).

## 3. Data Transfer Objects (DTOs) for API Contracts

The project strictly avoids exposing its internal domain models (`Employer`, `JobVacancy`) directly to the client. Instead, it uses **Data Transfer Objects (DTOs)** for all public-facing contracts.

The primary reasons for this are:

1.  **To Create a Stable Public Contract:** The internal domain models may change frequently during development. DTOs provide a stable "view" of the data that can be maintained without breaking client applications.
2.  **To Prevent Over-Exposing Data:** DTOs allow us to shape the data specifically for the client, exposing only the properties that are necessary and hiding any internal or sensitive information.

Mapping between domain models and DTOs is handled by simple extension methods in the `/Maps` directory.

## 4. Folder Structure

The project follows a feature-oriented folder structure:

- **/Data:** Contains the EF Core `DbContext` and related configuration.
- **/DTOs:** Contains all Data Transfer Objects.
- **/Endpoints:** Contains the Minimal API endpoint definitions, organized by feature.
- **/Maps:** Contains extension methods for mapping between domain models and DTOs.
- **/Migrations:** Contains EF Core database migration files.
- **/Models:** Contains the core C# domain model classes.
- **/Services:** Contains business logic and data access interfaces/implementations (Repositories).

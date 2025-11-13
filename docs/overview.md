# 🚀 Project Overview: Job Tracker API (Learning Project)

## 🎯 Primary Objective

To build a complete, production-ready backend **REST API** using **ASP.NET Core 8** and **C#**. This project will serve as a practical demonstration of enterprise-style practices, including decoupled architecture, comprehensive testing, and automated CI/CD workflows.

---

## 🏗️ Core Architecture & Domain

| Resource | Key Properties | Relationship |
| :--- | :--- | :--- |
| **Employer** | `Id` (GUID), `Name`, `CompanyDescription` | One Employer can have **Many** Job Vacancies. |
| **JobVacancy** | `Id` (GUID), `Title`, `Description`, `SalaryRange`, `EmployerId` | **Many** Vacancies belong to **One** Employer. |

---

## ✅ Key Concepts & Skills Checklist

This checklist tracks our progress sprint-by-sprint.

| Status | Sprint | Concept/Feature | Purpose & Rationale (The "Why") |
| :--- | :--- | :--- | :--- |
| [x] | **Sprint 1** | **Foundation:** C# & OOP | Define classes, properties, and the one-to-many relationship. The bedrock of the domain model. |
| [x] | **Sprint 2** | **API Layer:** Minimal APIs | Implement basic endpoints (GET, POST) for the API. |
| [x] | **Sprint 2** | **Architecture:** Repository & DI | Decouple business logic from data access using Dependency Injection. |
| [x] | **Sprint 3** | **Contracts:** DTOs | Define a stable public contract for the API, protecting internal domain models. |
| [x] | **Sprint 3** | **Structure:** Route Groups | Logically organize endpoints for clean, readable code. |
| [x] | **Sprint 4** | **Persistence:** EF Core & MySQL | Introduce a real database using an ORM and manage schema with migrations. |
| [x] | **Sprint 4** | **Configuration:** Connection Strings | Externalize database connection strings from source code into `appsettings.json`. |
| [x] | **Sprint 4** | **Performance:** Async/Await | Implement `async`/`await` for all I/O-bound database operations to ensure a scalable API. |
| [x] | **Sprint 5** | **Documentation:** OpenAPI & Markdown | Generate live API documentation (Swagger) and create high-level project guides. |
| [ ] | **Sprint 6** | **Robustness:** Validation & Error Handling | Handle invalid inputs gracefully and implement a consistent strategy for error responses. |
| [ ] | **Sprint 7** | **Quality:** Unit Testing | Write unit tests to verify individual components (like services) in isolation. |
| [ ] | **Sprint 8** | **Quality:** BDD/Acceptance Testing | Write acceptance tests that verify the API's behavior against user requirements. |
| [ ] | **Sprint 9** | **Automation:** CI/CD with GitHub Actions | Create a pipeline that automatically builds the project and runs all tests on every commit. |

---

## 🛠️ Required Tools

*   .NET 8 SDK
*   Visual Studio Code (VS Code)
*   VS Code Extensions: C# Dev Kit, REST Client
*   MySQL Database (e.g., via Docker or a local installation)
*   Git & a GitHub account
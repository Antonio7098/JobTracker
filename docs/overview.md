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

This checklist is our roadmap. Each item directly corresponds to a skill listed on your CV.

| Stage | Concept/Feature | Purpose & Rationale (The "Why") |
| :--- | :--- | :--- |
| **I. Foundation** | **C# & OOP Fundamentals** | Define classes, properties, and the one-to-many relationship. The bedrock of the domain model. |
| **II. API Layer** | **Minimal APIs** | Implement basic endpoints (GET, POST, PUT, DELETE) for both resources. |
| **III. Architecture** | **Repository Pattern & DI** | Decouple business logic from data access. Use Dependency Injection to manage services like repositories. |
| **IV. Contracts** | **Data Transfer Objects (DTOs)** | Define a stable public contract for the API, protecting internal domain models from breaking changes. |
| **V. Structure** | **Route Groups & Extension Methods** | Logically organize endpoints and apply common middleware/settings efficiently for clean, readable code. |
| **VI. Persistence** | **Entity Framework Core & MySQL** | Introduce a real database (**MySQL**) using an ORM. Manage schema changes with **Code-First Migrations**. |
| **VII Documentation (Sprint 5)** | **Detailed, Professional Documentation** | Establish high-quality docs: architecture overview, setup guides, API reference (OpenAPI/Swagger), contribution guide, changelog, and diagrams. Optionally publish via DocFX/Docusaurus for a browsable site. |
| **VIII. Configuration** | **Externalised Configuration** | Store secrets and connection strings in `appsettings.json` and user secrets, not hardcoded in the source. |
| **IX. Performance** | **Asynchronous Programming** | Implement `async`/`await` for all I/O-bound database operations to ensure the API is scalable and efficient. |
| **X. Robustness** | **Validation & Structured Error Handling** | Handle invalid inputs gracefully and implement a consistent strategy for error responses. |
| **XI. Quality** | **BDD: SpecFlow/Gherkin** | Write acceptance tests that verify the API's behavior against user requirements from the outside-in. |
| **XII. Quality** | **Unit Testing** | Write unit tests to verify individual components (like services or logic classes) in isolation. |
| **XIII. Automation** | **CI/CD with GitHub Actions** | Create a Continuous Integration pipeline that automatically builds the project and runs all tests on every commit. |

---

## 🛠️ Required Tools

*   .NET 8 SDK
*   Visual Studio Code (VS Code)
*   VS Code Extensions: C# Dev Kit, REST Client
*   MySQL Database (e.g., via Docker or a local installation)
*   Git & a GitHub account
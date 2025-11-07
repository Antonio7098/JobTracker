# 🚀 Project Overview: Job Vacancy API (Learning Project)

## 🎯 Primary Objective

To build a complete, production-ready backend **REST API** using **ASP.NET Core 8** and **C#**, fulfilling all standard **CRUD** operations for `JobVacancy` and `Employer` resources, while intentionally applying every concept covered in the beginner course guide.

---

## 🏗️ Core Resources (Domain Models)

| Resource | Key Properties to Manage | Relationship |
| :--- | :--- | :--- |
| **Employer** | ID (GUID), Name, Company Description | One Employer can have Many Job Vacancies |
| **JobVacancy** | ID (GUID), Title, Description, Salary Range, **Employer ID** | Many Vacancies belong to One Employer |

---

## ✅ Key Concepts to Implement (The Checklist)

| Stage | Concept/Feature | Purpose & Rationale |
| :--- | :--- | :--- |
| **I. Foundation** | **C# & OOP Fundamentals** | Define classes, properties, and the one-to-many relationship. |
| **II. API Design** | **Minimal APIs** | Implement basic endpoints (GET, POST, PUT, DELETE) for both resources. |
| **III. Architecture** | **Data Transfer Objects (DTOs)** | Define the contract for inbound and outbound data, protecting the internal domain models. |
| **IV. Structure** | **Extension Methods & Route Groups** | Organize endpoints logically and apply common middleware/settings efficiently. |
| **V. Data Access** | **Entity Framework Core (EF Core)** | Introduce a real database (SQLite for local dev) using an **ORM**. |
| **VI. Configuration** | **Configuration System** | Store the database connection string in `appsettings.json` (avoid hardcoding). |
| **VII. Decoupling** | **Dependency Injection (DI)** | Use DI for services (e.g., database context, repositories) and understand service lifetimes. |
| **VIII. Optimization** | **Asynchronous Programming** | Implement `async`/`await` for all I/O bound database operations for efficiency. |
| **IX. Quality** | **Validation** | Ensure proper handling of invalid inputs (e.g., missing titles, bad IDs). |
| **X. Testing** | **SpecFlow/Gherkin** | Implement acceptance tests using Behavior-Driven Development (BDD) to verify API functionality. |

---

## 🛠️ Required Tools

* **.NET 8 SDK**
* **Visual Studio Code (VS Code)**
* **VS Code Extensions:** C# Dev Kit, Rest Client, SQLite
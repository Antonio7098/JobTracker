# 🎯 Learning Sprint Template: Sprint 4 - Persistence with EF Core & MySQL

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** **Entity Framework Core**, **MySQL**, **DbContext**, **Code-First Migrations**, **Connection Strings**, **Async/Await**.
* **Primary Goal (Must-Have):** By the end of this sprint, you must be able to **replace the `InMemoryEmployersRepository` with a new `MySqlEmployersRepository` that uses EF Core to persist data to a real MySQL database, and explain how EF Core's `DbContext` and migrations work.**
* **Secondary Goals:**
    * Install and configure the necessary EF Core and MySQL NuGet packages.
    * Define a `DbContext` with `DbSet<>` properties for the domain models.
    * Use the EF Core CLI to generate and apply database migrations to manage schema changes.
    * Implement the `IEmployersRepository` interface using EF Core's asynchronous methods (`.ToListAsync()`, `.FirstOrDefaultAsync()`, etc.).

---


## ✅ Task List

- [X] **Task 1: Project Setup & Configuration**
    > *Description: Add the EF Core packages and configure the database connection string, separating it from source code.*
    - [X] Sub-task 1.1: Use the NuGet package manager to find and install `Pomelo.EntityFrameworkCore.MySql`. This is the database provider that allows EF Core to communicate with MySQL.
    - [X] Sub-task 1.2: Install the `Microsoft.EntityFrameworkCore.Design` package. This package provides the tools necessary for EF Core migrations.
    - [X] Sub-task 1.3: In your `appsettings.json` file, add a new `ConnectionStrings` section and a connection string named "DefaultConnection" for a local MySQL database. (e.g., `Server=localhost;Database=JobTrackerDb;User=user;Password=password;`).

- [ ] **Task 2: Create the `DbContext`**
    > *Description: Create the EF Core DbContext, which represents a session with the database and allows you to query and save instances of your entities.*
    - [X] Sub-task 2.1: Create a new top-level folder named `Data`.
    - [X] Sub-task 2.2: Inside `Data`, create a new class `JobTrackerDbContext` that inherits from `Microsoft.EntityFrameworkCore.DbContext`.S
    - [X] Sub-task 2.3: Add `DbSet<Employer>` and `DbSet<JobVacancy>` properties to your context. These properties represent the tables in your database.
    - [ ] Sub-task 2.4: In `Program.cs`, register your `DbContext` with the dependency injection container using `builder.Services.AddDbContext`. You will need to read the connection string from your configuration file here.

- [ ] **Task 3: Create and Apply the Initial Database Migration**
    > *Description: Use EF Core's "Code-First" migration tools to automatically generate the database schema based on your C# models.*
    - [ ] Sub-task 3.1: If you haven't already, install the EF Core command-line interface (CLI) tools by running `dotnet tool install --global dotnet-ef`.
    - [ ] Sub-task 3.2: From your terminal in the project root, run the command `dotnet ef migrations add InitialCreate`. This will generate your first migration file.
    - [ ] Sub-task 3.3: Inspect the generated migration file. Identify the `Up()` and `Down()` methods and understand what each one is responsible for.
    - [ ] Sub-task 3.4: Run the command `dotnet ef database update` to apply the migration and create the database and tables.

- [ ] **Task 4: Implement the EF Core Repository**
    > *Description: Create a new repository implementation that uses EF Core to perform CRUD operations against the MySQL database.*
    - [ ] Sub-task 4.1: In the `Services` folder, create a new class `MySqlEmployersRepository` that implements the `IEmployersRepository` interface.
    - [ ] Sub-task 4.2: Implement the `GetAllEmployers` and `GetEmployerById` methods using `async` EF Core methods like `.ToListAsync()` and `.FirstOrDefaultAsync()`.
    - [ ] Sub-task 4.3: Implement the `CreateEmployer` method using `.AddAsync()` and `.SaveChangesAsync()`.
    - [ ] Sub-task 4.4: Implement the `UpdateEmployer` and `DeleteEmployer` methods. Remember that `Update` and `Remove` are not async methods, but you will still call `.SaveChangesAsync()` to commit the changes.

- [ ] **Task 5: Swap the Implementation and Test**
    > *Description: Change the dependency injection registration to use the new repository and verify that the application now uses the database.*
    - [ ] Sub-task 5.1: In `Program.cs`, find the line where `IEmployersRepository` is registered and change the implementation from `InMemoryEmployersRepository` to `MySqlEmployersRepository`.
    - [ ] Sub-task 5.2: Run the application. Use the `employers.http` file to create a new employer.
    - [ ] Sub-task 5.3: Stop and restart the application. Send the `GET` request for the employer you just created. If it's still there, your data is successfully being persisted in the MySQL database.

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** `[AI will provide a brief, high-level summary of the work.]`

**Final Status:** `[Pass / Rework Needed / Incomplete]`

---

### Strengths

*`[AI will list specific things that were done correctly and well.]`
* ...

### Areas for Improvement & Corrections

*`[AI will list specific, actionable feedback on what was done incorrectly or could be improved. This is where errors will be flagged.]`
[ ] **Correction 1:** ...
[ ] **Correction 2:** ...

### Conceptual Gaps

*`[AI will identify any underlying conceptual misunderstandings based on the submitted work.]`
* ...

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the purpose of the `DbContext` class in EF Core?
    **Answer:** 
    > **AI Feedback:** 

2.  What is the difference between a `DbSet<T>` and a `List<T>`?
    **Answer:** 
    > **AI Feedback:** 

3.  In your own words, what problem do "migrations" solve? What would you have to do if you didn't use them?
    **Answer:** 
    > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** Why use an ORM like EF Core instead of writing raw SQL with a library like ADO.NET or Dapper? What are the primary trade-offs (e.g., performance vs. productivity)?
    **Answer:** 
    > **AI Feedback:** 

2.  **Connecting Concepts:** When you call `_context.SaveChangesAsync()`, how does the `DbContext`'s Change Tracker know whether to generate an `INSERT`, `UPDATE`, or `DELETE` statement for a particular C# object?
    **Answer:** 
    > **AI Feedback:** 

3.  **Critical Thinking:** What do you think would happen if you changed your `Employer` C# model (e.g., added a new `string Location` property) but forgot to create and apply a new migration before running the app? What kind of error might you see?
    **Answer:** 
    > **AI Feedback:** 

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
**AI FInal Feedback:**
* **Next Action:** [e.g., Start Sprint Template for next concept, Deep dive into Stretch Question 3].

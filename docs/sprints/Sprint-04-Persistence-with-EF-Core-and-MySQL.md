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

- [X] **Task 2: Create the `DbContext`**
    > *Description: Create the EF Core DbContext, which represents a session with the database and allows you to query and save instances of your entities.*
    - [X] Sub-task 2.1: Create a new top-level folder named `Data`.
    - [X] Sub-task 2.2: Inside `Data`, create a new class `JobTrackerDbContext` that inherits from `Microsoft.EntityFrameworkCore.DbContext`.S
    - [X] Sub-task 2.3: Add `DbSet<Employer>` and `DbSet<JobVacancy>` properties to your context. These properties represent the tables in your database.
    - [X]Sub-task 2.4: In `Program.cs`, register your `DbContext` with the dependency injection container using `builder.Services.AddDbContext`. You will need to read the connection string from your configuration file here.

- [ ] **Task 3: Set Up MySQL and Create/Apply the Initial Database Migration**
    > *Description: Use EF Core's "Code-First" migration tools to automatically generate the database schema based on your C# models.*
    - [X] Sub-task 3.0: Set up MySQL using Docker.
        - Create a bash script `mysql-docker.sh` in your project root to manage MySQL
        - The script should handle: `start`, `stop`, `status` commands at minimum
        - **Understanding the Docker command you'll use in the script:**
          - `docker run -d` = Create and run container in background
          - `--name jobtracker-mysql` = Give the container a name
          - `-e MYSQL_DATABASE=JobTrackerDb` = Create your database automatically
          - `-e MYSQL_USER=user` and `-e MYSQL_PASSWORD=password` = Match your connection string
          - `-p 3306:3306` = Port mapping (host:container) - exposes MySQL on localhost:3306
          - `mysql:8.0` = The Docker image
        - **Test your script:** Run `./mysql-docker.sh start` and verify with `docker ps`
    - [X] Sub-task 3.1: If you haven't already, install the EF Core command-line interface (CLI) tools by running `dotnet tool install --global dotnet-ef`.
    - [X] Sub-task 3.2: From your terminal in the project root, run the command `dotnet ef migrations add InitialCreate`. This will generate your first migration file.
    - [X] Sub-task 3.3: Inspect the generated migration file. Identify the `Up()` and `Down()` methods and understand what each one is responsible for.r
    - [X] Sub-task 3.4: Run the command `dotnet ef database update` to apply the migration and create the database and tables. You should see output confirming the migration was applied.

- [X] **Task 4: Implement the EF Core Repository**
    > *Description: Create a new repository implementation that uses EF Core to perform CRUD operations against the MySQL database.*
    - [X] Sub-task 4.1: In the `Services` folder, create a new class `MySqlEmployersRepository` that implements the `IEmployersRepository` interface.
    - [X] Sub-task 4.2: Implement the `GetAllEmployers` and `GetEmployerById` methods using `async` EF Core methods like `.ToListAsync()` and `.FirstOrDefaultAsync()`.
    - [X] Sub-task 4.3: Implement the `CreateEmployer` method using `.AddAsync()` and `.SaveChangesAsync()`.
    - [X] Sub-task 4.4: Implement the `UpdateEmployer` and `DeleteEmployer` methods. Remember that `Update` and `Remove` are not async methods, but you will still call `.SaveChangesAsync()` to commit the changes.

- [X] **Task 5: Swap the Implementation and Test**
    > *Description: Change the dependency injection registration to use the new repository and verify that the application now uses the database.*
    - [X] Sub-task 5.1: In `Program.cs`, find the line where `IEmployersRepository` is registered and change the implementation from `InMemoryEmployersRepository` to `MySqlEmployersRepository`.
    - [X] Sub-task 5.2: Run the application. Use the `employers.http` file to create a new employer.
    - [X] Sub-task 5.3: Stop and restart the application. Send the `GET` request for the employer you just created. If it's still there, your data is successfully being persisted in the MySQL database.

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** `You successfully replaced the in-memory repository with a fully persistent EF Core and MySQL implementation, demonstrating a strong command of the sprint's concepts.`

**Final Status:** `Pass`

---

### Strengths

*`Excellent end-to-end implementation, from installing packages and managing configuration to correctly using the DbContext and migrations.`
* `Proactively diagnosed and fixed a common but non-trivial dependency injection lifetime error, showing a great ability to debug.`
* `The code is clean, well-structured, and correctly uses async/await for database operations.`

### Areas for Improvement & Corrections

*`The code submission for this sprint is excellent. There are no corrections needed.`

### Conceptual Gaps

*`No conceptual gaps are apparent from the submitted code. You've shown a solid understanding of EF Core, repositories, and dependency injection.`

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the purpose of the `DbContext` class in EF Core?
    **Answer:**
    ```
    Allows you to connect and query to the database
    ```
    > **AI Feedback:** Correct! To expand on that, the `DbContext` represents a "sessifon" with the database. It not only allows you to query data but also tracks any changes you make to the C# objects you've queried, so it can save those changes back to the database.

2.  What is the difference between a `DbSet<T>` and a `List<T>`?
    **Answer:**
    ```
    DbSet is a collection of database entries that can be queried, list is a list of thigs.
    ```
    > **AI Feedback:** That's the right idea. The key difference is *where* the data lives. A `List<T>` is an in-memory collection in your application. A `DbSet<T>` represents a table in the database. When you write a LINQ query against a `DbSet`, EF Core translates it into SQL and runs it on the database server, only bringing the results into memory.

3.  In your own words, what problem do "migrations" solve? What would you have to do if you didn't use them?
    **Answer:**
    ```
    Migrations allow you to update the database through ef, without having to write sql.
    ```
    > **AI Feedback:** Exactly. Migrations keep your database schema synchronized with your C# models over time. Without them, you would have to manually write SQL scripts (`ALTER TABLE`, `CREATE TABLE`, etc.) every time you changed your models, which is slow, error-prone, and hard to manage in a team.

4.  What is a Docker container, and why might you use Docker for running MySQL instead of installing it directly on your system?
    **Answer:**
    ```
    Allows you to containerise things, so the database for this project runs in its own little container without affecting system wide MySql, or vice versa.
    ```
    > **AI Feedback:** Perfect answer. This captures the core benefit of isolation beautifully. It prevents conflicts, ensures every developer has the exact same setup, and makes it trivial to create and destroy the database environment without impacting your local machine.

5.  When you run `docker run -d --name jobtracker-mysql -p 3306:3306 mysql:8.0`, what does the `-p 3306:3306` flag do? Why is this necessary for your .NET application to connect?
    **Answer:**
    ```
    not sured
    ```
    > **AI Feedback:** The `-p` flag is for "port mapping". It connects a port on your local machine (the host) to a port inside the container. ` -p 3306:3306` maps port 3306 on your `localhost` to port 3306 inside the container, where MySQL is listening for connections. It's necessary because your .NET app runs on the host, outside the container's isolated network, so this mapping exposes the database to your app.

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** Why use an ORM like EF Core instead of writing raw SQL with a library like ADO.NET or Dapper? What are the primary trade-offs (e.g., performance vs. productivity)?
    **Answer:**
    ```
    It is easier. You can do things like dependency injection. You get less performance but more productivity.
    ```
    > **AI Feedback:** You've hit the main trade-off perfectly: productivity vs. performance. While you do use dependency injection *with* an ORM, the core benefits are the productivity boost from working with C# objects (no manual SQL), compile-time type checking for your queries, and abstracting away database-specific syntax.

2.  **Connecting Concepts:** When you call `_context.SaveChangesAsync()`, how does the `DbContext`'s Change Tracker know whether to generate an `INSERT`, `UPDATE`, or `DELETE` statement for a particular C# object?
    **Answer:**
    ```
    Because it keeps track of the changes made to anything queried entity from the database.
    ```
    > **AI Feedback:** Correct! The Change Tracker automatically assigns a state (`Added`, `Modified`, `Deleted`, `Unchanged`) to every entity it's aware of. When you call `SaveChangesAsync()`, it checks this state for each entity and generates the corresponding SQL command.

3.  **Critical Thinking:** What do you think would happen if you changed your `Employer` C# model (e.g., added a new `string Location` property) but forgot to create and apply a new migration before running the app? What kind of error might you see?
    **Answer:**
    ```
    You would see invalid field.
    ```
    > **AI Feedback:** You're right on track. You'd get an error indicating a mismatch between your application's model and the database's schema. Often, this is an `InvalidOperationException` from EF Core stating "The model backing the 'JobTrackerDbContext' context has changed since the database was created."

4.  **Docker & Environment Isolation:** If you run `docker stop jobtracker-mysql`, does your MySQL data disappear? What about if you run `docker rm jobtracker-mysql`? Explain the difference and what Docker concept is involved (hint: think about persistence).
    **Answer:**
    ```
    Docker stop means you will not be able to connect. If you delete it, you will have to re build it and will loose all of your data.
    ```
    > **AI Feedback:** Exactly right. `docker stop` just pauses the container; its filesystem and data are preserved. `docker rm` permanently removes the container and its filesystem. To make data persist even after a `rm`, you would use a Docker Volume, which stores the data on the host machine, separate from the container's lifecycle.

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
**AI FInal Feedback:**
* **Next Action:** [e.g., Start Sprint Template for next concept, Deep dive into Stretch Question 3].

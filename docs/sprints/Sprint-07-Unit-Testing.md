# 🎯 Learning Sprint 07: Unit Testing

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** **Unit Testing, xUnit, Moq, Test-Driven Development (TDD), Arrange-Act-Assert (AAA) Pattern**
* **Primary Goal (Must-Have):** By the end, I must be able to **write comprehensive unit tests for the repository layer using xUnit and Moq, structure tests using the AAA pattern, and verbally explain the purpose of test isolation, mocking, and the differences between unit, integration, and acceptance tests**.
* **Secondary Goals:**
    * Set up a separate test project following .NET conventions
    * Use Moq to create test doubles for dependencies (e.g., `DbContext`)
    * Achieve high code coverage for the `MySqlEmployersRepository` class
    * Understand when to mock and when not to mock
    * Write clear, maintainable test code with descriptive naming conventions

---

## ✅ Task List

- [X] **Task 1: Research & Plan the Testing Strategy**
    > *Description: Before writing tests, you need to understand the testing landscape in .NET and decide what to test.*
    - [X] **Sub-task 1.1:** Research the three main testing frameworks for .NET (xUnit, NUnit, MSTest). Which one is most commonly used in modern .NET projects? Why?
    ```
    XUnit is most commonly used. Why?
        - Mictosoft themselves have adopted it, setting a strong precedent.
        - It is built for parallel test execution by default, making it faster and more efficient in CI?CD pipelines.
        - It promotes better architecture with its opinionated design.

    NUnit has a rich ecosystem and is used in legacy frameworks.
    ```
    - [X] **Sub-task 1.2:** Read about the **Test Pyramid** (Unit → Integration → E2E). Where do unit tests fit, and what do they aim to verify?
    ```
    Unit tests are at the base of the pyramid, and aim to verify the individual components (functions, methods, etc...). They are fast, with no external dependencies, and should be the most numerous tests.
    ```
    - [X] **Sub-task 1.3:** Identify which components in your project should be unit tested. Should you test DTOs? Endpoints? The repository? Why or why not?
    ```
    We should test the repository methods, as these are individual components. Endpoints would be integration tests, and DTOs have no methods to test.
    ```

- [X] **Task 2: Create a Test Project**
    > *Description: Set up a separate project for your tests following .NET conventions.*
    - [X] **Sub-task 2.1:** Use the `dotnet` CLI to create a new xUnit test project named `JobTracker.Api.Tests` in your solution directory.
    - [X] **Sub-task 2.2:** Add a project reference from the test project to the main `JobTracker.Api` project.
    - [X] **Sub-task 2.3:** Install the necessary NuGet packages:
        - `xunit` (should be included by default)
        - `xunit.runner.visualstudio` (for IDE test discovery)
        - `Moq` (for mocking dependencies)
        - `Microsoft.EntityFrameworkCore.InMemory` (for testing EF Core without a real database)
    - [X] **Sub-task 2.4:** Run `dotnet test` to verify the test project is set up correctly (it should run the default generated test).

- [X] **Task 3: Write Your First Unit Test**
    > *Description: Create a simple test to understand the basic structure and AAA pattern.*
    - [X] **Sub-task 3.1:** Create a new folder in the test project called `Repositories`.
    - [ X] **Sub-task 3.2:** Create a test class named `MySqlEmployersRepositoryTests.cs`.
    - [X] **Sub-task 3.3:** Write a test method called `GetAllEmployers_WhenDatabaseIsEmpty_ReturnsEmptyList()`. This test should:
        - **Arrange:** Set up a mock or in-memory `DbContext` with no employers
        - **Act:** Call `GetAllEmployers()`
        - **Assert:** Verify the result is an empty list
    - [X] **Sub-task 3.4:** Run the test using `dotnet test` and ensure it passes.

- [X] **Task 4: Test the "Happy Path" CRUD Operations**
    > *Description: Write tests for each repository method assuming everything works correctly.*
    - [X] **Sub-task 4.1:** Write a test: `CreateEmployer_WhenValidEmployer_AddsToDatabase()`
        - Arrange an in-memory `DbContext`
        - Act: call `CreateEmployer()`
        - Assert: verify the employer was added to the `DbContext`
    - [X] **Sub-task 4.2:** Write a test: `GetEmployerById_WhenEmployerExists_ReturnsEmployer()`
    - [X] **Sub-task 4.3:** Write a test: `UpdateEmployer_WhenEmployerExists_UpdatesEmployer()`
    - [X] **Sub-task 4.4:** Write a test: `DeleteEmployer_WhenEmployerExists_RemovesEmployer()`
    - [X] **Sub-task 4.5:** Run all tests and ensure they pass.

- [X] **Task 5: Test Edge Cases & Failure Scenarios**
    > *Description: Write tests for what happens when things go wrong.*
    - [X] **Sub-task 5.1:** Write a test: `GetEmployerById_WhenEmployerDoesNotExist_ReturnsNull()`
    - [X] **Sub-task 5.2:** Write a test: `UpdateEmployer_WhenEmployerDoesNotExist_ReturnsFalse()`
    - [X] **Sub-task 5.3:** Write a test: `DeleteEmployer_WhenEmployerDoesNotExist_ReturnsFalse()`
    - [X] **Sub-task 5.4:** Consider: Do you need to test validation? Why or why not? (Hint: Where is validation happening in your architecture?)
    **Answer:**
    ```
    Validationis the concern of higher levels of your application like the endpoints. By the time the data gets to the repository, it should be properly validated
    ```

- [X] **Task 6: Refactor for Testability (Optional Challenge)**
    > *Description: If you used `Microsoft.EntityFrameworkCore.InMemory`, you've technically written integration tests. True unit tests would mock the `DbContext`.*
    - [X] **Sub-task 6.1:** Research: What's the difference between using an in-memory database vs. mocking `DbContext` with Moq?
    ```
    Using an in memory database simulates the databae in memory, giving you access to ef's query pipeline, including LINQ to Entities translation and change tracking. Mocking involves faking Db set behaviours which emulates  basic data access but doesn't replicate EF’s actual behavior.
    Using an in memory database is considered best practice.
    ```
    > **AI Feedback:** In summary, your description of the difference is spot-on. But the "best practice" is more nuanced. For repository
  tests, SQLite is often preferred over the basic InMemory provider for its realism. And for higher-level service
  tests, mocking the repository interface is the way to go.
    
    - [X] **Sub-task 6.2:** (Optional) Try writing one test that mocks the `DbContext` and `DbSet<Employer>`. Is it easier or harder than using in-memory?
    - [X] **Sub-task 6.3:** Make a decision: Which approach will you use going forward? Document your reasoning.
    **Answer:**
    ```
    Both! They are techniqies for different layers. To test the repository, we use an in meory database. To test the services, we mock the repository interface.
    ```

- [X] **Task 7: Review Test Quality & Conventions**
    > *Description: Ensure your tests follow best practices.*
    - [X] **Sub-task 7.1:** Review your test naming. Do they clearly state: `MethodName_Scenario_ExpectedResult`?
    - [X] **Sub-task 7.2:** Ensure each test has a clear AAA structure with comments or whitespace separating sections.
    - [X] **Sub-task 7.3:** Run `dotnet test` with the `--logger "console;verbosity=detailed"` flag to see detailed output. Are all tests passing?
    - [X] **Sub-task 7.4:** Check code coverage (optional): Install `coverlet.collector` and run tests with coverage reporting. What % of `MySqlEmployersRepository` is covered?

- [X] **Task 8: Update Project Documentation & Version**
    > *Description: Reflect the new testing infrastructure in your project documentation.*
    - [X] **Sub-task 8.1:** Update `README.md`:
        - Add a "Running Tests" section explaining how to run `dotnet test`
        - Add the test project to the "Project Structure" section
        - Update "Technologies Used" to include xUnit and Moq
    - [X] **Sub-task 8.2:** Update `docs/ARCHITECTURE.md`:
        - Add a new section titled "Testing Strategy" explaining your approach (unit tests with in-memory DB or mocks)
        - Document what layers you're testing and why
    - [X] **Sub-task 8.3:** Increment the version in `JobTracker.Api.csproj` to `0.3.0` (MINOR version bump for new testing feature).
    - [ ] **Sub-task 8.4:** Stage and commit your changes with the message: `feat: Add unit tests for MySqlEmployersRepository`
    - [X] **Sub-task 8.5:** Create an annotated tag: `git tag -a v0.3.0 -m "Release 0.3.0: Unit testing infrastructure"`
    - [X] **Sub-task 8.6:** Push your commits and tags to the remote repository.

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** Fantastic work on this sprint. You successfully created a new test project, wrote a comprehensive suite of tests for the repository's CRUD operations, and correctly updated all relevant project documentation. The tests are clean, well-structured, and cover both happy paths and edge cases.

**Final Status:** Pass

---

### Strengths

*   **Excellent Test Structure:** Your tests perfectly follow the Arrange-Act-Assert (AAA) pattern, making them very easy to read and understand. Using a new `Guid` for each in-memory database is the correct approach and ensures perfect test isolation.
*   **Thorough Coverage:** You wrote tests for all the "happy path" scenarios and, just as importantly, for the failure and edge cases (e.g., `GetEmployerById_WhenEmployerDoesNotExist_ReturnsNull`). This is the hallmark of a robust test suite.
*   **Great Documentation:** You updated both the `README.md` and `ARCHITECTURE.md` files accurately. The new "Running Tests" and "Testing Strategy" sections are clear and provide valuable information for anyone working on the project.
*   **Correct Versioning:** You correctly incremented the project version to `0.3.0` to reflect the new feature (testing infrastructure), which is a great habit.

### Areas for Improvement & Corrections

*   **Correction 1: Minor Naming Inconsistency in a Test Method**
    - **Issue:** One of your test methods has a small naming inconsistency that could be slightly confusing.
    - **Location:** `MySqlEmployersRepositoryTests.cs`
    - **Incorrect Code:**
      ```csharp
      [Fact]
      public async Task UpdateEmployerById_WhenEmployerDoesNotExist_ReturnsFalse()
      // ...
      ```
    - **Correct Code:**
      ```csharp
      [Fact]
      public async Task UpdateEmployer_WhenEmployerDoesNotExist_ReturnsFalse()
      // ...
      ```
    - **Reasoning:** The method being tested is `UpdateEmployer`, which takes an `id` as its first parameter. The original name `UpdateEmployerById` might imply that there is a different method with that specific name. Aligning the test method name precisely with the name of the method under test (`UpdateEmployer`) makes the intent clearer. This is a very minor point but is good practice for maintainability.

### Conceptual Gaps

*   **None.** Your work demonstrates a solid and practical understanding of the concepts covered in this sprint. You've correctly applied the AAA pattern, understood test isolation, and made an informed decision to use the in-memory provider for repository tests.

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1. What is the purpose of **unit testing**, and how does it differ from **integration testing**?  
   **Answer:**
   ```
   Unit testing tests components of your application in isolation. It aims at verifying the business logic within each component, without worrying about how it integrated with other components/layers of your application.
   ```
   > **AI Feedback:** Correct. To add a bit more detail, the key difference is the **scope**. A unit test verifies a single "unit" (like a method or a class) in isolation, often using mocks to replace dependencies. An integration test verifies that multiple components work together correctly, such as your API endpoint, repository, and a test database all interacting as expected.

2. What does the **AAA pattern** (Arrange-Act-Assert) mean, and why is it useful?  
   **Answer:**
   ```
   **Arrange**: You set up the components needed for the test eg seeding data in the database.
   **Act:** You use the component that is being tested.
   **Assert:** You check if the component did what you expected.
   **Note:** I do not really get why this is such a big deal. I dont understand how you could posibly do a test WITHOUT using this pattern.
   ```
   > **AI Feedback:** Your definitions are perfect. Your note is insightful—you're right that almost every test naturally follows this logic. The reason it's formalized as a "pattern" is to enforce **clarity and consistency**. By explicitly structuring your tests this way (often with comments or blank lines separating the sections), you make the test's purpose immediately obvious to any reader. It answers three questions sequentially: What's the setup? What's being tested? And what's the expected outcome? It's less about enabling testing and more about making tests readable and maintainable.

3. In your own words, what is a **mock**, and why do we use mocking libraries like Moq in unit tests?  
   **Answer:**
   ```
   A mock pretends to be a service, behaving like it without actually implementing it. We use mocking to keep tests isolated, without depending on external services.
   ```
   > **AI Feedback:** Exactly right. A mock is a test double that we can program with specific expectations (e.g., "this method should be called once with these parameters and should return this value"). This isolates the unit under test from its dependencies, ensuring you're only testing one thing at a time.

4. What is the difference between `Microsoft.EntityFrameworkCore.InMemory` and mocking `DbContext` with Moq? Which did you use, and why?  
   **Answer:**
   ```
   InMEmory creates an actual database that can then be queried. Mocking with Moq only imitates the database without giving access to ... . We used INMEmory because setting up miocks is complex and time consuming.
   ```
   > **AI Feedback:** This is a good summary. The `InMemory` provider gives you a lightweight but real database that uses EF Core's actual query engine and change tracker. Mocking `DbContext` and `DbSet` is much more manual—you have to fake the behavior of `Add`, `Remove`, `FindAsync`, etc., and it doesn't test EF Core's query translation at all. Your reason for choosing `InMemory` is spot on: it provides a high degree of confidence for repository tests with much less complexity than full mocking.

5. According to the **Test Pyramid**, should you have more unit tests or more integration tests? Why?  
   **Answer:**
   ```
   You should make more unit tests, becausethese test the foundational services. There will be more services than there are ways for them to interact with each other, hence there will be more unit tests.
   ```
   > **AI Feedback:** Correct. You should have many more unit tests. The primary reasons are that they are **fast**, **stable** (no external dependencies like networks or databases), and **precise** (they pinpoint the exact location of a failure). Your reasoning is also valid; you will naturally have more individual components than combinations of them.

6. Describe the step-by-step process that the xUnit test runner follows when it executes a single test method in a class that uses a constructor for setup and implements `IDisposable` for cleanup. Why is this "new instance per test" model beneficial?  
   **Answer:**
   ```
   Every time it sees a [Fact], it spins up a new instance of the class for that test. This then calls the constructor, allowing us to create a new instance of the database for every test. Furthermore, when xUnit detects a IDisposable class, it knows to call the Dispose method after the test has finished, cleaning up the database we created. This is beneficial because it allows for clean and easy test isolation.
   ```
   > **AI Feedback:** Perfect answer. You've correctly described the entire lifecycle (new instance -> constructor -> test method -> dispose) and correctly identified **test isolation** as the key benefit. This prevents state from one test from leaking into another, which is a common source of flaky, unreliable tests in other frameworks.

7. In our test setup, we use `UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())`. Explain the role of `Guid.NewGuid().ToString()` here. What would be the negative consequence of using a fixed string like `"TestDatabase"` instead?  
   **Answer:**
   ```
   This ensures every database is unique and no tests share the same database name. This is important becaus xUnit runs tests in parallel, so if two tests shared the same database, there may be conflicts that impact the test, making it non determenistic.
   ```
   > **AI Feedback:** Exactly. Using a fixed name would cause the same in-memory database instance to be shared across all tests in the class. Since xUnit runs tests in parallel by default, one test could be adding data while another is trying to assert that the database is empty, leading to race conditions and unpredictable test failures. The `Guid` ensures every single test method gets a pristine, isolated database.

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1. **Test Coverage vs. Test Quality:** If you achieve 100% code coverage, does that mean your code has no bugs? Why or why not?  
   **Answer:**
   ```
   No. Just because a test covers a component of the application does not mean it is implemented perfectly. It could have mistakes in allowing for bugs to slip through,
   ```
   > **AI Feedback:** Correct. 100% coverage only means that every line of code was *executed* during a test. It doesn't mean the *assertions* were meaningful. You could have 100% coverage with zero `Assert` statements. High coverage is a good goal, but it's no guarantee of quality. A good test must make strong assertions about the behavior and outcomes.

2. **What NOT to Test:** Should you write unit tests for your DTOs (like `EmployerDto`)? What about your Minimal API endpoints? Explain your reasoning.  
   **Answer:**
   ```
   The DTOS are verified through the validators. There is no need to do unit tests on them. The endpoint tests would be integration tests, because ...
   ```
   > **AI Feedback:** You are absolutely right on both points.
   > - **DTOs:** DTOs are simple data carriers with no logic, so there's nothing to unit test. Their *validation* is tested via the validator classes, but the DTOs themselves don't need tests.
   > - **Endpoints:** Testing a Minimal API endpoint involves the HTTP pipeline, routing, model binding, dependency injection, and the repository. Since it touches multiple components, it is by definition an **integration test**, not a unit test.

3. **Test-Driven Development (TDD):** In TDD, you write tests *before* writing code. What are the benefits of this approach? What are the drawbacks?  
   **Answer:**
   ```
   You know exactly what requirements the code needs to implement, saving you from backtracking. You are writing the code specifically to pass the test, so it is more likely to be correct. The drawback is it takes longer in the short term to implement TDD.
   ```
   > **AI Feedback:** Excellent summary.
   > **Benefits:** The biggest benefit is that it forces you to think about the *design and interface* of your code from a consumer's perspective first. This often leads to cleaner, more decoupled designs. It also ensures you have 100% test coverage by default.
   > **Drawbacks:** The main drawback is the initial learning curve and the perception that it's slower, as you noted. It can also be difficult for exploratory programming where the final design isn't clear from the start.

4. **Testability & Design:** If a class is difficult to unit test (e.g., has many dependencies, or tightly coupled to external systems), what does that tell you about the class's design?  
   **Answer:**
   ```
   It is not designed very well. There is a principle that states each class should implmement exactly one ... , which makes the classes mor etestable and maintainable.
   ```
   > **AI Feedback:** Precisely. Difficulty in testing is a "code smell" that often indicates poor design. The principle you're thinking of is the **Single Responsibility Principle (SRP)**, the 'S' in SOLID. A class that is hard to test is often doing too much (violating SRP) or is too tightly coupled to its dependencies. Writing tests first (TDD) often naturally leads to better design because you're forced to confront these issues early.

5. **Mocking Strategy:** When writing tests for `MySqlEmployersRepository`, you need to decide whether to mock `DbContext` or use an in-memory database. What are the trade-offs of each approach in terms of test speed, complexity, and realism?  
   **Answer:**
   ```
   Mocknig DbContext provides more isolation but it a lot harder. On balance, it is better to use in memory.
   ```
   > **AI Feedback:** You've captured the essence of it. Here's a more explicit breakdown of the trade-offs:
   > - **In-Memory DB:**
   >   - **Speed:** Fast.
   >   - **Complexity:** Low. Easy to set up.
   >   - **Realism:** High. It uses the real EF Core query engine.
   > - **Mocking DbContext:**
   >   - **Speed:** Fastest. No database engine involved.
   >   - **Complexity:** Very High. You have to manually mock `DbSet`, `FindAsync`, `AddAsync`, etc. It's brittle and complex.
   >   - **Realism:** Low. It doesn't actually test EF Core's LINQ translation or change tracking.
   > Your conclusion is the standard industry practice: use an in-memory or SQLite provider for repository tests; it's the best balance.

6. When using EF Core, which method is more likely to fail in a real-world application: `AddAsync()` or `SaveChangesAsync()`? List three distinct reasons why the more fallible method might fail when connected to a real database like MySQL.  
   **Answer:**
   ```
   AddAsync() is not performing any database operations, simply updating the state of the repository.
   ```
   > **AI Feedback:** Correct. `AddAsync` simply starts tracking the entity in EF Core's memory. `SaveChangesAsync` is the one that actually communicates with the database and is far more likely to fail.
   > Here are three common reasons for failure:
   > 1.  **Connection Issues:** The database server is down, the network is unavailable, or credentials are wrong.
   > 2.  **Constraint Violations:** You try to insert data that violates a database rule (e.g., a duplicate unique key, a missing foreign key, a `null` value in a non-nullable column).
   > 3.  **Concurrency Conflicts:** Another user or process has modified or deleted the same data between the time you read it and the time you tried to save your changes (optimistic concurrency).

7. **Test Assertion Strategy:** In our `DeleteEmployer` test, we discussed two ways to assert that the employer was removed:
    1. Using `_context.FindAsync(id)` to check the database state directly.
    2. Using `_repository.GetEmployerById(id)` to check via another public method.
    Explain the pros and cons of each approach. Which philosophy ("Pure Method Isolation" vs. "Public Contract Testing") is generally preferred for classes like repositories, and why?  
   **Answer:**
   ```
   Doing it directly provides more isolation, but means the test depens on internal dependencies rather than the api. We decided that it was okay to interact directly with the database in the arrange stage, but not in the assert. This seemed contradictory to me, but I was assured it is best practice. To me, it seems like doing it directly is more reliable and means that each test depends on exactly one component.
   ```
   > **AI Feedback:** This is an excellent and subtle question. Your intuition is sharp.
   > - **Approach 1 (Direct Context):** Pro: Verifies the absolute state of the world. Con: Tightly couples the test to the implementation detail (`DbContext`). If you swapped `DbContext` for something else, the test would break even if the public method still worked.
   > - **Approach 2 (Public Method):** Pro: Tests the public contract of the class. The test verifies that `Delete` works as advertised from a consumer's point of view (`GetById` can no longer find it). Con: The test now technically relies on *two* methods (`DeleteEmployer` and `GetEmployerById`), not just one.
   >
   > **Preferred Philosophy:** For repository tests, **Public Contract Testing (Approach 2)** is generally preferred. The goal is to test the repository *as a black box*. We care that its public methods behave as expected. If `DeleteEmployer` successfully removes an item, then `GetEmployerById` *should* return null. Relying on this public behavior makes your tests more resilient to internal refactoring. Your feeling that it's more reliable to check directly is valid, but it makes for more brittle tests.

8. **Pragmatism vs. Purity in Testing:** We've noted an apparent contradiction in testing best practices:
    *   In the **Arrange** phase, we often use implementation details (e.g., `_context.AddAsync`) as a shortcut to set up state.
    *   In the **Assert** phase, we try to avoid implementation details and use public methods (e.g., `_repository.GetEmployerById`).
    Explain the reasoning behind this pragmatic approach. Why is it often beneficial to have different standards for the setup and verification phases of a test?
   **Answer:**
   ```
   Still not sure.
   ```
   > **AI Feedback:** This is the deepest question here, and it builds on the last one. The reasoning is purely **pragmatism and efficiency**.
   > - **In Arrange:** The goal is to get the system into a specific state as quickly and easily as possible. Using implementation details like `_context.AddAsync` is often the most direct shortcut. If we only used public methods for setup (e.g., calling `_repository.CreateEmployer`), our setup could become very complex and slow.
   > - **In Assert:** The goal is to verify the *outcome* of the action from a user's perspective. We want to confirm that the public contract of our class is fulfilled. Asserting against the public API (`_repository.GetEmployerById`) ensures our test is verifying the behavior, not the implementation.
   >
   > In short, we are "impure" in the Arrange phase for the sake of convenience, but we become "pure" in the Assert phase for the sake of correctness and resilience to refactoring. We accept the trade-off that our test setup might be brittle, but we insist that our verification is not.

---

## 📝 Sprint Review

* **Status:** Done
* **Most Difficult Insight:** Understandig the tradeoffs between checking the database directly and using public methods in tests (Arrange, Assert).
* **AI Final Feedback:** You did an outstanding job on this sprint. Your tests were well-written, your documentation updates were professional, and your answers to the questions showed a deep engagement with the material. Your 'Most Difficult Insight' is one of the most important takeaways from this entire topic; understanding that pragmatic trade-off between setup and assertion is what separates good test writers from great ones. You're more than ready for the next challenge. Well done!
* **Next Action:** Sprint 8


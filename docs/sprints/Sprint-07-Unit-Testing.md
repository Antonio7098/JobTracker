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

- [ ] **Task 2: Create a Test Project**
    > *Description: Set up a separate project for your tests following .NET conventions.*
    - [X] **Sub-task 2.1:** Use the `dotnet` CLI to create a new xUnit test project named `JobTracker.Api.Tests` in your solution directory.
    - [X] **Sub-task 2.2:** Add a project reference from the test project to the main `JobTracker.Api` project.
    - [ ] **Sub-task 2.3:** Install the necessary NuGet packages:
        - `xunit` (should be included by default)
        - `xunit.runner.visualstudio` (for IDE test discovery)
        - `Moq` (for mocking dependencies)
        - `Microsoft.EntityFrameworkCore.InMemory` (for testing EF Core without a real database)
    - [ ] **Sub-task 2.4:** Run `dotnet test` to verify the test project is set up correctly (it should run the default generated test).

- [ ] **Task 3: Write Your First Unit Test**
    > *Description: Create a simple test to understand the basic structure and AAA pattern.*
    - [ ] **Sub-task 3.1:** Create a new folder in the test project called `Repositories`.
    - [ ] **Sub-task 3.2:** Create a test class named `MySqlEmployersRepositoryTests.cs`.
    - [ ] **Sub-task 3.3:** Write a test method called `GetAllEmployers_WhenDatabaseIsEmpty_ReturnsEmptyList()`. This test should:
        - **Arrange:** Set up a mock or in-memory `DbContext` with no employers
        - **Act:** Call `GetAllEmployers()`
        - **Assert:** Verify the result is an empty list
    - [ ] **Sub-task 3.4:** Run the test using `dotnet test` and ensure it passes.

- [ ] **Task 4: Test the "Happy Path" CRUD Operations**
    > *Description: Write tests for each repository method assuming everything works correctly.*
    - [ ] **Sub-task 4.1:** Write a test: `CreateEmployer_WhenValidEmployer_AddsToDatabase()`
        - Arrange an in-memory `DbContext`
        - Act: call `CreateEmployer()`
        - Assert: verify the employer was added to the `DbContext`
    - [ ] **Sub-task 4.2:** Write a test: `GetEmployerById_WhenEmployerExists_ReturnsEmployer()`
    - [ ] **Sub-task 4.3:** Write a test: `UpdateEmployer_WhenEmployerExists_UpdatesEmployer()`
    - [ ] **Sub-task 4.4:** Write a test: `DeleteEmployer_WhenEmployerExists_RemovesEmployer()`
    - [ ] **Sub-task 4.5:** Run all tests and ensure they pass.

- [ ] **Task 5: Test Edge Cases & Failure Scenarios**
    > *Description: Write tests for what happens when things go wrong.*
    - [ ] **Sub-task 5.1:** Write a test: `GetEmployerById_WhenEmployerDoesNotExist_ReturnsNull()`
    - [ ] **Sub-task 5.2:** Write a test: `UpdateEmployer_WhenEmployerDoesNotExist_ReturnsFalse()`
    - [ ] **Sub-task 5.3:** Write a test: `DeleteEmployer_WhenEmployerDoesNotExist_ReturnsFalse()`
    - [ ] **Sub-task 5.4:** Consider: Do you need to test validation? Why or why not? (Hint: Where is validation happening in your architecture?)

- [ ] **Task 6: Refactor for Testability (Optional Challenge)**
    > *Description: If you used `Microsoft.EntityFrameworkCore.InMemory`, you've technically written integration tests. True unit tests would mock the `DbContext`.*
    - [ ] **Sub-task 6.1:** Research: What's the difference between using an in-memory database vs. mocking `DbContext` with Moq?
    - [ ] **Sub-task 6.2:** (Optional) Try writing one test that mocks the `DbContext` and `DbSet<Employer>`. Is it easier or harder than using in-memory?
    - [ ] **Sub-task 6.3:** Make a decision: Which approach will you use going forward? Document your reasoning.

- [ ] **Task 7: Review Test Quality & Conventions**
    > *Description: Ensure your tests follow best practices.*
    - [ ] **Sub-task 7.1:** Review your test naming. Do they clearly state: `MethodName_Scenario_ExpectedResult`?
    - [ ] **Sub-task 7.2:** Ensure each test has a clear AAA structure with comments or whitespace separating sections.
    - [ ] **Sub-task 7.3:** Run `dotnet test` with the `--logger "console;verbosity=detailed"` flag to see detailed output. Are all tests passing?
    - [ ] **Sub-task 7.4:** Check code coverage (optional): Install `coverlet.collector` and run tests with coverage reporting. What % of `MySqlEmployersRepository` is covered?

- [ ] **Task 8: Update Project Documentation & Version**
    > *Description: Reflect the new testing infrastructure in your project documentation.*
    - [ ] **Sub-task 8.1:** Update `README.md`:
        - Add a "Running Tests" section explaining how to run `dotnet test`
        - Add the test project to the "Project Structure" section
        - Update "Technologies Used" to include xUnit and Moq
    - [ ] **Sub-task 8.2:** Update `docs/ARCHITECTURE.md`:
        - Add a new section titled "Testing Strategy" explaining your approach (unit tests with in-memory DB or mocks)
        - Document what layers you're testing and why
    - [ ] **Sub-task 8.3:** Increment the version in `JobTracker.Api.csproj` to `0.3.0` (MINOR version bump for new testing feature).
    - [ ] **Sub-task 8.4:** Stage and commit your changes with the message: `feat: Add unit tests for MySqlEmployersRepository`
    - [ ] **Sub-task 8.5:** Create an annotated tag: `git tag -a v0.3.0 -m "Release 0.3.0: Unit testing infrastructure"`
    - [ ] **Sub-task 8.6:** Push your commits and tags to the remote repository.

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** `[AI will provide a brief, high-level summary of the work.]`

**Final Status:** `[Pass / Rework Needed / Incomplete]`

---

### Strengths

*`[AI will list specific things that were done correctly and well.]`*
* ...

### Areas for Improvement & Corrections

*`[AI will list specific, actionable feedback on what was done incorrectly or could be improved. This is where errors will be flagged.]`*
- [ ] **Correction 1:** ...
- [ ] **Correction 2:** ...

### Conceptual Gaps

*`[AI will identify any underlying conceptual misunderstandings based on the submitted work.]`*
* ...

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1. What is the purpose of **unit testing**, and how does it differ from **integration testing**?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

2. What does the **AAA pattern** (Arrange-Act-Assert) mean, and why is it useful?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

3. In your own words, what is a **mock**, and why do we use mocking libraries like Moq in unit tests?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

4. What is the difference between `Microsoft.EntityFrameworkCore.InMemory` and mocking `DbContext` with Moq? Which did you use, and why?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

5. According to the **Test Pyramid**, should you have more unit tests or more integration tests? Why?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1. **Test Coverage vs. Test Quality:** If you achieve 100% code coverage, does that mean your code has no bugs? Why or why not?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

2. **What NOT to Test:** Should you write unit tests for your DTOs (like `EmployerDto`)? What about your Minimal API endpoints? Explain your reasoning.  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

3. **Test-Driven Development (TDD):** In TDD, you write tests *before* writing code. What are the benefits of this approach? What are the drawbacks?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

4. **Testability & Design:** If a class is difficult to unit test (e.g., has many dependencies, or tightly coupled to external systems), what does that tell you about the class's design?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

5. **Mocking Strategy:** When writing tests for `MySqlEmployersRepository`, you need to decide whether to mock `DbContext` or use an in-memory database. What are the trade-offs of each approach in terms of test speed, complexity, and realism?  
   **Answer:**
   ```
   
   ```
   > **AI Feedback:** 

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
* **AI Final Feedback:**
* **Next Action:** [e.g., Start Sprint 8: BDD/Acceptance Testing, Improve test coverage, Refactor tests to use mocks].


# 🎯 Sprint 08: BDD & Acceptance Testing with SpecFlow

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** Behavior-Driven Development (BDD), Acceptance Testing, Gherkin Syntax, SpecFlow, Living Documentation, Outside-In Testing
* **Primary Goal (Must-Have):** By the end, I must be able to **write executable acceptance tests in plain English using Gherkin syntax that verify the API's behavior from a user's perspective, and explain the difference between unit tests and acceptance tests.**
* **Secondary Goals:**
    * Understand what BDD is and why it exists as a testing philosophy
    * Write feature files using Given-When-Then syntax
    * Implement step definitions that execute real HTTP requests against the API
    * Generate living documentation from acceptance tests
    * Understand when to use acceptance tests vs unit tests vs integration tests

---

## ✅ Task List

### Phase 1: Understanding BDD & Setup

- [X] **Task 1: Research the Philosophy of BDD**
    > *Description: Before writing any code, understand what problem BDD solves and how it differs from traditional testing approaches.*
    - [X] **Sub-task 1.1:** Read about the history of BDD and why Dan North created it (hint: it was born from TDD but focuses on behavior rather than tests)
    ```
    Dan North observed that developers often thought writing tests was beneath them, and testers did not trust developers to write tests, making adoption of TDD difficult. He realised that TDD was fundamentally a design practice, not just a testing technique. He reframed the conversation to thinking about writing examples of behaviour—describing how the system works from the user's perspective—instead of thinking about tests.

    The linguistic shift removed many obstacles and allowed teams to focus on what software should do, rather than how to test it.
    ```
    > **AI Feedback:** This is a perfect summary. You've hit on the key insight: BDD is a communication process that grew out of TDD's focus on design. The shift in language from "tests" to "examples of behavior" was revolutionary because it made the process inclusive for the whole team, not just developers.

    - [X] **Sub-task 1.2:** Research the "Three Amigos" concept in BDD (Developer, Tester, Business Analyst)
    ```
    This is a collaborative practice in BDD where the three key roles meet early in the dev cycle to discuss and align on user stories or requirements.
    **Business/Product Owner (The Why):** Describes the business value, user needs, and expected outcomes. 
    **Developer (The How):** Assesses technical feasibility, implementation approach, and constraints.  
    **Tester/QA (The What):** Identifies edge cases, defines testability, and ensures clarity for verification.  
    The goal is to build shared understanding, clarify acceptance criteria, and catch ambiguities before coding begins.
    ```
    > **AI Feedback:** Excellent. You've perfectly captured the roles and perspectives of the 'Three Amigos.' The most important takeaway here is that this collaboration happens *before* heavy development begins, making it cheap and easy to fix misunderstandings about requirements.

    - [X] **Sub-task 1.3:** Write a comment in a markdown file explaining: "Why would a product owner or non-technical stakeholder care about tests written in Gherkin?"
    ```
    Because they define the user needs and requirements that they care about.
    ```
    > **AI Feedback:** Your answer is the right core idea. To expand on it: Gherkin tests are valuable to a product owner because they are written in plain English. This means the feature files act as a **contract** and a **single source of truth**. The product owner can read a `Given/When/Then` scenario and confirm, "Yes, that is exactly the behavior I want." Because these files are also the *actual executable tests*, they can be confident that this documentation never gets out of date. It answers the question "What does the system do?" in a way everyone can understand and trust.

- [X] **Task 2: Set Up SpecFlow Test Project**
    > *Description: Create a new test project specifically for acceptance tests, separate from your unit tests.*
    - [X] **Sub-task 2.1:** Create a new xUnit project named `JobTracker.Api.AcceptanceTests`
    - [X] **Sub-task 2.2:** Install the required NuGet packages:
        - `SpecFlow.xUnit`
        - `SpecFlow.Tools.MsBuild.Generation`
        - `Microsoft.AspNetCore.Mvc.Testing` (for WebApplicationFactory)
        - `FluentAssertions` (for readable assertions)
    - [X] **Sub-task 2.3:** Add a project reference to `JobTracker.Api`
    - [X] **Sub-task 2.4:** Research what `specflow.json` configuration file does and create one in your test project root

### Phase 2: Your First Feature File

- [X] **Task 3: Write a Feature File for Employer CRUD**
    > *Description: Create your first Gherkin feature file that describes the behavior of the Employers API in plain English.*
    - [X] **Sub-task 3.1:** Create a `Features` folder in your acceptance test project
    - [X] **Sub-task 3.2:** Create `EmployerManagement.feature` with at least 3 scenarios:
        - Scenario 1: Creating a new employer successfully
        - Scenario 2: Retrieving an employer that exists
        - Scenario 3: Attempting to create an employer with invalid data (validation failure)
    - [X] **Sub-task 3.3:** Use proper Gherkin syntax with Given-When-Then structure
    - [X] **Sub-task 3.4:** Add a `Background` section if you have setup steps common to all scenarios
    - [X] **Sub-task 3.5:** Research and use `Scenario Outline` with `Examples` table for one scenario to test multiple validation cases

- [X] **Task 4: Understand the Generated Code**
    > *Description: When you build the project, SpecFlow generates code-behind files from your feature files. Understand what this generated code does.*
    - [X] **Sub-task 4.1:** Build the project and locate the `.feature.cs` file generated in `obj/Debug/`
    - [X] **Sub-task 4.2:** Open the generated file and examine how SpecFlow converts Gherkin into xUnit test methods
    - [X] **Sub-task 4.3:** Add a comment explaining: "What happens if I change the Gherkin text? Does the generated C# code change?"

### Phase 3: Implementing Step Definitions

- [X] **Task 5: Create Step Definitions Class**
    > *Description: Write the C# code that executes when each Given/When/Then step runs.*
    - [X] **Sub-task 5.1:** Create a `StepDefinitions` folder
    - [X] **Sub-task 5.2:** Create `EmployerManagementSteps.cs` class decorated with `[Binding]` attribute
    - [X] **Sub-task 5.3:** Research and understand what `ScenarioContext` is and inject it into your step definitions class
    - [X] **Sub-task 5.4:** Add a comment explaining: "Why do we use `ScenarioContext` to share data between steps instead of class fields?"
    ```
    BEcaus SpecFlow instantiates the StepDefinitions class fresh for every step, so ScenarioContext is a way of passing state from a given to a then step.
    ```

- [X] **Task 6: Set Up Test Infrastructure**
    > *Description: Create a reusable test harness that spins up your API in-memory for testing.*
    - [X] **Sub-task 6.1:** Create a `TestWebApplicationFactory<Program>` class that inherits from `WebApplicationFactory<Program>`
    - [ X] **Sub-task 6.2:** Override `ConfigureWebHost` to:
        - Use an in-memory database (different from production)
        - Register any test-specific services
        - Disable authentication if present
    - [X] **Sub-task 6.3:** Create a `[BeforeScenario]` hook that initializes the test factory and HTTP client
    - [X] **Sub-task 6.4:** Create an `[AfterScenario]` hook that cleans up resources
    - [X] **Sub-task 6.5:** Research: "What's the difference between `[BeforeScenario]` and `[BeforeTestRun]` hooks?"

- [X] **Task 7: Implement Step Definitions**
    > *Description: Write the actual C# code for each step in your feature file.*
    - [X] **Sub-task 7.1:** Implement all `[Given]` steps (setup preconditions)
    - [X] **Sub-task 7.2:** Implement all `[When]` steps (perform actions - HTTP requests)
    - [X] **Sub-task 7.3:** Implement all `[Then]` steps (verify outcomes using FluentAssertions)
    - [X] **Sub-task 7.4:** Use the HTTP client from your test factory to make real HTTP requests
    - [X] **Sub-task 7.5:** Store the HTTP response in `ScenarioContext` so subsequent steps can access it
    - [X] **Sub-task 7.6:** Add a comment explaining: "Why do we make real HTTP requests in acceptance tests instead of calling repository methods directly?"

### Phase 4: Advanced BDD Patterns

- [X] **Task 8: Add Background Data Seeding**
    > *Description: Many scenarios need pre-existing data. Learn to seed the database before tests.*
    - [X] **Sub-task 8.1:** Create a `[BeforeScenario]` hook that seeds test data into the in-memory database
    - [X] **Sub-task 8.2:** Use tags (e.g., `@SeedEmployers`) to conditionally seed data only for scenarios that need it
    - [X] **Sub-task 8.3:** Research: "What's the difference between using a `Background` section in Gherkin vs. a `[BeforeScenario]` hook with tags?"

### Phase 5: Testing Edge Cases & Error Paths

- [X] **Task 9: Add Negative Test Scenarios**
    > *Description: Good acceptance tests cover both happy paths and error conditions.*
    - [X] **Sub-task 9.1:** Add scenarios for:
        - Requesting a non-existent resource (404)
        - Sending malformed request bodies (400/422)
        - Violating validation rules
    - [X] **Sub-task 9.2:** In your `[Then]` steps, assert on HTTP status codes and error response bodies
    - [X] **Sub-task 9.3:** Verify that your API's RFC 7807 Problem Details format is returned correctly

- [X] **Task 10: Test the Full Workflow**
    > *Description: Write a scenario that tests a complete user journey across multiple endpoints.*
    - [X] **Sub-task 10.1:** Create a scenario that:
        - Creates a new employer (POST)
        - Retrieves it (GET by ID)
        - Updates it (PUT)
        - Deletes it (DELETE)
        - Verifies it's gone (GET returns 404)
    - [X] **Sub-task 10.2:** Use `ScenarioContext` to pass data (like the created employer's ID) between steps

### Phase 6: Commit Strategy & Retrospective

- [X] **Task 11: Plan Your Commits**
    > *Description: Before committing, plan logical commit boundaries.*
    - [X] **Sub-task 11.1:** Review the commit strategy from the sprint instructions
    - [X] **Sub-task 11.2:** Identify at least 3 logical commits for this sprint:
        1. **test(acceptance): setup SpecFlow project and infrastructure**
           - Project creation, packages, test factory, hooks
        2. **test(acceptance): implement employer CRUD feature scenarios**
           - Feature file, step definitions, basic CRUD tests
           - Use body to explain BDD approach and scenario coverage
        3. **test(acceptance): add negative scenarios and living documentation**
           - Error cases, edge cases, documentation generation
    - [X] **Sub-task 11.3:** Write down your planned commit messages before coding

- [X] **Task 12: Update Project Documentation & Version**
    > *Description: Keep documentation in sync with your new testing capabilities.*
    - [X] **Sub-task 12.1:** Update `README.md` (root) to mention acceptance testing
    - [X] **Sub-task 12.2:** Create or update `JobTracker.Api.AcceptanceTests/README.md` with:
        - What BDD/acceptance tests are
        - How to run them
    - [X] **Sub-task 12.3:** Update `docs/ARCHITECTURE.md` "Testing Strategy" section to include acceptance tests
    - [X] **Sub-task 12.4:** Increment version in `.csproj` following SemVer:
        - Since this adds a new testing capability (feature), increment MINOR version (e.g., 0.7.0 → 0.8.0)
    - [X] **Sub-task 12.5:** Create a conventional commit: `test: add BDD acceptance test suite with SpecFlow`
    - [X] **Sub-task 12.6:** Create an annotated Git tag: `git tag -a v0.8.0 -m "Add BDD acceptance testing with SpecFlow"`
    - [X] **Sub-task 12.7:** Push commits and tags to remote: `git push && git push --tags`

---

## 🤖 AI Marking & Feedback

### Overall Assessment
Excellent work! You have successfully built a robust BDD acceptance testing suite from the ground up. You demonstrated a strong grasp of how to integrate SpecFlow with `WebApplicationFactory` to create high-fidelity end-to-end tests. The code is clean, the tests are comprehensive (covering happy paths, error states, and full workflows), and the documentation is well-maintained.

### Final Status
**Pass**

### Strengths
1.  **Infrastructure Setup:** Your implementation of `TestWebApplicationFactory` and `TestHooks` correctly handles the lifecycle of the test server, ensuring a fresh in-memory database for every scenario. This is critical for reliable tests.
2.  **Comprehensive Coverage:** You didn't just stop at happy paths; you included negative scenarios (404, 422), verified RFC 7807 Problem Details, and added a complete CRUD workflow scenario.
3.  **Step Definition Organization:** You proactively refactored your step definitions into clear `Given`, `When`, and `Then` sections, making the code much more maintainable.

### Areas for Improvement & Corrections
1.  **ScenarioContext Type Safety:**
    *   **Issue:** You are casting objects from `ScenarioContext` (e.g., `(HttpClient)_scenarioContext["HttpClient"]`).
    *   **Correction:** While this works, it's safer to use the generic `Get<T>` method: `_scenarioContext.Get<HttpClient>("HttpClient")`. This provides better type safety and clearer intent.
2.  **Gherkin Consistency:**
    *   **Issue:** You initially had a mix of "response status should be" and "response should be".
    *   **Correction:** Consistency is key in BDD. Stick to one phrasing (e.g., "response status should be") to maximize step reuse. You fixed this during the sprint, which is great!

### Conceptual Gaps
*   **None identified.** You showed a solid understanding of the BDD workflow, the difference between unit and acceptance tests, and how to manage test state.

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the fundamental difference between a **unit test** and an **acceptance test** in the context of this project?  
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  What is the purpose of **Gherkin syntax** (Given-When-Then)? Why not just write tests in plain C#?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  In your own words, explain what **SpecFlow** does. How does it bridge the gap between Gherkin feature files and executable C# code?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  What is the role of `ScenarioContext` in SpecFlow? Why can't you just use class-level fields to share data between steps?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  When you run your acceptance tests, they make **real HTTP requests** to an in-memory version of your API. Explain why this is different from your unit tests that mock the repository.
    **Answer:**
    ```
    
    ```
    > **AI Feedback:**

6.  Explain the purpose of `WebApplicationFactory<Program>` and why you needed to override `ConfigureWebHost`. In your `TestWebApplicationFactory`, you remove and replace the `DbContextOptions<JobTrackerDbContext>` service registration—why is this necessary instead of just adding the in-memory database?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

7.  **Workflow & Component Interconnection:** Trace the complete execution flow when a single scenario runs. Start from when xUnit discovers the test, and explain what happens in order: which classes are instantiated, which methods are called, and how data flows between them. Include: `TestHooks`, `TestWebApplicationFactory`, `ScenarioContext`, `EmployerManagementSteps`, and the actual API.
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

8.  **ScenarioContext Deep Dive:** You used `ScenarioContext` to pass data between steps (e.g., `HttpClient`, `EmployerId`, `Response`). Why is this necessary? What would happen if you tried to use class fields in `EmployerManagementSteps` instead? How does SpecFlow ensure `ScenarioContext` is thread-safe when running tests in parallel?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

9.  **TestWebApplicationFactory Lifecycle:** Your `TestHooks` creates a new `TestWebApplicationFactory` before each scenario and disposes it after. What would happen if you created the factory once in a `[BeforeTestRun]` hook and reused it for all scenarios? What problems would this cause?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

10. **Component Dependency Map:** Draw or describe the dependency relationships between these components: `EmployerManagement.feature`, `EmployerManagementSteps`, `TestHooks`, `TestWebApplicationFactory`, `ScenarioContext`, `HttpClient`, `Program.cs`, and `JobTrackerDbContext`. Which components know about which other components? Which are created first?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** You were instructed to use `WebApplicationFactory<Program>` to spin up an in-memory version of your API for acceptance tests. What are the trade-offs of this approach compared to:
    - (A) Running tests against a real, deployed instance of the API?
    - (B) Mocking all dependencies and testing controllers in isolation?
    
    What would happen if you didn't use `WebApplicationFactory` at all and instead called your service/repository methods directly in step definitions?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  **The Testing Pyramid:** You now have both unit tests (Sprint 7) and acceptance tests (Sprint 8). Research the concept of the "Testing Pyramid" (or "Testing Trophy"). Where do unit tests and acceptance tests fit in this pyramid? Why do most teams have more unit tests than acceptance tests?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  **Living Documentation vs Static Documentation:** You generated an HTML report from your feature files. How is this "living documentation" fundamentally different from the `README.md` and Swagger documentation you created in Sprint 5? What prevents living documentation from becoming outdated?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  **Critical Thinking - The BDD Trap:** A common mistake when implementing BDD is writing feature files that are too technical or implementation-focused (e.g., "Given the repository returns entity X"). Why is this a problem? How does it undermine the purpose of BDD? What's a better way to phrase that scenario?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  **Connecting Concepts:** In Sprint 6, you implemented FluentValidation to validate incoming DTOs. How do your acceptance tests verify that this validation is working correctly? Why is testing validation in acceptance tests different from testing validation in unit tests?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

6.  **The Three Amigos:** BDD emphasizes collaboration between developers, testers, and business stakeholders (the "Three Amigos"). In a real project, how would you involve a non-technical product owner in writing feature files? What would you gain from their participation that you wouldn't get from writing tests alone?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

7.  **Performance Considerations:** Acceptance tests that spin up a full in-memory API and make real HTTP requests are slower than unit tests. If your test suite takes 10 seconds to run unit tests but 2 minutes to run acceptance tests, how would you structure your CI/CD pipeline to balance speed and confidence? When would you run each type of test?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
* **Next Action:** [e.g., Start Sprint 9 for CI/CD, Deep dive into Stretch Question 4 about technical vs behavioral scenarios]

**AI Final Feedback:**
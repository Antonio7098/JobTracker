# 🎯 Learning Sprint 6: Validation & Error Handling

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** **Input Validation, Error Handling, HTTP Status Codes, Problem Details Standard, FluentValidation**
* **Primary Goal (Must-Have):** By the end, I must be able to **implement comprehensive input validation for API endpoints, create a consistent error response strategy using the Problem Details standard, and verbally explain the trade-offs between different validation approaches and when to use specific HTTP status codes**.
* **Secondary Goals:**
    * Understand where validation should occur in a layered architecture (API vs Service vs Database)
    * Implement validation rules that provide clear, actionable error messages
    * Handle different error types (validation errors, not found, server errors) with appropriate HTTP status codes
    * Explain how validation integrates with ASP.NET Core's request pipeline
    * Document error responses in Swagger/OpenAPI for API consumers
    * Update project documentation to reflect the new validation and error handling patterns
    * Formally version the release using Semantic Versioning

---

## ✅ Task List

- [X] **Task 1: Research Validation Approaches**
    > *Description: Before implementing validation, you need to understand the available approaches and their trade-offs.*
    - [X] **Sub-task 1.1:** Research the difference between **Data Annotations** (e.g., `[Required]`, `[MaxLength]`) and **FluentValidation**. What are the pros and cons of each?
    **Answer:**
    ```
    Data Annotations are applied directly to your models and dts's. They are simpler and require less knowledge overhead. Fluent Validation is a package that you get fom Nuget. It allows for much more coplex validation, and separation of concerns: validation logic lives in separate validator classes, not cluttering the dtos, and is more tastable and reusable.
    ```
    - [X] **Sub-task 1.2:** Read about where validation should occur in a multi-layered application. Should it happen at the API layer, service layer, or both? Why?
    **Answer:**
    ```
    API layer valdates the structure and format of the input eg. "is the Name field present". Service/repository validates the database acces, or complex ligic, eg "does an employer with this name already exist". Both layers are importsnt.
    ```
    - [X] **Sub-task 1.3:** Document your findings in a comment or markdown note explaining when you would choose Data Annotations vs FluentValidation.

- [X] **Task 2: Implement FluentValidation for DTOs**
    > *Description: Add FluentValidation to the project and create validators for your DTOs.*
    - [X] **Sub-task 2.1:** Use NuGet to find and install the appropriate FluentValidation package for ASP.NET Core.
    - [X] **Sub-task 2.2:** Create a `Validators` folder in your project.
    - [X] **Sub-task 2.3:** Create a `CreateEmployerDtoValidator` class that validates `CreateEmployerDto`. Define rules for:
        - `Name` (required, max length, not just whitespace)
        - `CompanyDescription` (optional but if provided, has a max length)

    **Note:**
    ```
    Company description was added to the employer model at this point.
    ```
    - [X] **Sub-task 2.4:** Create an `UpdateEmployerDtoValidator` with similar rules.
    - [X] **Sub-task 2.5:** Research and then register FluentValidation with the Dependency Injection container in `Program.cs`. How does the framework discover your validators?

- [X] **Task 3: Understand HTTP Status Codes & Problem Details**
    > *Description: Before handling errors, understand the semantic meaning of HTTP status codes and the Problem Details standard.*
    - [X] **Sub-task 3.1:** Research the semantic difference between:
        - `400 Bad Request`
        - `404 Not Found`
        - `422 Unprocessable Entity`
        - `500 Internal Server Error`
    - [X] **Sub-task 3.2:** Read about RFC 7807 "Problem Details for HTTP APIs". What problem does it solve? What are the standard fields in a Problem Details response?
    - [X] **Sub-task 3.3:** Investigate how ASP.NET Core supports Problem Details. Look into the `TypedResults.ValidationProblem()` and `TypedResults.Problem()` methods.

- [X] **Task 4: Implement Validation in Endpoints**
    > *Description: Manually trigger validation in your endpoints and return appropriate error responses.*
    - [X] **Sub-task 4.1:** In your `CreateEmployer` endpoint, inject the `IValidator<CreateEmployerDto>` and call its `ValidateAsync()` method.
    - [X] **Sub-task 4.2:** If validation fails, return a `ValidationProblem` result with a `422 Unprocessable Entity` status code. Ensure the validation errors are included in the response.
    - [X] **Sub-task 4.3:** Repeat this process for the `UpdateEmployer` endpoint.
    - [X] **Sub-task 4.4:** Test your validation by sending invalid requests using your `.http` file. Verify the response structure and status codes.

- [ ] **Task 5: Implement "Not Found" Error Handling**
    > *Description: Handle cases where a requested resource doesn't exist.*
    - [ ] **Sub-task 5.1:** In your `GetEmployerById`, `UpdateEmployer`, and `DeleteEmployer` endpoints, check if the employer exists before proceeding.
    - [ ] **Sub-task 5.2:** If the employer is not found, return a `Problem` result with:
        - Status code: `404 Not Found`
        - Title: "Employer not found"
        - Detail: A descriptive message (e.g., "The employer with ID {id} was not found.")
    - [ ] **Sub-task 5.3:** Test these scenarios by requesting non-existent IDs in your `.http` file.

- [ ] **Task 6: Implement Global Exception Handling**
    > *Description: Handle unexpected exceptions gracefully and consistently.*
    - [ ] **Sub-task 6.1:** Research ASP.NET Core's exception handling middleware. What is `UseExceptionHandler()` and how does it work?
    - [ ] **Sub-task 6.2:** In `Program.cs`, configure the exception handler to return a `500 Internal Server Error` Problem Details response for unhandled exceptions.
    - [ ] **Sub-task 6.3:** Ensure that in Development mode, the exception details are visible, but in Production mode, they are hidden (for security).
    - [ ] **Sub-task 6.4:** Deliberately cause an exception (e.g., throw an exception in an endpoint) and verify that your global handler catches it and returns the correct response.

- [ ] **Task 7: Update API Documentation for Error Responses**
    > *Description: Enhance the Swagger documentation to reflect the new validation and error handling behavior.*
    - [ ] **Sub-task 7.1:** Add `.Produces<ProblemDetails>(422)` to endpoints that perform validation (e.g., `CreateEmployer`, `UpdateEmployer`) to document that they can return validation errors.
    - [ ] **Sub-task 7.2:** Add `.Produces<ProblemDetails>(404)` to endpoints that can return "not found" errors (e.g., `GetEmployerById`, `UpdateEmployer`, `DeleteEmployer`).
    - [ ] **Sub-task 7.3:** Add `.Produces<ProblemDetails>(500)` to document that any endpoint can return a server error.
    - [ ] **Sub-task 7.4:** Update the XML documentation comments (or `.WithSummary()` / `.WithDescription()`) to mention the possible error scenarios for each endpoint.
    - [ ] **Sub-task 7.5:** Test the Swagger UI (`/swagger`) to verify that error responses are now documented with their schemas.

- [ ] **Task 8: Update Project Documentation & Version**
    > *Description: Update high-level documentation to reflect the new validation capabilities and formally version this release.*
    - [ ] **Sub-task 8.1:** Update the `README.md` file to include a new section called "Error Handling" that explains:
        - The Problem Details standard (RFC 7807) used for all error responses
        - The different HTTP status codes returned by the API (404, 422, 500)
        - An example of a validation error response
    - [ ] **Sub-task 8.2:** Update the `docs/ARCHITECTURE.md` file to add a new section describing:
        - The validation strategy (FluentValidation at the API layer)
        - Where validation occurs in the request pipeline
        - The global exception handling approach
    - [ ] **Sub-task 8.3:** In `JobTracker.Api.csproj`, increment the version from `0.1.0` to `0.2.0` (a MINOR version bump, since we're adding new functionality without breaking existing endpoints).
    - [ ] **Sub-task 8.4:** Commit all changes with the message: `feat: add input validation and error handling with FluentValidation and Problem Details`.
    - [ ] **Sub-task 8.5:** Create an annotated Git tag for this release: `git tag -a v0.2.0 -m "v0.2.0: Add comprehensive validation and error handling"`.
    - [ ] **Sub-task 8.6:** Push your commits and the new tag to the remote repository.

- [ ] **Task 9: Add Validation to JobVacancy Endpoints (Stretch)**
    > *Description: Apply what you've learned to the JobVacancy resource.*
    - [ ] **Sub-task 9.1:** Create DTOs for `JobVacancy` if they don't exist yet (`CreateJobVacancyDto`, `UpdateJobVacancyDto`, `JobVacancyDto`).
    - [ ] **Sub-task 9.2:** Create validators for the JobVacancy DTOs with appropriate rules (e.g., `Title` required, `EmployerId` must be a valid GUID).
    - [ ] **Sub-task 9.3:** Implement CRUD endpoints for `JobVacancy` with full validation and error handling.
    - [ ] **Sub-task 9.4:** Add a validation rule that ensures the `EmployerId` references an existing employer. Where should this check happen?

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
- [ ] **Correction 1:** ...
- [ ] **Correction 2:** ...

### Conceptual Gaps

*`[AI will identify any underlying conceptual misunderstandings based on the submitted work.]`
* ...

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the difference between **Data Annotations** and **FluentValidation** in the context of ASP.NET Core input validation? What are the advantages of each?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  What is the semantic difference between HTTP status codes **400 Bad Request**, **422 Unprocessable Entity**, and **404 Not Found**? When should each be used?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  What is **RFC 7807** (Problem Details for HTTP APIs)? What problem does it solve, and what are the standard fields in a Problem Details response?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  In your own words, how does FluentValidation integrate with ASP.NET Core's dependency injection system? How does the framework discover your validators?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  Why is it important to document error responses (404, 422, 500) in the Swagger/OpenAPI specification using `.Produces<ProblemDetails>()`? What happens if you don't document them?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Architecture & Responsibility:** Where should validation logic live in a well-architected application? Should it be at the API endpoint level, the service/repository level, or both? What are the trade-offs of each approach? Consider scenarios like: validating a GUID format vs. checking if a referenced entity exists in the database.
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

2.  **Trade-Offs:** You were instructed to use **FluentValidation**. What are the trade-offs of this approach compared to using **Data Annotations** directly on your DTOs? In what scenarios would Data Annotations be more appropriate? When would FluentValidation be overkill?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

3.  **Error Handling Strategy:** What happens if you *don't* implement global exception handling in your API? How does ASP.NET Core handle unhandled exceptions by default? What information might be leaked to clients in production, and why is this a security concern?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

4.  **Validation Performance:** FluentValidation supports both synchronous and asynchronous validation rules. When would you use `MustAsync()` instead of `Must()`? What are the performance implications? What happens if you use a synchronous validator with an async database call?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

5.  **Real-World Scenario:** Imagine a client sends a `POST` request to create an `Employer` with a valid name, but the database is temporarily unavailable and throws a `TimeoutException`. Walk through what happens: What status code should be returned? What error details should be included? How does your global exception handler deal with this?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

6.  **Versioning & Breaking Changes:** We incremented the version from `0.1.0` to `0.2.0` (a MINOR bump). However, imagine that instead of returning Problem Details, we changed the error response format in a way that breaks existing clients. What version number should we use then? Why is it critical to understand the difference between MAJOR, MINOR, and PATCH versions when evolving an API?
    **Answer:**
    ```
    
    ```
    > **AI Feedback:** 

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
* **AI Final Feedback:**
* **Next Action:** [e.g., Start Sprint 7: Unit Testing, Deep dive into async validation].


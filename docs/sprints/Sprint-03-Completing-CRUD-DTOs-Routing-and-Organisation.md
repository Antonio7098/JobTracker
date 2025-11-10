# 🎯 Learning Sprint: Sprint 3 - Completing CRUD, DTOs, and Routing Organization

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** **Data Transfer Objects (DTOs)**, Minimal API Endpoints (PUT, DELETE), HTTP Status Codes, **Extension Methods**, **Route Groups**.
* **Primary Goal (Must-Have):** By the end, I must be able to **successfully implement the PUT (Update) and DELETE (Delete) endpoints for the `Employer` resource, using DTOs to handle request data.**
* **Secondary Goals:**
    * Create and utilize appropriate DTOs for creating and updating the `Employer` resource.
    * Refactor the `Program.cs` file using **Extension Methods** and **Route Groups** to logically organize the API code.
    * Use correct **HTTP status codes** for successful updates, successful deletions, and not found scenarios.

---


## ✅ Task List

- [X] **Task 1: Introduce Data Transfer Objects (DTOs)**
    > *Description: Create the C# records or classes that will define the contract with the client, ensuring your domain model is never exposed directly.*
    - [X] Sub-task 1.1: Create a new folder named `DTOs`.
    - [X] Sub-task 1.2: Define a **Record** (or class) named `CreateEmployerDto`. This should only contain the properties necessary to create a new employer (e.g., Name, Description).
    - [X] Sub-task 1.3: Define a **Record** (or class) named `UpdateEmployerDto`. This should contain the properties that can be updated.
    - [X] Sub-task 1.4: Define an **Record** (or class) named `EmployerDto`. This should be what is returned to the client (may be slightly different from the domain model).

- [X] **Task 2: Update Repository to Handle Update/Delete**
    > *Description: Add the necessary logic to your `IEmployersRepository` and `InMemoryEmployersRepository` to complete the CRUD pattern.*
    - [X] Sub-task 2.1: Update the `IEmployersRepository` interface with method signatures for `UpdateEmployer` and `DeleteEmployer`.
    - [X] Sub-task 2.2: Implement `UpdateEmployer` in the concrete repository class. This method should handle finding the employer by ID, applying the changes, and returning a result indicating success or failure (e.g., a boolean).
    - [X] Sub-task 2.3: Implement `DeleteEmployer` in the concrete repository class. This method should remove the employer by ID and return a boolean indicating success or failure.

- [X] **Task 3: Implement PUT (Update) and DELETE Endpoints**
    > *Description: Add the final two endpoints for the `Employer` resource using the new DTOs and repository methods.*
    - [X] Sub-task 3.1: Implement the **`PUT /employers/{id}`** endpoint. It must accept the `UpdateEmployerDto` in the request body, find the employer by the route ID, perform the update (mapping the DTO to the domain model), and return the correct status code (e.g., **204 No Content** on success, **404 Not Found** on failure).
    - [X] Sub-task 3.2: Implement the **`DELETE /employers/{id}`** endpoint. It must remove the employer and return the correct status code (e.g., **204 No Content** on success, **404 Not Found** on failure).

- [X] **Task 4: Refactor with Organization Techniques**
    > *Description: Clean up `Program.cs` by applying organizational patterns from the guide.*
    - [X] Sub-task 4.1: Use the `app.MapGroup()` method to group all `/employers` endpoints under a single **Route Group**.
    - [X] Sub-task 4.2: Create a new **static extension class** (e.g., `EmployersEndpointsExtensions`) and move all the employer endpoint mapping code out of `Program.cs` and into a new **Extension Method** (e.g., `MapEmployersEndpoints`). Call this extension method from `Program.cs`.

- [X] **Task 5: Test the New Endpoints**
    > *Description: Update your `employers.http` file to test the full CRUD cycle.*
    - [X] Sub-task 5.1: Add a request to successfully `PUT` (update) an existing employer's name.
    - [X] Sub-task 5.2: Add a request to `DELETE` an existing employer.
    - [X] Sub-task 5.3: Add a final `GET` request to verify the deleted employer is gone and the updated one reflects the changes.

- [X] **Task 6: Refactor Mapping Logic (Self-Initiated)**
    > *Description: Further improve code cleanliness by moving mapping logic out of endpoint handlers and into a dedicated static class.*
    - [X] Sub-task 6.1: Create a new `Maps` folder and a static `EmployerMaps` class.
    - [X] Sub-task 6.2: Implement a `ToDto` extension method to convert an `Employer` model to an `EmployerDto`.
    - [X] Sub-task 6.3: Implement a `ToEmployer` extension method to convert a `CreateEmployerDto` to an `Employer` model.
    - [X] Sub-task 6.4: Refactor the endpoint handlers in `EmployersEndpointsExtensions` to use the new, cleaner mapping methods.

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** `Excellent work! After several rounds of corrections and debugging, you have successfully met all primary and secondary goals for this sprint. Your final code is clean, correct, and well-organized.`

**Final Status:** `Pass`

---

### Strengths

* **Persistence and Debugging:** You did a fantastic job sticking with the problems, from build errors to runtime issues. Working through these issues patiently is one of the most important skills for a developer.
* **Great Architectural Questions:** You asked insightful questions about mapping DTOs and repository responsibilities. This shows you're thinking deeply about *why* you're doing things, not just *how*.
* **Clean Final Code:** The final version of your code properly separates concerns, uses DTOs for all public contracts, and organizes endpoints logically in their own file.

### Areas for Improvement & Corrections

*
[X] **Correction 1: Consistent DTO Usage:** Initially, the `POST` and `GET` endpoints were exposing the internal `Employer` model. We corrected this by ensuring all endpoints that communicate with the client use a DTO (`CreateEmployerDto`, `UpdateEmployerDto`, or `EmployerDto`), and mapping between the DTO and the domain model happens within the endpoint.
[X] **Correction 2: API Organization:** `Program.cs` was initially becoming cluttered. We fixed this by refactoring all the endpoint logic into a separate `EmployerEndpoints.cs` file using a static extension method and a route group. This greatly improved readability and organization.
[X] **Correction 3: HTTP Testing and Syntax:** We worked through several common testing issues, including invalid JSON (missing quotes), and `.http` file to ensure a reliable test workflow.

### Conceptual Gaps

*These were the concepts we worked on and clarified during the review.*
* **API Organization with Extension Methods:** We clarified how to use extension methods to attach your own code to framework classes like `WebApplication`, enabling you to neatly organize your endpoint logic outside of `Program.cs`.
* **HTTP Testing Best Practices:** We established the importance of checking HTTP status codes, ensuring valid request syntax, and following a logical Create -> Act -> Verify pattern when testing an API.

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the **primary difference in purpose** between a **`CreateEmployerDto`** and the **`Employer` Domain Model**?
    **Answer:**
    ```
    The Employer is the internal entity that is stored in the system. The DTO is for transferring data through the application, and to and from the client. This pattern is efficient because you are only exposing the data you want to the client. Furthermore, internal changes to the Employer model will not affect the endpoints, so long as the mapping remains correct.
    ```
    > **AI Feedback:** Correct! This is a perfect summary. You've hit on the three key benefits: separating your internal model from your public contract, controlling exactly what data gets exposed, and decoupling your API from internal model changes.

2.  What is the recommended **HTTP status code** for a successful **PUT** operation where the response body is intentionally empty?
    **Answer:**
    ```
    204 no content
    ```
    > **AI Feedback:** Correct. `204 No Content` is the standard way to signal that the server successfully fulfilled the request but has no data to send back, which is common for `PUT` and `DELETE` operations.

3.  How is a **C# Extension Method** defined, and what is the significance of the `this` keyword in its first parameter?
    **Answer:**
    ```
    you define a class and use the this key word on methods to show what class you are extending. This allows you to attach methods to existing classes, and call them as if they belonged to the class itself.
    ```
    > **AI Feedback:** This is a great summary and captures the core concept perfectly. Just to add two small but important details: the method itself must be `static`, and the class that contains it must also be `static`.

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs (DTO Mapping):** In your `PUT` endpoint, you had to manually map data from the `UpdateEmployerDto` to the existing `Employer` domain model. If you failed to map a property (e.g., you forgot to copy the `Name` property over), what would the client see when they immediately requested the resource again? **How does this risk change** if you were using a library like **AutoMapper** (which is not covered yet)?
    **Answer:**
    ```
    They would not see the property they changes, assuming it was the one you forgot to copy, and think the put request has not worked. Not sure what AutoMapper does.
    ```
    > **AI Feedback:** You are exactly right about the first part. The client would see the old data and assume the update failed.
    > As for AutoMapper, it's a library that automates these mappings. You configure it once (e.g., `CreateMap<UpdateEmployerDto, Employer>()`), and it handles copying the properties for you, usually by matching property names. This reduces the risk of forgetting to map a field, but it can also make the code less explicit and sometimes hide issues if property names change.

2.  **Architecture:** You used a **Route Group** to organize your endpoints. Besides code cleanliness, what is one major **architectural benefit** of using a route group (e.g., related to authorization or middleware) that applies equally to *all* endpoints within that group?
    **Answer:**
    ```
    Not sure. DOes it make it easier to apply authorisation andd middleware?
    ```
    > **AI Feedback:** That's exactly it! You can apply filters or middleware to an entire group with a single line of code. For example, you could add `.RequireAuthorization()` to the group, and suddenly all employer endpoints would require a user to be logged in. This is much cleaner and safer than adding it to each endpoint individually.

3.  **Critical Thinking (HTTP Verbs):** You implemented an **Update** using **PUT**. If a client only wanted to update *one* field (like the company's name) and left the other fields in the DTO blank, what might happen to the other fields in your repository? What is the standard **HTTP verb** that is better suited for *partial* updates, and why?
    **Answer:**
    ```
    The other fields would not exist. Patch would be more suited to this
    ```
    > **AI Feedback:** Correct, `PATCH` is the verb for partial updates. In your specific code, because the `UpdateEmployerDto` only has a `Name` property, any other properties on the `Employer` model (like `Description`, if you added it) would be unaffected.
    > However, the reason `PATCH` is better is because `PUT` implies a *full replacement* of the resource. A very strict `PUT` implementation would interpret any omitted fields in the DTO as a request to set those fields to `null` or their default value on the server, which is often not what the client intends. `PATCH` avoids this ambiguity.

---

## 📝 Sprint Review

* **Status:** Done
* **Most Difficult Insight:** The separation of conerns between the endpoints and services.
**AI Final Feedback:**
* **Next Action:** Sprint 4.

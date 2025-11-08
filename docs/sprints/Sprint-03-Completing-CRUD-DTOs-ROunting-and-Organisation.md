# 🎯 Learning Sprint Template: Sprint 3 - Completing CRUD, DTOs, and Routing Organization

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** **Data Transfer Objects (DTOs)**, Minimal API Endpoints (PUT, DELETE), HTTP Status Codes, **Extension Methods**, **Route Groups**.
* **Primary Goal (Must-Have):** By the end, I must be able to **successfully implement the PUT (Update) and DELETE (Delete) endpoints for the `Employer` resource, using DTOs to handle request data.**
* **Secondary Goals:**
    * Create and utilize appropriate DTOs for creating and updating the `Employer` resource.
    * Refactor the `Program.cs` file using **Extension Methods** and **Route Groups** to logically organize the API code.
    * Use correct **HTTP status codes** for successful updates, successful deletions, and not found scenarios.

---


### Task List

- [X] - **Task 1: Introduce Data Transfer Objects (DTOs)**  
    Description: Create the C# records or classes that will define the contract with the client, ensuring your domain model is never exposed directly.
    [X] - **Sub-task 1.1:** Create a new folder named `DTOs`.  
    [X] - **Sub-task 1.2:** Define a **Record** (or class) named `CreateEmployerDto`. This should only contain the properties necessary to create a new employer (e.g., Name, Description).  
    [X] - **Sub-task 1.3:** Define a **Record** (or class) named `UpdateEmployerDto`. This should contain the properties that can be updated.  
    [X] - **Sub-task 1.4:** Define an **Record** (or class) named `EmployerDto`. This should be what is returned to the client (may be slightly different from the domain model).  

[ ] - **Task 2: Update Repository to Handle Update/Delete**
    Description: Add the necessary logic to your `IEmployersRepository` and `InMemoryEmployersRepository` to complete the CRUD pattern.
    [ ] - **Sub-task 2.1:** Update the `IEmployersRepository` interface with method signatures for `UpdateEmployer` and `DeleteEmployer`.
    [ ] - **Sub-task 2.2:** Implement `UpdateEmployer` in the concrete repository class. This method should handle finding the employer by ID, applying the changes, and returning a result indicating success or failure (e.g., a boolean).
    [ ] - **Sub-task 2.3:** Implement `DeleteEmployer` in the concrete repository class. This method should remove the employer by ID and return a boolean indicating success or failure.

[ ] - **Task 3: Implement PUT (Update) and DELETE Endpoints**
    Description: Add the final two endpoints for the `Employer` resource using the new DTOs and repository methods.
    [ ] - **Sub-task 3.1:** Implement the **`PUT /employers/{id}`** endpoint. It must accept the `UpdateEmployerDto` in the request body, find the employer by the route ID, perform the update (mapping the DTO to the domain model), and return the correct status code (e.g., **204 No Content** on success, **404 Not Found** on failure).
    [ ] - **Sub-task 3.2:** Implement the **`DELETE /employers/{id}`** endpoint. It must remove the employer and return the correct status code (e.g., **204 No Content** on success, **404 Not Found** on failure).

[ ] - **Task 4: Refactor with Organization Techniques**
    Description: Clean up `Program.cs` by applying organizational patterns from the guide.
    [ ] - **Sub-task 4.1:** Use the `app.MapGroup()` method to group all `/employers` endpoints under a single **Route Group**.
    [ ] - **Sub-task 4.2:** Create a new **static extension class** (e.g., `EmployersEndpointsExtensions`) and move all the employer endpoint mapping code out of `Program.cs` and into a new **Extension Method** (e.g., `MapEmployersEndpoints`). Call this extension method from `Program.cs`.

[ ] - **Task 5: Test the New Endpoints**
    Description: Update your `employers.http` file to test the full CRUD cycle.
    [ ] - **Sub-task 5.1:** Add a request to successfully `PUT` (update) an existing employer's name.
    [ ] - **Sub-task 5.2:** Add a request to `DELETE` an existing employer.
    [ ] - **Sub-task 5.3:** Add a final `GET` request to verify the deleted employer is gone and the updated one reflects the changes.

---

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** ``

**Final Status:** ``

---

### Strengths

*
* ...

### Areas for Improvement & Corrections

*
[ ] **Correction 1:**
[ ] **Correction 2:**

### Conceptual Gaps

*
* ...

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the **primary difference in purpose** between a **`CreateEmployerDto`** and the **`Employer` Domain Model**?
    **Answer:**
    ```
    [Your answer here]
    ```
    > **AI Feedback:** 

2.  What is the recommended **HTTP status code** for a successful **PUT** operation where the response body is intentionally empty?
    **Answer:**
    ```
    [Your answer here]
    ```
    > **AI Feedback:** 

3.  How is a **C# Extension Method** defined, and what is the significance of the `this` keyword in its first parameter?
    **Answer:**
    ```
    [Your answer here]
    ```
    > **AI Feedback:** 

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs (DTO Mapping):** In your `PUT` endpoint, you had to manually map data from the `UpdateEmployerDto` to the existing `Employer` domain model. If you failed to map a property (e.g., you forgot to copy the `Name` property over), what would the client see when they immediately requested the resource again? **How does this risk change** if you were using a library like **AutoMapper** (which is not covered yet)?
    **Answer:**
    ```
    [Your answer here]
    ```
    > **AI Feedback:** 

2.  **Architecture:** You used a **Route Group** to organize your endpoints. Besides code cleanliness, what is one major **architectural benefit** of using a route group (e.g., related to authorization or middleware) that applies equally to *all* endpoints within that group?
    **Answer:**
    ```
    [Your answer here]
    ```
    > **AI Feedback:** 

3.  **Critical Thinking (HTTP Verbs):** You implemented an **Update** using **PUT**. If a client only wanted to update *one* field (like the company's name) and left the other fields in the DTO blank, what might happen to the other fields in your repository? What is the standard **HTTP verb** that is better suited for *partial* updates, and why?
    **Answer:**
    ```
    [Your answer here]
    ```
    > **AI Feedback:** 

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
**AI FInal Feedback:**
* **Next Action:** [e.g., Start Sprint Template for next concept, Deep dive into Stretch Question 3].
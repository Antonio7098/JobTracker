# 🎯 Learning Sprint Template: Sprint 2 - Minimal API CRUD & Service Introduction

---

## 📅 Sprint Details & Goals

* **Concepts/Topics:** REST API Fundamentals, Minimal API Endpoints (GET, POST), In-Memory Data Storage, Service Decoupling.
* **Primary Goal (Must-Have):** By the end, I must be able to **implement a dedicated C# data repository service and use it to successfully handle GET (List/Single) and POST (Create) requests for the `Employer` resource.**
* **Secondary Goals:**
    * Create a simple, static, in-memory collection (like a `List<T>`) to store data.
    * Structure the application to separate data access logic from endpoint handling.
    * Successfully test the GET and POST endpoints using the **Rest Client** extension.

---


### Task List

[ ] - **Task 1: Create the Data Repository Service**
    Description: Create a separate C# class that will act as your dedicated data access layer (DAL). This class will initially use an in-memory list to store `Employer` objects.
    [X] - **Sub-task 1.1:** Create a new folder named `Services` or `Repositories`.
    [ ] - **Sub-task 1.2:** Define an **interface** named `IEmployersRepository` with method signatures for creating, listing, and getting a single Employer.
    [ ] - **Sub-task 1.3:** Create a concrete class named `InMemoryEmployersRepository` that implements the interface. Include a private, static `List<Employer>` in this class to simulate the data store.
    [ ] - **Sub-task 1.4:** Implement the methods to **add** a new employer to the list and **retrieve** all employers from the list.

[ ] - **Task 2: Register the Service via Dependency Injection (DI)**
    Description: Use the `WebApplicationBuilder` in `Program.cs` to register your new service so that the application can use it.
    [ ] - **Sub-task 2.1:** In `Program.cs`, use the `Builder.Services` property to register the `InMemoryEmployersRepository` against the `IEmployersRepository` interface. You must choose an appropriate **service lifetime** (e.g., Singleton, Scoped, Transient).

[ ] - **Task 3: Implement Minimal API Endpoints**
    Description: Create the Minimal API endpoints in `Program.cs` to handle requests for the `Employer` resource, utilizing the newly registered repository service.
    [ ] - **Sub-task 3.1:** Implement a `GET /employers` endpoint to retrieve and return the list of all employers using the injected `IEmployersRepository`.
    [ ] - **Sub-task 3.2:** Implement a `GET /employers/{id}` endpoint to retrieve a single employer by its ID. Remember to handle the case where the ID does not exist.
    [ ] - **Sub-task 3.3:** Implement a `POST /employers` endpoint to create a new employer. This endpoint should accept an `Employer` object in the request body, add it using the repository, and return a **201 Created** status code.

[ ] - **Task 4: API Testing with Rest Client**
    Description: Write test files to prove your endpoints work as expected.
    [ ] - **Sub-task 4.1:** Create a file named `employers.http` in the root of your project.
    [ ] - **Sub-task 4.2:** Use the Rest Client syntax to define and execute the `POST` request to create two sample employers.
    [ ] - **Sub-task 4.3:** Define and execute the `GET` request to retrieve the full list of employers, confirming the two you created are present.

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

## ✅ Consolidation Questions (Test of Basic Understanding)

*These questions focus on recall, definition, and direct application.*

1.  What is the **main difference** between **GET** and **POST** requests in terms of how they affect the server's state?
2.  What is a **service interface** (like `IEmployersRepository`), and why is it better practice to register and inject the **interface** instead of the concrete **class**?
3.  What are the three main **service lifetimes** in ASP.NET Core DI (covered in the guide), and which one did you choose for your in-memory repository?

---

## 🧠 Stretch & Deep Dive Questions (Test of Underlying Mechanics & Synthesis)

*These questions require synthesis, exploring trade-offs, and connecting the concept to broader ideas.*

1.  **Causality & Trade-Offs (Service Lifetime):** You chose a specific service lifetime for your `InMemoryEmployersRepository`. If you had chosen a different lifetime (e.g., **Transient** or **Scoped**), what would happen to your stored data every time a new request came in? Explain the mechanism that causes this failure/success.
2.  **Architecture:** Right now, your Minimal API endpoints accept and return the full `Employer` **Domain Model** class. This violates the principle of **Data Transfer Objects (DTOs)**. Explain two specific risks you introduce by exposing your **Domain Model** directly to the client.
3.  **Critical Thinking (Error Handling):** When handling the `GET /employers/{id}` endpoint, what HTTP status code did you return if an employer was **not found**? Why is returning that specific status code better than, say, returning a **200 OK** with an empty body?

---

## 📝 Sprint Review

* **Status:** [Fully Done / Partially Done / Rework Needed]
* **Most Difficult Insight:** [What was the single hardest concept to grasp?]
* **Next Action:** [e.g., Start Sprint Template for next concept, Deep dive into Stretch Question 3].
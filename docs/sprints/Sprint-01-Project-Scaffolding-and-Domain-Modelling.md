# 🎯 Learning Sprint: Project Scaffolding & Domain Modeling

## 📅 Sprint Details & Goals

**Concepts/Topics:** .NET Project Scaffolding, C# Fundamentals (Syntax, Namespaces), Object-Oriented Programming (Classes, Properties).

**Primary Goal (Must-Have):** By the end, I must be able to create a new, runnable ASP.NET Core Empty Web project and define the C# classes that represent my core `JobVacancy` and `Employer` models.

**Secondary Goals:**
*   Understand the purpose and difference between a Solution (`.sln`) and a Project (`.csproj`).
*   Articulate the C# properties (data types, names) needed for each model.
*   Run the empty project from the .NET CLI (`dotnet run`) to prove it works.

---

## ✅ Task List

- [X] **Task 1: Initialize the Solution & Project**
    > *Description: You need to create the overall folder structure and the .NET project.*
    - [X] Sub-task 1.2: Create a new ASP.NET Core Empty Web project (as described in the guide) named `JobApi.Api`.
    - [X] Sub-task 1.3: Add the new project to your solution file.
    - [X] Sub-task 1.4: Run the project (`dotnet run`) to confirm you see the "Hello World" or equivalent default.

- [X] **Task 2: Define the Domain Models**
    > *Description: Create the C# classes that will represent your application's data.*
    - [X] Sub-task 2.1: Create a new folder in your project named `Models` or `Domain`.
    - [X] Sub-task 2.2: Inside that folder, create a new C# class file named `Employer.cs`. Define the class and give it the properties you think an employer should have (e.g., an ID, a Name, etc.).
    - [X] Sub-task 2.3: Create a new C# class file named `JobVacancy.cs`. Define the class and give it the properties a job vacancy should have (e.g., an ID, a Title, a Description, etc.).

- [X] **Task 3: Establish the Model Relationship**
    > *Description: Your models are related. You need to represent that relationship in the C# classes.*
    - [X] Sub-task 3.1: Modify your `JobVacancy` and/or `Employer` class(es) to link them together. You will have to decide how to do this.

---

## Feedback from Marking

**First time**
[ ] - Feedback point 1
[ ] - Feedback point 2

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the difference between a **class** and an **object** in C#?
2.  What is the purpose of the `.csproj` file? What key information from the guide does it contain (e.g., `TargetFramework`)?
3.  In C#, what is the difference between an `int` and a `string`? What about an `int` and a `Guid`? Which one do you think is better for a unique ID, and why?
4.  What does the `public` keyword mean on a class or a property?

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** You were instructed to start with the **ASP.NET Core Empty Web** template. The guide mentioned you could have used the **Web API** template, which gives you controllers and a `WeatherForecast` example. Why are we starting empty? What specific knowledge would you fail to build if you just used the pre-built Web API template?

2.  **Connecting Concepts (Task 3.1):** How did you decide to link your `JobVacancy` and `Employer` classes?
    *   **Option A:** Did you put an `EmployerId` property on the `JobVacancy` class?
    *   **Option B:** Did you put a full `Employer` object on the `JobVacancy` class?
    *   **Option C:** Did you put a `List<JobVacancy>` on the `Employer` class?
    
    > **Critique:** Justify your choice. What are the potential problems and benefits of your chosen approach versus the others, before we even add a database?

3.  **Critical Thinking:** Look at your new C# classes. They are likely "POCOs" (Plain Old C# Objects). Right now, someone could create a `JobVacancy` object and set its `Title` to `null` or an empty string. Is this a problem? Whose job is it to prevent this? Is it the job of the model itself (the class) or the job of the code that *uses* the class?

---

## 📝 Sprint Review

**Status:** `[Fully Done / Partially Done / Rework Needed]`

**Most Difficult Insight:** `[What was the single hardest concept to grasp?]`

**Next Action:** `[e.g., Start Sprint Template for next concept, Deep dive into Stretch Question 3].`

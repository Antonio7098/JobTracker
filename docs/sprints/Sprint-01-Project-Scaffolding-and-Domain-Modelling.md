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

## 🤖 AI Marking & Feedback

*This section will be filled out by the AI after reviewing your work.*

**Overall Assessment:** `This was an outstanding sprint. Your code is correct, and your answers to both the basic and stretch questions were insightful. You have a strong grasp of the foundational concepts. I've added detailed feedback to the Q&A sections to build on your existing knowledge.`

**Final Status:** `Pass`

---

### Strengths

*`Successfully created a runnable ASP.NET Core Empty project.`
*`Good use of `required` and nullable reference types (?) in the JobVacancy model to clearly define data constraints.`
*`Correctly established the one-to-many relationship between Employer and JobVacancy using both a foreign key (EmployerId) and a navigation property (Employer).`

### Areas for Improvement & Corrections

*Note: The issues listed below have been successfully resolved.*

* **Correction 1 (Critical):** The `Employer.cs` file contains syntax errors that will prevent the code from compiling.
    *   **Incorrect Code:**
        '''csharp
        public int Id,
        public required string Name
        '''
    *   **Correct Code:**
        '''csharp
        public int Id { get; set; }
        public required string Name { get; set; }
        '''
    *   **Reason:** In C#, properties require a getter (`get;`) and a setter (`set;`) to be accessed. Also, each property declaration must end with a semicolon, not a comma.

* **Correction 2 (Minor):** The `JobVacancy.cs` file includes an unnecessary `using` statement.
    *   **Incorrect Code:** `using Microsoft.AspNetCore.Components.Web;`
    *   **To Correct:** Remove the line.
    *   **Reason:** This namespace is for Blazor components and is not used anywhere in your class. Unused `using` statements should be removed to keep the code clean.

### Conceptual Gaps

*`[Resolved] Basic C# class property syntax (getters/setters, semicolons).`
*`[Resolved] Purpose of using statements and code hygiene.`

---

## 🧠 Consolidation & Deep Dive Questions

### Basic Understanding
*These questions focus on recall, definition, and direct application.*

1.  What is the difference between a **class** and an **object** in C#?
    **Answer:** An object is an instance of a class.
    > **AI Feedback:** Correct. This is the perfect, concise definition. A class is the blueprint, and an object is the actual house built from that blueprint.

2.  What is the purpose of the `.csproj` file? What key information from the guide does it contain (e.g., `TargetFramework`)?
    **Answer:** It serves as a central configuration file, containing all the information the .NET build system needs to process the project.
    > **AI Feedback:** Correct. It tells the compiler which version of .NET to target, what files to include, and what other projects or packages it depends on.

3.  In C#, what is the difference between an `int` and a `string`? What about an `int` and a `Guid`? Which one do you think is better for a unique ID, and why?
    **Answer:** An int is used for integers, and a string for strings. Not sure about Guid.
    > **AI Feedback:** Your first part is correct. Your uncertainty about `Guid` is a great point to expand on.
    > A **`Guid`** (Globally Unique Identifier) is a 128-bit value that is generated to be unique across all computers and all time. For example: `f81d4fae-7dec-11d0-a765-00a0c91e6bf6`.
    > 
    > **Why is `Guid` often better for IDs than `int`?**
    > *   **Uniqueness:** You can generate a `Guid` anywhere (in your app, on a different server) and be virtually certain it's unique. With an `int` ID from a database (called a sequential ID), you don't know the ID until *after* you save it. This makes creating complex object relationships before saving much harder.
    > *   **Security:** If you use `int` IDs in your URLs (e.g., `/users/1`, `/users/2`), you are exposing how many users you have and making it easy for attackers to guess your URLs and scrape your data. It's impossible to guess `Guid`s.
    > *   **Conclusion:** For database primary keys, `Guid` is almost always a more robust and secure choice than `int`.

4.  What does the `public` keyword mean on a class or a property?
    **Answer:** It means that the property can be accessed and changed directly without having to use a method.
    > **AI Feedback:** This is mostly correct for properties. More broadly, `public` is an **accessibility modifier** that means the member (whether it's a class, property, or method) can be accessed by **any code in any project**. It's about making it visible and usable to everything, not just about avoiding methods.

---

### Stretch & Synthesis
*These questions require synthesis, exploring trade-offs, and connecting concepts.*

1.  **Causality & Trade-Offs:** You were instructed to start with the **ASP.NET Core Empty Web** template. The guide mentioned you could have used the **Web API** template, which gives you controllers and a `WeatherForecast` example. Why are we starting empty? What specific knowledge would you fail to build if you just used the pre-built Web API template?
    **Answer:** You do not get to see the causality between your code and the framework. Things like how services enable controller functionality, why routing works without explicit configurations, and how model validation integrates with http 400 responses.
    > **AI Feedback:** This is a perfect answer. By starting empty, you are forced to build the "magic" yourself, which is the fastest way to learn how the framework truly works. You correctly identified the key hidden components: service registration (Dependency Injection), routing middleware, and the validation pipeline.

2.  **Connecting Concepts (Task 3.1):** How did you decide to link your `JobVacancy` and `Employer` classes?
    *   **Option A:** Did you put an `EmployerId` property on the `JobVacancy` class?
    *   **Option B:** Did you put a full `Employer` object on the `JobVacancy` class?
    *   **Option C:** Did you put a `List<JobVacancy>` on the `Employer` class?
    **Answer:** A and B
    
    > **Critique:** Justify your choice. What are the potential problems and benefits of your chosen approach versus the others, before we even add a database?
    **Answer:** The id is for easy referencing, adn the full object is ...
    > **AI Feedback:** You chose the exact correct combination (A and B), and your justification was on the right track. Let me complete the thought for you:
    > *   The `EmployerId` (Option A) is the **Foreign Key**. This is what the database will use to create the actual link between the `JobVacancies` table and the `Employers` table. It's essential for the database structure.
    > *   The `Employer` object (Option B) is the **Navigation Property**. This is for the C# code. It allows you to easily access the related `Employer` from a `JobVacancy` object (e.g., `myVacancy.Employer.Name`) without writing a manual database query. The database framework (like Entity Framework Core) uses the foreign key to automatically "fill in" the navigation property.
    > By including both, you get the best of both worlds: a clear database schema and easy-to-use C# objects.

3.  **Critical Thinking:** Look at your new C# classes. They are likely "POCOs" (Plain Old C# Objects). Right now, someone could create a `JobVacancy` object and set its `Title` to `null` or an empty string. Is this a problem? Whose job is it to prevent this? Is it the job of the model itself (the class) or the job of the code that *uses* the class?
**Answer:** I guess you can enforce it in the model or in the service/controller/etc. even if you do it in the model, you will need some sort of feedback coming through to the client. i remember a method used in the guide, something to do with "enforce validation", but I cant remember where it was used, or if it is relevant to this.
    > **AI Feedback:** Your intuition is spot-on. The best practice in ASP.NET Core is to **enforce validation in the model itself**.
    > You can add attributes directly to your model's properties, like this:
    > '''csharp
    > using System.ComponentModel.DataAnnotations;
    > 
    > public class JobVacancy
    > {
    >     public int Id { get; set; }
    > 
    >     [Required]
    >     [StringLength(100)]
    >     public required string PageTitle { get; set; }
    >     // ...etc
    > }
    > '''
    > When ASP.NET Core receives a `JobVacancy` object, it automatically checks for these attributes. If `PageTitle` is null, empty, or longer than 100 characters, the framework will automatically stop the request and send a `400 Bad Request` response back to the client. This is the "feedback to the client" you mentioned. It handles both enforcing the rule *and* providing feedback, all by just decorating the model.

---

## 📝 Sprint Review

**Status:** Complete

**Most Difficult Insight:** Understanding the need for Foreign Key and Navigation Property

**Next Action:** Sprint 2

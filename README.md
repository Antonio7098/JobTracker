# Job Tracker API

A learning project to demonstrate professional practices in building a production-ready backend REST API using ASP.NET Core 8.

---

## About This Project

This project is a REST API for a simple job tracking application. It has been built from the ground up to serve as a practical demonstration of modern .NET backend development, focusing on clean architecture, testability, and professional coding standards.

## Features

- **RESTful API:** Full CRUD (Create, Read, Update, Delete) operations for `Employer` resources.
- **Layered Architecture:** Clear separation of concerns using the Repository Pattern to decouple business logic from data access.
- **Data Transfer Objects (DTOs):** Protects the internal domain model and provides a stable public API contract.
- **EF Core & MySQL Persistence:** Uses Entity Framework Core for data persistence with a MySQL database.
- **Interactive API Documentation:** Integrated Swagger/OpenAPI documentation for live, interactive API exploration and testing.
- **Containerized Database:** Uses Docker to run the MySQL database for a consistent and isolated development environment.

## Technologies Used

- **Framework:** .NET 8, ASP.NET Core
- **Language:** C#
- **Database:** MySQL
- **ORM:** Entity Framework Core 8
- **API Documentation:** Swashbuckle (Swagger/OpenAPI)
- **Containerization:** Docker

## Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- A Git client

### Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Antonio7098/JobTracker.git
    cd JobTracker/JobTracker.Api
    ```

2.  **Set up the database with Docker:**
    This project is configured to work with a MySQL database running in a Docker container. Run the following command in your terminal to start the database:
    ```bash
    docker run -d --name jobtracker-mysql -p 3306:3306 -e MYSQL_DATABASE=JobTrackerDb -e MYSQL_USER=user -e MYSQL_PASSWORD=password mysql:8.0
    ```
    The connection string in `appsettings.json` is already configured to connect to this container.

3.  **Apply Database Migrations:**
    EF Core will create the database schema from the C# models. Run the following command to apply the migrations:
    ```bash
    dotnet ef database update
    ```

4.  **Run the application:**
    ```bash
    dotnet run
    ```
    The API will be running and listening on `http://localhost:5035`.

### Exploring the API

Once the application is running, you can explore and interact with the API using the built-in Swagger UI. Navigate to the following URL in your browser:

**`http://localhost:5035/swagger`**

## API Endpoints

The following endpoints are available for the `Employer` resource.

| Verb   | Path              | Description                    |
| :----- | :---------------- | :----------------------------- |
| `GET`  | `/employers`      | Gets a list of all employers.  |
| `GET`  | `/employers/{id}` | Gets a single employer by ID.  |
| `POST` | `/employers`      | Creates a new employer.        |
| `PUT`  | `/employers/{id}` | Updates an existing employer.  |
| `DELETE`| `/employers/{id}` | Deletes an employer by ID.     |

## Project Structure

- **/Data:** Contains the EF Core `DbContext`.
- **/DTOs:** Data Transfer Objects used for the public API contract.
- **/Endpoints:** Minimal API endpoint definitions, organized by feature.
- **/Maps:** Extension methods for mapping between domain models and DTOs.
- **/Migrations:** EF Core database migration files.
- **/Models:** Core C# domain model classes (`Employer`, `JobVacancy`).
- **/Services:** Contains the business logic and data access layer (e.g., `IEmployersRepository`).

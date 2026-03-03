# TaskFlow API

TaskFlow is a backend Web API designed for comprehensive task and project management. This application was developed as a hands-on pet project with a strong focus on implementing Clean Architecture, adhering to RESTful API design principles, and ensuring robust database interactions.

## Architecture

The solution is structured using a Layered (Clean-ish) Architecture approach. It is divided into distinct class libraries to strictly enforce the separation of concerns:

* **WebAPITest.Api:** HTTP layer (Controllers, Middleware, Routing)
* **WebAPITest.Application:** Core business logic, Application Services, and DTOs
* **WebAPITest.Domain:** Core domain entities, Interfaces, and Enums
* **WebAPITest.Infrastructure:** Data access (EF Core), Database context, Repositories, and JWT configuration

## Tech Stack

* **Language:** C#
* **Framework:** ASP.NET Core Web API
* **ORM:** Entity Framework Core
* **Database:** MS SQL Server
* **Security:** JWT Authentication
* **Documentation:** Swagger (OpenAPI)
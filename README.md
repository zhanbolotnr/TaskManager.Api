\# TaskManager API



RESTful Web API for managing tasks, built with ASP.NET Core and Entity Framework Core.



Проект демонстрирует полный backend-цикл: CRUD, валидацию, работу с базой данных, миграции и документирование через Swagger.



---



\## 🚀 Technologies



\- .NET 8

\- ASP.NET Core Web API

\- Entity Framework Core

\- SQL Server (LocalDB)

\- Swagger / OpenAPI

\- Git \& GitHub



---



\## ✨ Features



\- Create, read, update and delete tasks (CRUD)

\- Data persistence with EF Core and SQL Server

\- Database migrations

\- DTO separation (Contracts)

\- Input validation

\- Clean project structure

\- Swagger UI for API testing



---



\## 📂 Project Structure





TaskManager.Api

│

├── Contracts # DTOs (request / response models)

├── Controllers # API controllers

├── Domain # Domain entities and enums

├── Persistence # DbContext and database configuration

├── Validation # Input validation logic

├── Migrations # EF Core migrations

│

├── Program.cs

├── appsettings.json

├── global.json

└── TaskManager.Api.csproj


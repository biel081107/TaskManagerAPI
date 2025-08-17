# 📌 REST API for Task Management

## 🇺🇸 English Version

### 📖 Description
A simple and extensible REST API for task management, built with ASP.NET Core and Entity Framework Core on SQLite. The goal of this project is to provide a practical foundation for creating, reading, updating, and deleting tasks (CRUD), focusing on best practices in architecture, persistence, and easy deployment (Docker). Ideal as a template for small applications, proof of concepts, or for learning web development in .NET.

Main features:
- Full task management (create, list, update, delete).
- Mark tasks as completed, pending, or in progress.
- Input validation and consistent error handling in the API.
- Local persistence with SQLite and support for migrations via EF Core.
- Structure ready for adding tests.
- Authentication and authorization system (JWT): each user has their own tasks and can only create, view, edit, or delete their own.

How this project helps:
- Serves as a practical example of a well-organized REST API in ASP.NET Core.
- Facilitates learning of common patterns (DTOs, services, repositories, and migrations).
- Demonstrates user access control and data separation between accounts.
- Can be extended for front-end integration, authentication by other providers, or storage in other databases.
- Simple setup to run locally and in a containerized environment.

### 🚀 Technologies Used
- ASP.NET Core
- Entity Framework Core
- SQLite
- Docker
- JWT
- RESTful API

### ⚙️ How to Run the Project

1. **Clone the repository**
   git clone https://github.com/your-username/repo-name.git

2. **Enter the project folder**
    cd repo-name

3. **Restore packages**
    dotnet restore

4. **Apply migrations and create the database**
    dotnet ef migrations add InitialCreate
    dotnet ef database update

5. **Run the project**
    dotnet run

6. **Access via Swagger (if you want)**
    /{url}/swagger

**📌 Main Endpoints**

**🔑 Authentication**
    POST /api/autenticacao/login → Logs in and returns a JWT token
    POST /api/autenticacao/register → Registers a new user

👤 Users

GET /api/usuarios/perfil → Returns a profile with user information

✅ Tasks

GET /api/tarefas → Lists all tasks of the logged-in user

POST /api/tarefas → Creates a new task for the user

PUT /api/tarefas/{id} → Updates a task

DELETE /api/tarefas/{id} → Removes a task


**🔒 JWT Authentication**

Some endpoints require authentication via Bearer Token.
Example usage in the header:

Authorization: Bearer {your_jwt_token}

**🛠️ Author**

Developed by Gabriel Olímpio
📧 Email: contatoolimpiodev@gmail.com
🌐 GitHub: https://github.com/biel081107

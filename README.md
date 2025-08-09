# To-Do List Project 

## Definition of the Project

This is the backend for the Todo List Application built with .NET CORE , MediatR  and PostgreSQL, providing API endpoints for managing users and todo items. It uses JWT authentication and CORS for integration with a Vue3 frontend.

---

## Technologies Used 
- **.NET:** backend framework
- **Entitiy Framework Core:** database operations
- **PostgreSQL:** Realtional database
- **MediatR:** CQRS pattern implementation
- **JWT(JSON Web Token):** authentication 
- **ASP .NET Core Identity:** password hashing and user security
- **CORS:** Cross-origin resource sharing configuration  

---

## Setup Instructions 

### Prerequisites
Firstly if you don't have VSCode download VSCode then follow these steps;
1. Download .NET Install Tools (https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
2. Download PostgreSQL (https://www.postgresql.org/download/) 
3. Download pgAdmin (optionally) for database management 

---
### Information 
Make sure in the terminal you are the right directory. if you are not write this in your terminal:
```
cd TodoApi
```

---

### Apply Database Migrations 
  ```
  dotnet ef database update 
  ```

---

### Run the API
 ```
 dotnet run 
 ```
API will start at (http://localhost:5000) 

---

## API Endpoints 

| GET    | `/todos`              | Get all tasks                      |
| POST   | `/todos`              | Create new task                    |
| PUT    | `/todos/:id`          | Update the task                    |
| DELETE | `/todos/:id`          | Delete task                        |
| GET    | `/todos/:id`          | Get the task by id                 |

---

### Auth Register and Login 
```
POST http://localhost:5000/api/auth/register
Content-Type: application/json

{
  "username": "...",
  "password": "..."
}
```
You have to write your name and password for the app. after you write them you will get;
```
{
  "message": "User registered successfully"
}
```
---

```
POST http://localhost:5000/api/auth/login
Content-Type: application/json

{
  "username": "your-username",
  "password": "your-password"
}
```
When you write your name and password correctly for the login you will get a token. Like this;
```
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
``` 


### Todos 
```
GET  http://localhost:5000/api/todo/GetAll
Authorization: Bearer <your_token> 
``` 
You will get all todos.

### Create new Todo
```
POST http://localhost:5000/api/todo/
Authorization: Bearer <your_token>
Content-Type: application/json
```
You will create new todo.

### Update Todo 
```
PUT http://localhost:5000/api/todo/{id}
Authorization: Bearer <your_token>
Content-Type: application/json
```
you will update your todo by the id. 

### Delete Todo 
```
DELETE http://localhost:5000/api/todo/{id}
Authorization: Bearer <your_token>
```
If you want to delete your todo you have write the todo id.

---

## Authentication Notes
All /api/todo routes require a valid JWT token obtained from /api/auth/login.

---














# 👨‍💼 Employee Management System – ASP.NET Core MVC

A clean, secure, and scalable **ASP.NET Core 6.0 MVC** web application for managing employees, departments, users, and roles.  
Built with **Entity Framework Core**, **ASP.NET Identity**, and follows a layered architecture with Razor Views for a fully responsive UI.

---

## 🚀 Features

- ✅ **MVC Architecture** (Models, Views, Controllers)  
- 🔐 **User Authentication & Authorization** (Register, Login, Logout) via ASP.NET Identity  
- 👥 **Role Management** (create roles, assign roles to users)  
- 📁 **Employee Management** (CRUD operations on employees)  
- 🏢 **Department Management** (CRUD operations on departments)  
- 🔍 **Search & Filtering** for employees by name  
- 🔄 **Forgot & Reset Password** flows (email-based)  
- ✨ **AutoMapper** for mapping Entities ↔ ViewModels  
- 🧱 **Layered Architecture** separation (PL, BLL, DAL)  
- 🎨 **Razor Views** + **Bootstrap** for responsive UI  
- 🧪 **Swagger UI** available under `/swagger` for API testing  
---

## 🛠️ Tech Stack

| Layer / Purpose | Technology                     |
| --------------- | ------------------------------ |
| Framework       | ASP.NET Core 6.0 MVC           |
| ORM             | Entity Framework Core          |
| Database        | SQL Server                     |
| Identity        | ASP.NET Core Identity          |
| Views / UI      | Razor Views + Bootstrap 5      |
| Mapping         | AutoMapper                     |
| Docs            | Swagger (Swashbuckle)          |

---

## 🧱 Project Structure (3-Layer Architecture)

The solution is separated into three main layers:

### 1️⃣ **Presentation Layer** – `Demo.PL`
- `Controllers/`  RESTful MVC controllers  
- `Views/`  Razor Views (Employee, Department, Account, Roles, Shared)  
- `wwwroot/`  Static assets (CSS, JS, images)  
- `Program.cs`  App start-up & DI configuration  
- `appsettings.json`  DB connection string, Identity options  

### 2️⃣ **Business Layer** – `Demo.BLL`
- `Interfaces/`  IGenericRepository, IUnitOfWork, IEmployeeService …  
- `Repositories/`  Unit-of-Work + concrete service/repository classes  
- Handles validation, business rules, and coordinates DAL calls  

### 3️⃣ **Data Access Layer** – `Demo.DAL`
- `Context/`  `AppDbContext` with DbSets & FluentAPI configs  
- `Entities/`  Employee, Department, ApplicationUser, ApplicationRole  
- `Migrations/`  EF Core migration history  

---

## 📦 Getting Started

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/kareemwaleed266/employee-management-system-mvc.git
cd employee-management-system-mvc

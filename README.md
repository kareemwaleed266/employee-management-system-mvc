 🏢 Employee Management System – ASP.NET Core MVC

A robust **ASP.NET Core 6.0 MVC** web application for managing employees, departments, users and roles.  
Built with **Entity Framework Core**, **ASP.NET Identity**, **Razor Views**, and follows a clean **3-layered architecture** for testability and maintainability.

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

| Layer          | Technology                    |
| -------------- | ------------------------------ |
| **Framework**  | ASP.NET Core 6.0 MVC          |
| **ORM**        | Entity Framework Core         |
| **Identity**   | ASP.NET Core Identity         |
| **Views**      | Razor Views + Bootstrap       |
| **Database**   | SQL Server                    |
| **Mapping**    | AutoMapper                    |

---

## 📁 Project Structure

Demo.PL/ → Presentation Layer (MVC)
├── Controllers/ → AccountController, EmployeeController, DepartmentController, RolesController, UserController, HomeController
├── Models/ → ViewModels (SignUp, SignIn, EmployeeViewModel, DepartmentViewModel, etc.)
├── Views/ → Razor Views (Account, Employee, Department, Roles, User, Shared)
├── wwwroot/ → Static assets (CSS, JS, images)
├── Program.cs → App entry point & DI setup
├── appsettings.json → Configuration (ConnectionStrings, Identity options, etc.)

Demo.BLL/ → Business Logic Layer
├── Interfaces/ → IUnitOfWork, IGenericRepository, IEmployeeRepository, IDepartmentRepository
├── Repositories/ → GenericRepository, EmployeeRepository, DepartmentRepository, UnitOfWork
└── Demo.BLL.csproj

Demo.DAL/ → Data Access Layer
├── Context/ → AppDbContext (DbSets & Fluent API configurations)
├── Entities/ → BaseEntity, Employee, Department, ApplicationUser, ApplicationRole
├── Migrations/ → EF Core migration files
└── Demo.DAL.csproj

yaml
Copy
Edit

---

## 📦 Getting Started

### 1️⃣ Clone the repository

```bash
git clone https://github.com/kareemwaleed266/employee-management-system-mvc.git

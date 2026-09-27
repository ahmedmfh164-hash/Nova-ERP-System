# Nova ERP System

<p align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge\&logo=csharp\&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge\&logo=microsoftsqlserver\&logoColor=white)

![JWT](https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge\&logo=jsonwebtokens\&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge\&logo=swagger\&logoColor=black)
![ADO.NET](https://img.shields.io/badge/ADO.NET-Data%20Access-0078D4?style=for-the-badge)
![GitHub](https://img.shields.io/badge/GitHub-Repository-181717?style=for-the-badge\&logo=github\&logoColor=white)

![Architecture](https://img.shields.io/badge/Architecture-Layered%20Architecture-3ECF8E?style=for-the-badge)
![Database](https://img.shields.io/badge/Database-Stored%20Procedures-CC2927?style=for-the-badge)
![Security](https://img.shields.io/badge/Security-JWT%20%7C%20RBAC-6F42C1?style=for-the-badge)

</p>

<p align="center">
  <strong>Enterprise Resource Planning & Management System</strong>
</p>

> A secure, modular Enterprise Resource Planning (ERP) backend built with ASP.NET Core Web API and SQL Server.

Nova ERP System is a portfolio-grade ERP backend designed to manage core business operations such as users, products, customers, purchasing, sales, invoices, returns, and inventory movements.

The project focuses on building a maintainable backend with clear separation of responsibilities, secure API access, reusable data-access components, and database operations implemented through SQL Server Stored Procedures.

---

## 🚀 Project Overview

Nova ERP is an API-first ERP system. The backend is designed to serve a future web or desktop frontend while keeping business rules, security, and data access separated from the presentation layer.

The system covers major ERP workflows including:

* User and role management
* Permission-based authorization
* Product management
* Customer management
* Purchase invoices
* Sales invoices
* Sales returns
* Inventory and stock movements
* Audit logging
* Pagination and filtering
* Secure authentication and authorization

---

## 🏗️ Architecture

The project follows a layered architecture that separates HTTP handling, business logic, domain models, contracts, and database access.

```text
                    Client
                      │
                      ▼
                ┌─────────────┐
                │   ERP.API   │
                │ Controllers │
                └──────┬──────┘
                       │
                       ▼
              ┌─────────────────┐
              │ ERP.Application │
              │    Services     │
              └────────┬────────┘
                       │
                       ▼
              ┌─────────────────┐
              │   Interfaces    │
              │   Repositories  │
              └────────┬────────┘
                       │
                       ▼
           ┌─────────────────────────┐
           │ ERP.Infrastructure      │
           │ Repositories / DB       │
           └────────────┬────────────┘
                        │
                        ▼
                 ┌──────────────┐
                 │ SQL Server   │
                 │ Stored Procs │
                 └──────────────┘
```

### Main Projects / Layers

| Layer                | Responsibility                                                    |
| -------------------- | ----------------------------------------------------------------- |
| `ERP.API`            | Controllers, HTTP endpoints, authentication and API configuration |
| `ERP.Application`    | Business logic, services and application interfaces               |
| `ERP.Domain`         | Core entities and domain-related models                           |
| `ERP.Contracts`      | Request/Response DTOs and mappings                                |
| `ERP.Infrastructure` | Repositories, database access and infrastructure services         |
| `SQL`                | Tables and Stored Procedures used by the application              |

---

## 🧩 Main Modules

### 👤 Users & Access Control

* User management
* Roles
* Permissions
* Role-Permission relationships
* Permission-based endpoint protection
* Ownership-aware authorization

Example permission model:

```text
Users.View
Users.Create
Users.Update
Users.Delete
Products.View
Products.Update
Sales.Create
Reports.View
```

---

### 📦 Products & Inventory

The inventory side of the system keeps track of products and stock changes through explicit stock movements.

Each stock movement contains information such as:

* Product
* Movement Type
* Quantity
* Movement Date
* Reference ID

This allows inventory changes to be associated with business operations such as purchasing, sales, and returns.

---

### 🧾 Purchase Invoices

Purchase invoices are used to record products purchased from suppliers and their corresponding invoice items.

The module supports operations such as:

* Create purchase invoices
* Add invoice items
* Update invoice items
* Delete invoice items
* Track invoice status
* Calculate invoice totals
* Update inventory based on purchasing operations

---

### 🛒 Sales Invoices

Sales invoices manage customer sales and their invoice items.

The module includes:

* Sales invoice creation
* Invoice item management
* Product quantity handling
* Price tracking
* Invoice status management
* Inventory updates
* Transaction-related database operations

---

### ↩️ Sales Returns

The Sales Returns module handles returned products and connects return operations with inventory.

It includes:

* Sales return creation
* Return item management
* Product quantities
* Return references
* Inventory adjustments

---

### 📊 Pagination & Filtering

Large collections are designed to support pagination instead of returning every record in a single response.

A typical request can provide:

```text
Page
PageSize
Search
```

This helps reduce unnecessary database and network load when working with large datasets.

---

## 🔐 Security

Security is an important part of Nova ERP rather than an additional feature added at the end.

The API includes concepts such as:

* HTTPS
* JWT Bearer Authentication
* Role-Based Access Control (RBAC)
* Permission-Based Authorization
* Ownership checks
* CORS configuration
* Rate Limiting
* Auditing
* Monitoring and alerts
* Secure configuration management
* Externalized secrets

Endpoints can be protected using permission attributes such as:

```csharp
[Authorize]
[HasPermission(PermissionModules.Users, PermissionAction.Read)]
```

This allows authorization rules to be expressed at the API endpoint level while keeping permission definitions centralized.

---

## 🗄️ Database & Data Access

Nova ERP uses **Microsoft SQL Server** as its database.

Database operations are primarily implemented using **Stored Procedures** and a reusable data-access infrastructure.

The application follows a flow similar to:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Stored Procedure Executor
    ↓
SQL Server
```

This approach keeps SQL operations centralized and allows repositories to focus on mapping database results into domain models.

### Example Database Operations

* Create / Update / Delete records
* Get by ID
* Get all records
* Search and filtering
* Pagination
* Inventory updates
* Stock movement creation
* Invoice item operations
* Transactional business operations

---

## 🛡️ SQL Injection Protection

The project uses parameterized SQL commands and Stored Procedures instead of concatenating user input directly into SQL statements.

Example concept:

```text
User Input
    ↓
Parameterized Command
    ↓
Stored Procedure
    ↓
SQL Server
```

This prevents user input from being interpreted as executable SQL in the normal application data-access flow.

---

## 🧱 DTOs & Mapping

The API separates external request/response contracts from domain entities.

The project uses dedicated DTOs for operations such as:

* Create requests
* Update requests
* Response models

Mappings are kept separately so that API contracts do not have to be tightly coupled to the domain entities.

---

## 📝 Audit Logging

The system includes an audit logging concept for recording important application activities.

Audit information can be used to track:

* Who performed an operation
* What operation was performed
* When it happened
* Relevant entity/reference information
* Additional request or operation details

This provides traceability for important ERP operations.

---

## 📚 API Documentation

The API can be explored and tested through **Swagger / OpenAPI**.

Swagger provides an interactive interface for:

* Viewing available endpoints
* Sending API requests
* Testing authentication
* Testing authorization and permissions
* Inspecting request and response models

---

## ⚙️ Technologies

* **C#**
* **ASP.NET Core Web API**
* **Microsoft SQL Server**
* **ADO.NET**
* **SQL Server Stored Procedures**
* **JWT Authentication**
* **Role-Based Access Control (RBAC)**
* **Permission-Based Authorization**
* **Swagger / OpenAPI**
* **RESTful API Design**

---

## 📁 Project Structure

A simplified view of the project structure:

```text
Nova-ERP-System/
│
├── ERP.API/
│   └── Controllers/
│
├── ERP.Application/
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   └── Services/
│   └── Services/
│
├── ERP.Domain/
│   └── Entities/
│
├── ERP.Contracts/
│   ├── Requests/
│   ├── Responses/
│   └── Mappings/
│
├── ERP.Infrastructure/
│   ├── Repositories/
│   ├── Database/
│   └── Services/
│
└── SQL/
    ├── Tables/
    └── Stored Procedures/
```

---

## 🔄 Example Business Flow

A simplified sales flow can be represented as:

```text
Create Sales Invoice
        ↓
Add Invoice Items
        ↓
Validate Product / Quantity
        ↓
Update Stock
        ↓
Create Stock Movement
        ↓
Update Invoice Total
        ↓
Save Operation
```

The same principle is applied to purchasing and sales-return operations where inventory changes need to remain consistent with the related business operation.

---

## 🛠️ Getting Started

### Prerequisites

Make sure you have:

* .NET SDK compatible with the project
* SQL Server / SQL Server Express
* Visual Studio or another compatible .NET IDE
* SQL Server Management Studio (recommended)

### 1. Clone the Repository

```bash
git clone https://github.com/ahmedmfh164-hash/Nova-ERP-System.git
cd Nova-ERP-System
```

### 2. Create the Database

Create the `ERP` database in SQL Server and execute the SQL scripts included with the project.

The scripts contain the required tables and Stored Procedures for the ERP modules.

### 3. Configure the Connection String

Configure the SQL Server connection string using your local environment.

Example:

```text
Server=.\SQLEXPRESS;Database=ERP;Trusted_Connection=True;TrustServerCertificate=true
```

> Do not commit production credentials, passwords, JWT secrets, or other sensitive configuration values to source control.

### 4. Run the API

Open the solution in Visual Studio and run the `ERP.API` project.

Then open Swagger to explore and test the API.

---

## 🎯 Project Goals

Nova ERP was built to practice and demonstrate real-world backend development concepts rather than only implementing basic CRUD endpoints.

The main goals were:

* Build a complete API-first ERP backend
* Apply layered architecture
* Separate business logic from data access
* Build reusable repository and database-access components
* Use Stored Procedures for database operations
* Implement secure authentication and authorization
* Implement role and permission management
* Handle inventory changes consistently
* Model real business workflows such as invoices and returns
* Build an API that can later serve a web or desktop frontend

---

## 🔮 Future Improvements

Potential future extensions include:

* Web frontend
* Advanced reporting and dashboards
* Notifications
* More ERP modules
* Automated testing
* Caching
* Background processing
* Deployment and CI/CD pipelines

---

## 👨‍💻 Author

**Ahmed Mohamed Faheem**

.NET Developer focused on C#, ASP.NET Core, Web APIs, SQL Server, backend architecture, and secure application development.

* GitHub: https://github.com/ahmedmfh164-hash
* LinkedIn: https://linkedin.com/in/ahmed-mohamed-faheem-7b1642395
* Project: https://github.com/ahmedmfh164-hash/Nova-ERP-System

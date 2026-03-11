# 🧾 InvoiceHub API – Invoice Management System

A backend REST API built with **ASP.NET Core (.NET 8)** implementing the **CQRS pattern** for managing invoices, customers, suppliers, and items.

This project demonstrates a clean backend architecture using **CQRS, Repository Pattern, Entity Framework Core, and MediatR**.

---

## 🚀 Tech Stack

* ASP.NET Core (.NET 8)
* C#
* Entity Framework Core
* SQL Server
* MediatR
* CQRS Pattern
* Swagger (OpenAPI)

---

## 📌 Features

* Create and manage invoices
* Manage customers, suppliers, and items
* Command and Query separation using CQRS
* Generic repository pattern
* Clean layered architecture
* RESTful API endpoints
* Interactive API documentation with Swagger

---

## 📷 API Documentation (Swagger)

### 📊 Swagger API Overview

![Swagger Overview](screenshots/invoicehub-swagger-overview.png)

### 📦 Item & Supplier APIs

![Item and Supplier APIs](screenshots/invoicehub-item-supplier-apis.png)

---

## 🏗 Project Architecture

The project follows a layered architecture with CQRS separation:

```
Controllers        → API Endpoints
CQRS               → Commands & Queries
Entities           → Domain Models
Repositories       → Data Access Layer
Data               → DbContext & Database Configuration
Common             → Shared utilities (Pagination etc.)
DTOs               → Data Transfer Objects
Mapping            → Entity ↔ DTO Mapping
```

---

## ▶ How to Run the Project

1️⃣ Clone the repository

```
git clone https://github.com/your-username/Invoice-Management-System-API.git
```

2️⃣ Navigate to the project folder

```
cd Invoice-Management-System-API
```

3️⃣ Update the connection string in:

```
appsettings.json
```

4️⃣ Run the database migrations

```
dotnet ef database update
```

5️⃣ Run the API

```
dotnet run
```

6️⃣ Open Swagger UI

```
http://localhost:5183/swagger/index.html
```

---

## 👨‍💻 Author

**Pratik Zodpe**

If you like the project, consider ⭐ starring the repository.

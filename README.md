# ShoppingWeb Application

## Overview
The **ShoppingWeb Application** is a web-based platform developed using ASP.NET Core MVC, Web API, and Entity Framework Core. This application allows users to browse products, place orders, and manage product categories through a user-friendly interface. It also incorporates JWT-based authentication and authorization to secure user interactions.

## Features

- **Product Management**
  - Add, update, delete, and view products.
  - Categorize products under different categories.
  - View products by category.

- **Order Management**
  - Place orders for multiple products.
  - View order details along with ordered items.
  - Manage orders efficiently.

- **Category Management**
  - Add, update, and delete product categories.
  - Organize products under specific categories.

- **JWT Authentication & Authorization**
  - Secure the API endpoints using JWT.
  - Protect resources and ensure that only authenticated users can access certain features.

- **Error Logging**
  - Implement logging using Serilog.
  - Log errors to a file and the console for troubleshooting and maintaining the system.

- **API Versioning**
  - Supports multiple API versions.
  - Enables smooth evolution of the API without breaking existing clients.

- **Unit Testing**
  - Comprehensive unit tests using xUnit and Moq.
  - Ensure the reliability and correctness of the application's features.

## Technologies Used
- **Programming Language**: C#
- **Framework**: .NET Core, ASP.NET MVC Core / Web API
- **Database**: SQL Database, Entity Framework Core
- **Unit Testing**: xUnit, Moq
- **Version Control**: Git, GitHub
- **Object Mapping**: AutoMapper

## Prerequisites

- **.NET 8 SDK**: Ensure you have the .NET 8 SDK installed.
- **SQL Server**: Required for database setup.
- **Visual Studio 2022**: Recommended for development and testing.
- **Git**: Version control system to manage codebase.

## Installation

### 1. Clone the Repository
git clone https://github.com/yasasgunarathna/CSC8470Assignment3.git
cd CSC8470Assignment3
git checkout final
dotnet restore

### 2. Set Up the Database
Restore the SQL Database Backup
Locate the ShoppingDb.bak file in your local directory.
Use SQL Server Management Studio (SSMS) to restore the database from the backup.
Ensure the connection string in appsettings.json matches your SQL Server setup.

### 3. Run the Application
dotnet run --project ShoppingWeb.API


## Usage
1. **Login:**
   - Use the `/login` endpoint to authenticate.
   - Example credentials: `admin` / `admin1234`

2. **Products API:**
   - `/api/Products` (GET, POST, PUT, DELETE)

3. **Categories API:**
   - `/api/Categories` (GET, POST, PUT, DELETE)

4. **Orders API:**
   - `/api/Orders` (GET, POST)

## Version Control

- **Branching:** Separate branches were used for different features and bug fixes.
- **Merging:** Feature branches were merged into the final branch after code review and corrections.
- **Commits:** Frequent commits were made to keep track of changes, each with a clear message.

## Unit Testing
Run the unit tests:
```bash
dotnet test

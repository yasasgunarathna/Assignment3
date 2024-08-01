# Online Store Application

## Overview
The Online Store Application is a robust e-commerce platform designed to provide users with a seamless shopping experience. This application allows users to browse products, place orders, manage their profiles, and track order history. Administrators can manage products, categories, brands, stock levels, and user roles through an intuitive admin interface.

## Features
- **Product Management**: Add, view, update, and delete products with detailed information.
- **User Management**: Register, login, and manage user profiles.
- **Order Management**: Place orders, view order history, and update order statuses.
- **Stock Management**: Manage stock levels and receive low stock alerts.
- **Role-Based Access Control**: Create and assign roles to manage access levels.

## Technologies Used
- **Programming Language**: C#
- **Framework**: .NET Core, ASP.NET MVC Core / Web API
- **Database**: SQL Database, Entity Framework Core
- **Unit Testing**: xUnit, Moq
- **Version Control**: Git, GitHub
- **Object Mapping**: AutoMapper

## Getting Started

### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)

### Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/username/repository.git
    ```
2. Navigate to the project directory:
    ```bash
    cd repository
    ```
3. Restore the dependencies:
    ```bash
    dotnet restore
    ```
4. Update the database connection string in `appsettings.json`.

5. Apply the database migrations:
    ```bash
    dotnet ef database update
    ```

### Running the Application
Start the application by running:
```bash
dotnet run


# GBC Ticketing System (ASP.NET Core + PostgreSQL)

A web-based event ticketing platform built with ASP.NET Core MVC and deployed to Azure App Service. The application supports role-based authentication (Admin and User) and integrates with Azure Database for PostgreSQL.

## Live Demo
Azure Deployment:
https://gbc-ticketing-project-bfb3fgh0dtgkhzhy.canadacentral-01.azurewebsites.net/

---

## Features

### Guest Users
- View homepage
- Browse events
- View event details

### Registered Users
- Register and login (ASP.NET Core Identity)
- Purchase tickets
- View personal bookings (if implemented)

### Admin Role
- Manage events (Create / Edit / Delete)
- Control ticket availability
- Manage event categories
- Admin gmail: Admin0@gmail.com
- Admin password: P@ssw0rd

---

## Technology Stack

- ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core
- Npgsql (PostgreSQL EF Provider)
- Azure App Service
- Azure Database for PostgreSQL



Production database is hosted on:

Azure Database for PostgreSQL

Connection string is configured through:
- Azure App Service → Configuration (Environment Variables)



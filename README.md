# Flight Management API

This repository contains the Flight Management API, an ASP.NET Core application designed to manage flight data. The API supports CRUD operations for managing flight details and user authentication.

## Features

- **Flight Management**: Add, read, update, and delete flight records.
- **User Authentication**: Secure API endpoints using JWT-based authentication.
- **Data Validation**: Input validation to ensure accurate and reliable data entry.
- **ORM with Entity Framework**: Utilize Entity Framework for data handling.

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

Before you begin, ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (version specific to the project requirements)
- An appropriate IDE, such as [Visual Studio](https://visualstudio.microsoft.com/vs/) 
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Clone the project repository to your local machine using Git
git clone https://github.com/yourgithub/flight-management-api.git

### Navigate into the project directory
cd flight-management-api

### Restore all NuGet packages required by the project
dotnet restore

### Apply the EF Core migrations to your database
dotnet ef database update

### Build the project
dotnet build

### Run the project
dotnet run

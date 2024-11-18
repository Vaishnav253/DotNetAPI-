# DotNetAPI-

This project is a RESTful API built with .NET 8 Core and C#, designed to interact with a Microsoft Azure SQL Database for data storage and retrieval also posting new data as well as deleting data as well. It leverages Swagger for API documentation and testing, providing a seamless development and integration experience.

Features:

    CRUD Operations: Perform Create, Read, Update, and Delete operations on the database.
    Azure SQL Database Integration: Uses Azure SQL for scalable and secure data management.
    Swagger UI: Interactive API documentation for testing and exploration.
    Entity Framework Core: Simplified database interactions through an ORM.
    Error Handling: Structured and user-friendly error responses.
    Scalability: Designed to scale with your application's needs in cloud environments.

Technologies Used:

    Backend: .NET Core, C#
    Database: Azure SQL Database
    Documentation: Swagger (OpenAPI)
    ORM: Entity Framework Core

Getting Started, open powershell in windows or in macos, go to teminal and run 'pwsh' to open powershell:  

Prerequisites:

    .NET 8 Core SDK
    Azure SQL Database setup with connection string
    Visual Studio or VS Code (optional)
    Postman or a browser for testing via Swagger UI

Installation:

    Clone this repository:

git clone https://github.com/yourusername/your-repository.git
cd your-repository

Configure the connection string in appsettings.json:

"ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Database=your file name; Trusted_Connection=false; TrustServerCertificate=true; User Id=username of your Azure data studio; Password=password of your azure data studio;"
}

Run database migrations:

dotnet ef database update

Start the application:

dotnet watch run

The packages that are required to run the application is found in the itemgroup of the DotnetAPI.csproj file.

To add the package for example:

dotnet add package automapper                                                   


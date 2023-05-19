# BlogApi

This student project is a RESTful API service developed with Dotnet, C#, SQL Server database, and Entity Framework. The main purpose of this project is to provide an API for a blog where users can perform CRUD (Create, Read, Update, Delete) operations on blog posts.

## Table of Contents

- [Introduction](#introduction)
- [Table of Contents](#table-of-contents)
- [Technologies Used](#technologies-used)
- [Project Installation](#project-installation)
- [Code Editing](#code-editing)
  - [Models](#models)
  - [Controllers](#controllers)
  - [Migrations](#migrations)
  - [Data Transfer Objects (DTOs)](#data-transfer-objects-dtos)
  - [AutoMapper](#automapper)
- [Test the project](#test-the-project)
- [Debugging](#debugging)
- [License](#license)

## Introduction

The BlogApi project is a RESTful API service application developed with Dotnet and C#. It provides an interface for managing a blog, allowing users to perform CRUD operations on blog posts. It utilizes a SQL Server database with Entity Framework to store and retrieve data.

## Technologies Used

- Dotnet
- C#
- SQL Server
- Entity Framework
- AutoMapper
- Visual Studio Code

## Project Installation

1. Clone the project:

   ```shell
   git clone https://github.com/jmcandia/BlogApi.git
   ```

2. Install the Dotnet SDK:

   Follow the instructions at [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) to download and install the appropriate Dotnet SDK for your operating system.

3. Install SQL Server Express:

   Download and install SQL Server Express from [www.microsoft.com/sql-server/sql-server-downloads](https://www.microsoft.com/sql-server/sql-server-downloads).

4. Configure the connection string:

   In the `appsettings.json` file, configure the connection string named 'BlogDatabase' to connect to the locally running SQL Server Express instance with Windows authentication:

   ```json
   {
     "ConnectionStrings": {
       "BlogDatabase": "Server=localhost\\SQLEXPRESS;Database=BlogDb;Trusted_Connection=True;"
     }
   }
   ```

   Make sure to adjust the server name (in this case, `localhost\\SQLEXPRESS`) and the database name (in this case, `BlogDb`) according to your configuration.

## Code Editing

### Models

To create a new model in the project, follow these steps:

1. Open Visual Studio Code in the project's root folder.

2. Create a new class inside the `Models` folder. For example, create a class called `Post`:

   ```csharp
   namespace BlogApi.Models
   {
       public class Post
       {
           public int Id { get; set; }
           public string Title { get; set; }
           public string Content { get; set; }
       }
   }
   ```

   Make sure to define the necessary properties for your model.

### Controllers

To create a new controller in the project, follow these steps:

1. Open Visual Studio Code in the project's root folder.

2. Create a new class inside the `Controllers` folder. For example, create a class called `PostsController`:

   ```csharp
   using Microsoft.AspNetCore.Mvc;
   using BlogApi.Models;

   namespace BlogApi.Controllers
   {
       [Route("api/[controller]")]
       [ApiController]
       public class PostsController : ControllerBase
       {
           // Implement CRUD methods here
       }
   }
   ```

   Make sure to add the 'Controller' suffix to the class name.

#### Scaffolding

Scaffolding is a technique that allows you to automatically generate initial code for controllers and views based on existing models. To use scaffolding in the project, you need to install the "dotnet-aspnet-codegenerator" extension. Follow these steps:

1. Open a terminal in the project's root folder.

2. Run the following command to install the extension:

   ```shell
   dotnet tool install -g dotnet-aspnet-codegenerator
   ```

3. Ensure that the project is built by running the following command:

   ```shell
   dotnet build
   ```

4. To generate a controller and views for an existing model, run the following command:

   ```shell
   dotnet-aspnet-codegenerator controller -name ControllerName -async -api -m ModelName -dc DataContextName -outDir Controllers
   ```

   Replace `ControllerName`, `ModelName`, and `DataContextName` with the appropriate names for your project.

### Migrations

Migrations in Entity Framework allow you to manage changes to the database schema in a controlled manner. To use migrations in the project, follow these steps:

1. Open a terminal in the project's root folder.

2. Run the following command to install the Entity Framework Core tools:

   ```shell
   dotnet tool install --global dotnet-ef
   ```

3. Run the following command to create a new migration:

   ```shell
   dotnet ef migrations add NewMigrationName
   ```

   Replace `NewMigrationName` with the appropriate name for your project.

4. Run the following command to apply the migrations to the database:

   ```shell
   dotnet ef database update
   ```

### Data Transfer Objects (DTOs)

Data Transfer Objects (DTOs) are objects that define how data will be sent over the network. To create a new DTO in the project, follow these steps:

1. Open Visual Studio Code in the project's root folder.

2. Create a new class inside the `Dtos` folder. For example, create a class called `PostReadDto`:

   ```csharp
   namespace BlogApi.Dtos
   {
       public class PostReadDto
       {
           public int Id { get; set; }
           public string Title { get; set; }
           public string Content { get; set; }
       }
   }
   ```

   Make sure to define the necessary properties for your DTO.

### AutoMapper

AutoMapper is a library that allows you to map objects from one type to another. To use AutoMapper in the project, follow these steps:

1. Open a terminal in the project's root folder.

2. Run the following command to install the AutoMapper NuGet package:

   ```shell
   dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
   ```

3. Open the `Program.cs` file in the project's root folder.

4. Add the following code to the `ConfigureServices` method:

   ```csharp
   services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
   ```

5. Create a new class inside the `Profiles` folder. For example, create a class called `BlogApiProfile`:

   ```csharp
   using AutoMapper;
   using BlogApi.Dtos;
   using BlogApi.Models;

   namespace BlogApi.Profiles;

   public class BlogApiProfile : Profile
   {
      public PostsProfile()
      {
         CreateMap<Post, PostReadDto>();
         CreateMap<PostCreateDto, Post>();
         CreateMap<PostUpdateDto, Post>();
         CreateMap<Post, PostUpdateDto>();
      }
   }
   ```

   Make sure to define the necessary mappings for your project.

6. Open the `PostsController.cs` file in the `Controllers` folder.

7. Add the following code to the `PostsController` class:

   ```csharp
   private readonly IMapper _mapper;

   public PostsController(IMapper mapper)
   {
      _mapper = mapper;
   }
   ```

8. Use the `_mapper` field to map objects from one type to another. For example:

   ```csharp
   var postReadDto = _mapper.Map<PostReadDto>(post);
   ```

## Test the project

To execute the project, follow these steps:

1. Open a terminal in the project's root folder.

2. Trust the HTTPS development certificate by running the following command:

   ```shell
   dotnet dev-certs https --trust
   ```

   The preceding command doesn't work on Linux. See your Linux distribution's documentation for trusting a certificate.

3. Run the following command to compile and run the application:

   ```shell
   dotnet run
   ```

4. The application will run and be available at the URL specified in the terminal output. Typically, it will be `http://localhost:5000` or `https://localhost:5001`.

## Debugging

To debug the project in Visual Studio Code, follow these steps:

1. Open Visual Studio Code in the project's root folder.

2. Set breakpoints in the code where you want to debug.

3. Open the Debug tab in Visual Studio Code.

4. Select the appropriate debug configuration from the dropdown menu.

5. Click the "Start Debugging" button or press F5 to start the debugging process.

6. The debugger will stop at the specified breakpoints, allowing you to inspect the program's state and track the execution flow.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

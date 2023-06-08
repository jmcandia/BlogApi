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
  - [Authentication](#authentication)
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

### Authentication

Authentication is the process of verifying the identity of a user or system before granting access to specific resources or functionalities. It ensures that only authorized individuals or systems can access protected information or perform certain actions.

In C#, authentication can be implemented using various techniques and frameworks. One popular method is to use JSON Web Tokens (JWT) for authentication.

JWT is a compact and self-contained token format that consists of three parts: header, payload, and signature. The header contains metadata about the token, such as the algorithm used for signing. The payload contains claims or statements about the user, such as their identity, roles, or permissions. The signature is used to verify the integrity of the token.

Here's a step-by-step explanation of how JWT authentication works in C#:

1. User Authentication: When a user attempts to authenticate, they typically provide their credentials, such as a username and password. The server verifies these credentials using various mechanisms, such as comparing against a database or using an external authentication provider like OAuth or Active Directory.

2. Token Generation: If the user's credentials are valid, the server generates a JWT. The JWT is created by constructing a JSON object containing the necessary information, such as the user's ID or roles. This object is then signed using a secret key known only to the server.

3. Token Issuance: The server sends the JWT back to the client as a response. The client, typically a web browser or a mobile app, receives the JWT and stores it securely, such as in a cookie or local storage.

4. Token Usage: For subsequent requests to access protected resources or perform actions, the client includes the JWT in the request. This is often done by including the JWT in the Authorization header as a bearer token.

5. Token Verification: On the server side, when a request with a JWT is received, the server verifies the token's integrity and authenticity. This is done by checking the signature using the same secret key used during token generation.

6. Access Control: After the token is verified, the server examines the claims in the token's payload to determine if the user has the necessary permissions to access the requested resource or perform the action. If the user has the required permissions, the server processes the request and responds accordingly. Otherwise, an appropriate error response is returned.

It's important to note that JWTs are self-contained, meaning the server doesn't need to store the token or perform database lookups during the verification process. This makes JWTs scalable and suitable for stateless authentication scenarios.

In C#, you can use various libraries and frameworks like Microsoft Identity, IdentityServer, or third-party libraries like System.IdentityModel.Tokens.Jwt to handle JWT authentication and related operations. These libraries provide convenient methods and utilities to generate, validate, and process JWTs within your C# application.

To implement authentication we must follow the steps below:

1. Open a terminal in the project's root folder. Then, run the following command to install the following NuGet packages:

   ```shell
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
   dotnet add package System.IdentityModel.Tokens.Jwt
   ```

2. Open the `BlogApiContext.cs` file in the `Models` folder. Then, add the following code:

   ```csharp
   using Microsoft.EntityFrameworkCore;
   using BlogApi.Models.Configurations;
   using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
   using Microsoft.AspNetCore.Identity;

   // ...

   public class BlogApiContext : IdentityUserContext<IdentityUser>
   {
      public BlogApiContext(DbContextOptions<BlogApiContext> options)
         : base(options) { }

      // ...

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder); // Add this line

         // ...
      }
   }
   ```

3. Create the migration by running the following command:

   ```shell
   dotnet ef migrations add AddIdentity
   dotnet ef database update
   ```

4. Open the `appsettings.json` file in the project's root folder. Then, add the following configuration:

   ```json
   "AppSettings": {
      "Secret": "<your-super-secret-key>"
   }
   ```

   Replace `<your-super-secret-key>` with a secret key of your choice. Remember to keep this key secure and never share it with anyone.

5. Create a new folder named `Services`, and then create a new file named `TokenService.cs` inside it. Then, add the following code:

   ```csharp
   using System.IdentityModel.Tokens.Jwt;
   using System.Security.Claims;
   using System.Text;
   using Microsoft.AspNetCore.Identity;
   using Microsoft.IdentityModel.Tokens;

   namespace BlogApi.Services;

   public class TokenService
   {
      private readonly IConfiguration _configuration;
      private const int ExpirationMinutes = 30;

      public TokenService(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      public string CreateToken(IdentityUser user)
      {
         byte[] key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings")["Secret"]!);
         var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

         var claims = new List<Claim>
         {
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(ClaimTypes.Email, user.Email!)
         };

         var tokenDescriptor = new SecurityTokenDescriptor
         {
               Subject = new ClaimsIdentity(claims),
               Expires = expiration,
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
         SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }
   }
   ```

6. Open the `Program.cs` file in the project's root folder. Then, add the following code:

   ```csharp
   using BlogApi.Models;
   using Microsoft.EntityFrameworkCore;
   using AutoMapper;
   using AutoMapper.EquivalencyExpression;
   using Microsoft.AspNetCore.Authentication.JwtBearer;
   using Microsoft.IdentityModel.Tokens;
   using System.Text;
   using Microsoft.AspNetCore.Identity;
   using BlogApi.Services;

   // ...

   // Set up authentication
   byte[] key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings")["Secret"]!);
   builder.Services
     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.Events = new JwtBearerEvents
         {
            OnTokenValidated = async (context) =>
            {
               var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
               var id = context.Principal!.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
               var user = await userManager.FindByIdAsync(id);
               if (user == null)
               {
                  context.Fail("Unauthorized");
               }
            }
         };
         options.TokenValidationParameters = new TokenValidationParameters()
         {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
         };
     });

   // Set up identity
   builder.Services
      .AddIdentityCore<IdentityUser>(options =>
      {
         options.SignIn.RequireConfirmedAccount = false;
         options.User.RequireUniqueEmail = true;
         options.Password.RequireDigit = true;
         options.Password.RequiredLength = 8;
         options.Password.RequireNonAlphanumeric = true;
         options.Password.RequireUppercase = true;
         options.Password.RequireLowercase = true;
      })
      .AddEntityFrameworkStores<BlogApiContext>();

   // Set up TokenService
   builder.Services.AddScoped<TokenService>();

   // ...

   app.UseAuthentication(); // Add this line
   app.UseAuthorization();

   // ...
   ```

7. Create the `AccountController.cs` file in the `Controllers` folder. Then, add the following code:

   ```csharp
   using BlogApi.Dtos;
   using BlogApi.Models;
   using BlogApi.Services;
   using Microsoft.AspNetCore.Identity;
   using Microsoft.AspNetCore.Mvc;

   namespace BlogApi.Controllers;

   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
      private readonly UserManager<IdentityUser> _userManager;
      private readonly BlogApiContext _context;
      private readonly TokenService _tokenService;

      public AuthController(UserManager<IdentityUser> userManager, BlogApiContext context, TokenService tokenService)
      {
         _userManager = userManager;
         _context = context;
         _tokenService = tokenService;
      }

      [HttpPost]
      [Route("register")]
      public async Task<IActionResult> Register(RegisterDto registerDto)
      {
         if (!ModelState.IsValid)
         {
               return BadRequest(ModelState);
         }
         var result = await _userManager.CreateAsync(
               new IdentityUser { UserName = registerDto.Username, Email = registerDto.Email },
               registerDto.Password
         );
         if (result.Succeeded)
         {
               return Created("User created", new { Email = registerDto.Email, Username = registerDto.Username });
         }
         foreach (var error in result.Errors)
         {
               ModelState.AddModelError(error.Code, error.Description);
         }
         return BadRequest(ModelState);
      }

      [HttpPost]
      [Route("login")]
      public async Task<ActionResult<TokenDto>> Authenticate([FromBody] LoginDto loginDto)
      {
         if (!ModelState.IsValid)
         {
               return BadRequest(ModelState);
         }

         var user = await _userManager.FindByEmailAsync(loginDto.Email);
         if (user == null)
         {
               return NotFound();
         }
         var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
         if (!isPasswordValid)
         {
               return Unauthorized();
         }
         var accessToken = _tokenService.CreateToken(user);
         return Ok(new TokenDto
         {
               Username = user.UserName!,
               Email = user.Email!,
               Token = accessToken,
         });
      }
   }
   ```

8. Create the `RegisterDto.cs`, `LoginDto.cs` and `TokenDto.cs` files in the `Dtos` folder. Then, add the following code:

   ```csharp
   // RegisterDto.cs
   using System.ComponentModel.DataAnnotations;

   namespace BlogApi.Dtos;

   public class RegisterDto
   {
      [Required]
      [EmailAddress]
      public string Email { get; set; } = null!;

      [Required]
      public string Username { get; set; } = null!;

      [Required]
      [MinLength(8)]
      [MaxLength(16)]
      [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
      public string Password { get; set; } = null!;
   }
   ```

   ```csharp
   // LoginDto.cs
   using System.ComponentModel.DataAnnotations;

   namespace BlogApi.Dtos;

   public class LoginDto
   {
      [Required]
      [EmailAddress]
      public string Email { get; set; } = null!;

      [Required]
      [MinLength(8)]
      [MaxLength(16)]
      [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
      public string Password { get; set; } = null!;
   }
   ```

   ```csharp
   // TokenDto.cs
   namespace BlogApi.Dtos;

   public class TokenDto
   {
      public string Username { get; set; } = null!;
      public string Email { get; set; } = null!;
      public string Token { get; set; } = null!;
   }
   ```

9. Now, we can add security to our API. Open the `PostsController.cs` file in the `Controllers` folder. Then, add the `[Authorize]` attribute to the class:

   ```csharp
   [Authorize]
   [Route("api/[controller]")]
   [ApiController]
   public class PostsController : ControllerBase
   {
      // ...
   }
   ```

   We can also add the `[Authorize]` attribute to a single method:

   ```csharp
   [HttpGet]
   [Authorize]
   public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
   {
      // ...
   }
   ```

   If we need to allow anonymous access to a method, we can add the `[AllowAnonymous]` attribute:

   ```csharp
   [HttpGet]
   [AllowAnonymous]
   public async Task<ActionResult<PostDto>> Get(PostDto postDto)
   {
      // ...
   }
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

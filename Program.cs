using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Identity;
using BlogApi.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogApiContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("BlogDatabase"))
);

// Add AutoMapper (using EntityFrameworkCore extension)
builder.Services.AddAutoMapper((serviceProvider, automapper) =>
{
    automapper.AddCollectionMappers();
    automapper.UseEntityFrameworkCoreModel<BlogApiContext>(serviceProvider);
}, typeof(BlogApiContext).Assembly);

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

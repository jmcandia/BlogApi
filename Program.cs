using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Identity;
using BlogApi.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters()
      {
          ClockSkew = TimeSpan.Zero,
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = "BlogApi",
          ValidAudience = "BlogApi",
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings")["Secret"]!)
         ),
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

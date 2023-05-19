using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogApiContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("BlogDatabase"))
);

// // Add AutoMapper
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add AutoMapper (using EntityFrameworkCore extension)
builder.Services.AddAutoMapper((serviceProvider, automapper) =>
{
    automapper.AddCollectionMappers();
    automapper.UseEntityFrameworkCoreModel<BlogApiContext>(serviceProvider);
}, typeof(BlogApiContext).Assembly);

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

app.UseAuthorization();

app.MapControllers();

app.Run();

using Api.Domain.DTOs;
using Api.Domain.Service;
using Api.Domain.Service.Interfaces;
using Api.Infrastructure.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<ApplicationContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.
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

app.MapGet("/api/health_check", () =>
    {
        var now = DateTime.Now;
        var dbString = builder.Configuration["DB_CONNECTION_STRING"];
        Console.WriteLine("Health check performed at: " + dbString);

        return Results.Ok(new
        {
            Message = "The service is up and running",
            Time = now.ToLongTimeString(),
            Date = now.ToLongDateString()
        });
    })
    .WithName("HealthCheck")
    .WithOpenApi();

app.MapPost("/api/signup", async ([FromBody] SignUpDto signUp, IUserService service) =>
{
    var user = await service.CreateUser(signUp);
    return Results.Created($"/api/user/{user.Email}", user);
}).WithName("SignUp").WithOpenApi().WithTags("User");

app.MapPost("/api/signin", async ([FromBody] LoginDTO login, IUserService service) =>
{
    var user = await service.GetUser(login.Email, login.Password);
    return user is not null ? Results.Ok(user) : Results.Unauthorized();
}).WithName("GetUser").WithOpenApi().WithTags("User");

app.Run();

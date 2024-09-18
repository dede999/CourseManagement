using Api.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
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

app.MapGet("/health_check", () =>
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

app.Run();

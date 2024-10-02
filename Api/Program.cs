using System.Text.Json.Serialization;
using Api.Domain.DTOs;
using Api.Domain.DTOs.Course;
using Api.Domain.Service;
using Api.Domain.Service.Interfaces;
using Api.Infrastructure.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
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
    var response = await service.CreateUser(signUp);
    return response.IsValid
        ? Results.Created($"/api/user/{response.Data!.Email}", response.Data.ToUserResponseDto())
        : Results.BadRequest(response.Errors);
}).WithName("SignUp").WithOpenApi().WithTags("User");

app.MapPost("/api/signin", async ([FromBody] LoginDto login, IUserService service) =>
{
    var response = await service.GetUser(login.Email, login.Password);
    return response.IsValid ? Results.Ok(response.Data!.ToUserResponseDto()) : Results.BadRequest(response.Errors);
}).WithName("GetUser").WithOpenApi().WithTags("User");

#region course
app.MapGet("/api/courses", async ([FromQuery] int? page, ICourseService service) =>
{
    var response = await service.AllCourses(page ?? 1);
    return response.IsValid
        ? Results.Ok(response.Data!)
        : Results.BadRequest(response.Errors);
}).WithName("All Courses").WithOpenApi().WithTags("Course");

app.MapPost("/api/courses", async ([FromBody] CourseDto course, ICourseService service) =>
{
    var response = await service.CreateCourse(course);
    return response.IsValid
        ? Results.Created($"/api/courses/{response.Data!.Code}", response.Data)
        : Results.BadRequest(response.Errors);
}).WithName("Create Course").WithOpenApi().WithTags("Course");

app.MapGet("/api/courses/{code}", async ([FromRoute] Guid code, ICourseService service) =>
{
    var response = await service.GetCourse(code);
    return response.IsValid ? Results.Ok(response.Data!) : Results.BadRequest(response.Errors);
}).WithName("Get Course").WithOpenApi().WithTags("Course");

app.MapPut("/api/courses/{code}", async ([FromRoute] Guid code, [FromBody] CourseDto course, ICourseService service) =>
{
    var response = await service.UpdateCourse(code, course);
    return response.IsValid ? Results.Ok(response.Data) : Results.BadRequest(response.Errors);
}).WithName("Update Course").WithOpenApi().WithTags("Course");

app.MapDelete("/api/courses/{code}", ([FromRoute] Guid code, ICourseService service) =>
{
    service.DeleteCourse(code);
    return Results.NoContent();
}).WithName("Delete Course").WithOpenApi().WithTags("Course");
#endregion

app.Run();

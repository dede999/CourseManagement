using Api.Domain.DTOs.Course;
using Api.Domain.Entities;
using Api.Domain.Service.Interfaces;
using Api.Infrastructure.DB;
using Api.Infrastructure.Request;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Service;

public class CourseService(ApplicationContext context) : ICourseService
{
    private static int PerPageInstances => 10;


    public Task<RetrieveResponse<CourseResponseDto[]>> AllCourses(int page)
    {
        var courses = context.Courses
            .Include(c => c.Creator)
            .Select(c => c.ToResponseDto())
            .Skip((page - 1) * PerPageInstances)
            .Take(PerPageInstances).ToArray();

        RetrieveResponse<CourseResponseDto[]> response = new(courses);
        return Task.FromResult(response);
    }

    public Task<ValidationResponse<CourseResponseDto>> CreateCourse(CourseDto course)
    {
        var courseInstance = Course.FromDto(course);
        var validationResult = new ValidationResponse<Course>(courseInstance);
        if (validationResult.IsValid)
        {
            context.Courses.Add(courseInstance);
            context.SaveChanges();
            return Task.FromResult(
                new ValidationResponse<CourseResponseDto>(
                    courseInstance.ToResponseDto()));
        }
        else
        {
            return Task.FromResult(validationResult.Cast(validationResult.Data!.ToResponseDto()));
        }
    }

    public Task<RetrieveResponse<CourseResponseDto?>> GetCourse(Guid code)
    {
        
        var course = context.Courses
            .Include(c => c.Creator)
            .FirstOrDefault(c => c.Code == code);

        return course != null
            ? Task.FromResult(new RetrieveResponse<CourseResponseDto?>(course.ToResponseDto()))
            : Task.FromResult(new RetrieveResponse<CourseResponseDto?>("Course not found"));
    }

    public Task<ValidationResponse<Course?>> UpdateCourse(Guid code, CourseDto course)
    {
        Course? courseInstance = context.Courses.FirstOrDefault(c => c.Code == code);

        if (courseInstance == null)
        {
            return Task.FromResult(new ValidationResponse<Course?>("Database", "Course not found"));
        }
        var courses = Course.FromDto(course, code);
        var validationResult = new ValidationResponse<Course?>(courses);
        if (validationResult.IsValid)
        {
            context.Courses.Update(courses);
            context.SaveChanges();
        }

        return Task.FromResult(validationResult);
    }

    public void DeleteCourse(Guid code)
    {
        var course = context.Courses.First(c => c.Code == code);
        context.Courses.Remove(course);
        context.SaveChanges();
    }
}
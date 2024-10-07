using Api.Domain.DTOs.Course;
using Api.Domain.Entities;
using Api.Domain.Service.Interfaces;
using Api.Infrastructure;
using Api.Infrastructure.DB;
using Api.Infrastructure.Request;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Service;

public class CourseService(ApplicationContext context) : GenericService(context), ICourseService
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
        try
        {
            context.Courses.Add(courseInstance);
            context.SaveChanges();
            context.Entry(courseInstance).Reference(c => c.Creator).Load();
            return Task.FromResult(new ValidationResponse<CourseResponseDto>(courseInstance.ToResponseDto()));
        }
        catch (Exception e)
        {
            return Task.FromResult(
                new ValidationResponse<CourseResponseDto>("Database", e.Message));
        }
    }
    else
    {
        return Task.FromResult(validationResult.Cast(courseInstance.ToResponseDto()));
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

    public Task<ValidationResponse<CourseResponseDto?>> UpdateCourse(Guid code, CourseDto course)
    {
        Course? courseInstance = GetInstanceByCode(code, context.Courses);

        if (courseInstance == null)
        {
            return Task.FromResult(new ValidationResponse<CourseResponseDto?>("Database", "Course not found"));
        }
        var courses = Course.FromDto(course, courseInstance);
        var validationResult = new ValidationResponse<Course?>(courses);
        if (validationResult.IsValid)
        {
            try
            {
                context.Courses.Update(courses);
                context.SaveChanges();
                context.Entry(courseInstance).Reference(c => c.Creator).Load();
                return Task.FromResult(new ValidationResponse<CourseResponseDto?>(courses.ToResponseDto()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(new ValidationResponse<CourseResponseDto?>("Database", e.Message));
            }
        } else
        {
            return Task.FromResult(validationResult.Cast(courseInstance.ToResponseDto()))!;
        }
    }

    public bool DeleteCourse(Guid code)
    {
        var course = GetInstanceByCode(code, context.Courses);
        return DeleteInstanceByCode(course, context.Courses);
    }
}
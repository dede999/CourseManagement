using Api.Domain.DTOs.Course;
using Api.Domain.Entities;
using Api.Infrastructure.Request;

namespace Api.Domain.Service.Interfaces;

public interface ICourseService
{
    Task<RetrieveResponse<CourseResponseDto[]>> AllCourses(int page = 1);
    Task<ValidationResponse<CourseResponseDto>> CreateCourse(CourseDto course);
    Task<RetrieveResponse<CourseResponseDto?>> GetCourse(Guid code);
    Task<ValidationResponse<CourseResponseDto?>> UpdateCourse(Guid code, CourseDto course);
    bool DeleteCourse(Guid code);
}

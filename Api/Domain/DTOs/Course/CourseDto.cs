namespace Api.Domain.DTOs.Course;

public record CourseDto(
    string Title,
    string Description,
    string EndDate,
    string CreatorEmail);
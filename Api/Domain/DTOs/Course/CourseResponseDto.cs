namespace Api.Domain.DTOs.Course;

public record CourseResponseDto(
    Guid Code,
    string Title,
    string Description,
    DateTime EndDate,
    string Creator,
    string CreatorEmail);
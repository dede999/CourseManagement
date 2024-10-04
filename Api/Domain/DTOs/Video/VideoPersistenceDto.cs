namespace Api.Domain.DTOs;

public record VideoPersistenceDto(
    string Title,
    string Description,
    string VideoUrl,
    int Duration,
    int BlockNumber,
    string BlockTitle,
    Guid CourseCode);
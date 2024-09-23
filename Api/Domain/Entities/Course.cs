using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Api.Domain.DTOs.Course;

namespace Api.Domain.Entities;

public class Course
{
    [Key]
    [Required]
    public Guid Code { get; set; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string Title { get; set; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public string CreatorEmail { get; set; }
    
    public User Creator { get; set; } = null!;
    
    public static Course FromDto(CourseDto dto, Guid? code = null) {
        (string title, string description, string endDate, string creatorEmail) = dto;
        
        return new Course
        {
            Code = code ?? Guid.NewGuid(),
            Title = title,
            Description = description,
            EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToUniversalTime(),
            CreatorEmail = creatorEmail
        };
    }
    
    public CourseResponseDto ToResponseDto() {
        return new CourseResponseDto(Code, Title, Description, EndDate, Creator.Name, Creator.Email);
    }
}

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Api.Domain.DTOs.Course;
using Api.Infrastructure.DB;

namespace Api.Domain.Entities;

public class Course: GenericEntity
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
    
    public ICollection<Video> Videos { get; } = new List<Video>();
    
    public static Course FromDto(CourseDto dto, Course? course = null) {
        (string title, string description, string endDate, string creatorEmail) = dto;
        
        if (course == null) {
            return new Course {
                Code = Guid.NewGuid(),
                Title = title,
                Description = description,
                EndDate = DateTime.Parse(endDate, CultureInfo.InvariantCulture).ToUniversalTime(),
                CreatorEmail = creatorEmail
            };
        }
        else
        {
            course.Title = title;
            course.Description = description;
            course.EndDate = DateTime.Parse(endDate, CultureInfo.InvariantCulture).ToUniversalTime();
            course.CreatorEmail = creatorEmail;
            return course;
        }
    }
    
    public CourseResponseDto ToResponseDto() {
        return new CourseResponseDto(Code, Title, Description, EndDate, Creator.Name, Creator.Email);
    }
}

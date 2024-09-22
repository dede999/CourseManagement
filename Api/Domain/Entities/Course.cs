using System.ComponentModel.DataAnnotations;

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
}
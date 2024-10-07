using System.ComponentModel.DataAnnotations;
using Api.Domain.DTOs;
using Api.Infrastructure.DB;

namespace Api.Domain.Entities;

public class Video : GenericEntity
{
    [Key]
    [Required]
    public Guid Code { get; set; }
    
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Required]
    [Url]
    public string VideoUrl { get; set; }
    
    [Required]
    public  int Duration { get; set; }

    public int BlockNumber { get; set; } = 1;
    
    public string BlockTitle { get; set; } = "";

    [Required]
    public Guid CourseCode { get; set; }
    
    public Course Course { get; set; } = null!;

    public static Video FromDto(VideoPersistenceDto formData)
    {
        var video = new Video();
        video.Code = Guid.NewGuid();
        video.SetProperties(formData);
        return video;
    }

    public Video Update(VideoPersistenceDto formData)
    {
        SetProperties(formData);
        return this;
    }
    
    private void SetProperties(VideoPersistenceDto formData)
    {
        (
            string title, string description, string videoUrl, int duration,
            int blockNumber, string blockTitle, Guid courseCode
        ) = formData;
        
        Title = title;
        Description = description;
        VideoUrl = videoUrl;
        Duration = duration;
        BlockNumber = blockNumber;
        BlockTitle = blockTitle;
        CourseCode = courseCode;
    }
}
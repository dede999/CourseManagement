using Api.Domain.Entities;

namespace Api.Domain.DTOs.Video;                        

public record VideoBlockDto(string Name, int Id, Entities.Video[] Videos);
using Api.Domain.DTOs;
using Api.Domain.DTOs.Video;
using Api.Domain.Entities;
using Api.Infrastructure.Request;

namespace Api.Domain.Service.Interfaces;

public interface IVideoService
{
    Task<RetrieveResponse<VideoBlockDto[]>> AllVideos(Guid courseCode);
    Task<ValidationResponse<Video>> CreateVideo(VideoPersistenceDto video);
    Task<RetrieveResponse<Video?>> GetVideo(Guid code);
    Task<ValidationResponse<Video?>> UpdateVideo(Guid code, VideoPersistenceDto video);
    bool DeleteVideo(Guid code);
}
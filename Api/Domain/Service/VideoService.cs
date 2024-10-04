using Api.Domain.DTOs;
using Api.Domain.DTOs.Video;
using Api.Domain.Entities;
using Api.Domain.Service.Interfaces;
using Api.Infrastructure.DB;
using Api.Infrastructure.Request;

namespace Api.Domain.Service;

public class VideoService(ApplicationContext context): IVideoService
{
    public Task<RetrieveResponse<VideoBlockDto[]>> AllVideos(Guid courseCode)
    {
        var data = context.Videos
            .Where(v => v.CourseCode == courseCode)
            .GroupBy(c => c.BlockNumber)
            .Select(g => new VideoBlockDto(g.First().BlockTitle, g.Key, g.ToArray()))
            .ToArray();
        
        return Task.FromResult(new RetrieveResponse<VideoBlockDto[]>(data));
    }

    public Task<ValidationResponse<Video>> CreateVideo(VideoPersistenceDto video)
    {
        throw new NotImplementedException();
    }

    public Task<RetrieveResponse<Video?>> GetVideo(Guid code)
    {
        var video = GetVideoByCode(code);
        
        return video != null
            ? Task.FromResult(new RetrieveResponse<Video?>(video))
            : Task.FromResult(new RetrieveResponse<Video?>("Video not found"));
    }

    public Task<ValidationResponse<Video?>> UpdateVideo(Guid code, VideoPersistenceDto video)
    {
        throw new NotImplementedException();
    }

    public void DeleteVideo(Guid code)
    {
        throw new NotImplementedException();
    }
    
    private Video? GetVideoByCode(Guid code)
    {
        return context.Videos.FirstOrDefault(v => v.Code == code);
    }
}
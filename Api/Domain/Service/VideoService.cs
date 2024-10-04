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
        var videoInstance = Video.FromDto(video);
        var validationResult = new ValidationResponse<Video>(videoInstance);
        if (validationResult.IsValid)
        {
            try
            {
                context.Videos.Add(videoInstance);
                context.SaveChanges();
                return Task.FromResult(new ValidationResponse<Video>(videoInstance));
            }
            catch (Exception e)
            {
                return Task.FromResult(new ValidationResponse<Video>("Database", e.Message));
            }
        }
        else
        {
            return Task.FromResult(validationResult);
        }
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
        var videoInstance = GetVideoByCode(code);
        if (videoInstance == null)
        {
            return Task.FromResult(new ValidationResponse<Video?>("Database", "Video not found"));
        }

        var validationResult = new ValidationResponse<Video>(videoInstance.Update(video));
        if (validationResult.IsValid)
        {
            try
            {
                context.Videos.Update(videoInstance);
                context.SaveChanges();
                return Task.FromResult(new ValidationResponse<Video?>(videoInstance));
            }
            catch (Exception e)
            {
                return Task.FromResult(new ValidationResponse<Video?>("Database", e.Message));
            }
        }
        else
        {
            return Task.FromResult(validationResult.Cast<Video?>(videoInstance));
        }
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
namespace Api.Infrastructure.Request;

public interface Response<T>
{
    public T? Data { get; set; }
    public Dictionary<string, string> Errors { get; set; }
    public bool IsValid { get; set; }
}
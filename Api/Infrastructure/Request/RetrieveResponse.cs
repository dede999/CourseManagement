using Api.Domain.Entities;

namespace Api.Infrastructure.Request;

public class RetrieveResponse<T> : Response<T>
{
    public T? Data { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();
    public bool IsValid { get; set; }
    
    public RetrieveResponse(T data)
    {
        IsValid = true;
        Data = data;
    }
    
    public RetrieveResponse(string errorMessage)
    {
        IsValid = false;
        Errors.Add("error", errorMessage);
    }
}
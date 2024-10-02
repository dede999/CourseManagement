using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.Request;

public class ValidationResponse<T> : Response<T>
{
    public T? Data { get; set; }
    public Dictionary<string, string?> Errors { get; set; } = new();
    public List<ValidationResult> Validations { get; set; } = new();
    public bool IsValid { get; set; }
    
    public ValidationResponse(string errorKey, string errorMessage)
    {
        Errors.Add(errorKey, errorMessage);
        IsValid = false;
    }

    public ValidationResponse(T data)
    {
        Data = data;
        var context = new ValidationContext(data);
        IsValid = Validator.TryValidateObject(data, context, Validations, true);
        if (!IsValid)
        {
            TransferErrors();
        }
    }
    
    public void TransferErrors()
    {
        foreach (var validation in Validations)
        {
            Errors.Add(validation.MemberNames.First(), validation.ErrorMessage);
        }
    }
    
    public ValidationResponse<TK> Cast<TK>(TK newData)
    {
        return new ValidationResponse<TK>(newData)
        {
            Errors = Errors,
            IsValid = IsValid,
            Validations = Validations
        };
    }
}
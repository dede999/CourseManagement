using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Infrastructure.DB;

namespace Api.Infrastructure.Request;

public class SignInResponse: Response<User>
{
    public User? Data { get; set; }
    public Dictionary<string, string> Errors { get; set; } = new();
    public bool IsValid { get; set; }

    public SignInResponse(LoginDto data, ApplicationContext ctx)
    {
        var user = ctx.Users.Find(data.Email);
        if (user is not null)
        {
            if (user.Password != data.Password)
            {
                IsValid = false;
                Errors.Add("password", "Invalid password");
            } else
            {
                IsValid = true;
                Data = user;
            }
        }
        else
        {
            IsValid = false;
            Errors.Add("email", "User not found");
        }
    }
}
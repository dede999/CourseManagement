using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Infrastructure.DB;
using static BCrypt.Net.BCrypt;

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
            if (!Verify(data.Password, user.Password))
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
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Service.Interfaces;
using Api.Infrastructure.DB;

namespace Api.Domain.Service;

public class UserService: IUserService
{
    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Response<User>> CreateUser(SignUpDto signUp)
    {
        var user = User.FromSignUp(signUp);
        ValidationResponse<User> validationResponse = new(user);
        if (validationResponse.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        return validationResponse;
    }

    public Task<User?> GetUser(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(
            u => u.Email == email && u.Password == password);
        return Task.FromResult(user);
    }
}
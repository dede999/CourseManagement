using Api.Domain.DTOs;
using Api.Domain.Entities;

namespace Api.Domain.Service.Interfaces;

public interface IUserService
{
    Task<User> CreateUser(SignUpDto signUp);
    Task<User?> GetUser(string email, string password);
}
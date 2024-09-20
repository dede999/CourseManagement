using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Infrastructure.Request;

namespace Api.Domain.Service.Interfaces;

public interface IUserService
{
    Task<Response<User>> CreateUser(SignUpDto signUp);
    Task<User?> GetUser(string email, string password);
}
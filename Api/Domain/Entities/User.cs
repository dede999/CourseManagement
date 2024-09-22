using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Api.Domain.DTOs;
using Api.Domain.Enums;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace Api.Domain.Entities;

public class User
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public UserRoles Role { get; set; } = UserRoles.Student;
    
    private static int _workFactor = 12;

    public static User FromSignUp(SignUpDto signUp) {
        (string email, string password, string name) = signUp;

        return new User
        {
            Email = email,
            Password = HashPassword(password, User._workFactor),
            Name = name
        };
    }
    
    public UserResponseDTO ToUserResponseDto() {
        return new UserResponseDTO(Email, Name, Role);
    }
}
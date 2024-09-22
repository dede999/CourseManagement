using Api.Domain.Enums;

namespace Api.Domain.DTOs;

public record UserResponseDTO(string email, string name, UserRoles role);
using Domain.Enums;

namespace Application.DTOs.Users;

public record UserDto(int Id, string Name, string Email, UserRole Role);

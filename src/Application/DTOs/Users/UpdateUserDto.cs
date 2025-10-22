using Domain.Enums;

namespace Application.DTOs.Users;

public record UpdateUserDto(string Name, UserRole Role);

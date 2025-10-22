namespace Application.DTOs.Auth;

public record AuthRegisterDto(string Name, string Email, string Password, string Role);

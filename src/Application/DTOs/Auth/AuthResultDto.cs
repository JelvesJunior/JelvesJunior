namespace Application.DTOs.Auth;

public record AuthResultDto(string Token, DateTime ExpiresAt);

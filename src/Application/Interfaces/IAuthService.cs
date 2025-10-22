using Application.DTOs.Auth;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(AuthRegisterDto dto, CancellationToken cancellationToken = default);
    Task<AuthResultDto> LoginAsync(AuthLoginDto dto, CancellationToken cancellationToken = default);
}

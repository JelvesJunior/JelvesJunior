using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResultDto> RegisterAsync(AuthRegisterDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);
        if (existing is not null)
        {
            throw new InvalidOperationException("Email already registered");
        }

        if (!Enum.TryParse<UserRole>(dto.Role, true, out var role))
        {
            throw new InvalidOperationException("Invalid role");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = role,
            PasswordHash = PasswordHasher.Hash(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return GenerateToken(user);
    }

    public async Task<AuthResultDto> LoginAsync(AuthLoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);
        if (user is null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Invalid credentials");
        }

        return GenerateToken(user);
    }

    private AuthResultDto GenerateToken(User user)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var issuer = jwtSection["Issuer"] ?? "StreamingApi";
        var audience = jwtSection["Audience"] ?? issuer;
        var secret = jwtSection["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured");
        var expirationMinutes = int.TryParse(jwtSection["ExpiresInMinutes"], out var minutes) ? minutes : 60;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
            new("name", user.Name)
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResultDto(new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}

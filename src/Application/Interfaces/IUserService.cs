using Application.Common;
using Application.DTOs.Users;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<UserDto>> GetAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, UpdateUserDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

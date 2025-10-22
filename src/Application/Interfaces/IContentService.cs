using Application.Common;
using Application.DTOs.Content;

namespace Application.Interfaces;

public interface IContentService
{
    Task<int> CreateAsync(int creatorId, CreateContentDto dto, CancellationToken cancellationToken = default);
    Task<ContentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<ContentDto>> SearchAsync(int? creatorId, string? query, int page, int pageSize, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, int creatorId, UpdateContentDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, int creatorId, CancellationToken cancellationToken = default);
}

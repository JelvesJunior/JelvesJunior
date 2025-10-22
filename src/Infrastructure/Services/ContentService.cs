using Application.Common;
using Application.DTOs.Content;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ContentService : IContentService
{
    private readonly AppDbContext _context;

    public ContentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(int creatorId, CreateContentDto dto, CancellationToken cancellationToken = default)
    {
        var content = new Content
        {
            Title = dto.Title,
            Description = dto.Description,
            MediaUrl = dto.MediaUrl,
            CreatorId = creatorId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Contents.Add(content);
        await _context.SaveChangesAsync(cancellationToken);
        return content.Id;
    }

    public async Task<ContentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Contents.AsNoTracking()
            .Select(c => new ContentDto(c.Id, c.Title, c.Description, c.MediaUrl, c.CreatorId, c.CreatedAt))
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<PagedResult<ContentDto>> SearchAsync(int? creatorId, string? query, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var baseQuery = _context.Contents.AsNoTracking().AsQueryable();

        if (creatorId.HasValue)
        {
            baseQuery = baseQuery.Where(c => c.CreatorId == creatorId);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = query.Trim().ToLower();
            baseQuery = baseQuery.Where(c => EF.Functions.Like(c.Title.ToLower(), $"%{pattern}%"));
        }

        var total = await baseQuery.CountAsync(cancellationToken);
        var items = await baseQuery
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ContentDto(c.Id, c.Title, c.Description, c.MediaUrl, c.CreatorId, c.CreatedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<ContentDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = total
        };
    }

    public async Task UpdateAsync(int id, int creatorId, UpdateContentDto dto, CancellationToken cancellationToken = default)
    {
        var content = await _context.Contents.FirstOrDefaultAsync(c => c.Id == id && c.CreatorId == creatorId, cancellationToken);
        if (content is null)
        {
            throw new KeyNotFoundException("Content not found");
        }

        content.Title = dto.Title;
        content.Description = dto.Description;
        content.MediaUrl = dto.MediaUrl;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, int creatorId, CancellationToken cancellationToken = default)
    {
        var content = await _context.Contents.FirstOrDefaultAsync(c => c.Id == id && c.CreatorId == creatorId, cancellationToken);
        if (content is null)
        {
            throw new KeyNotFoundException("Content not found");
        }

        _context.Contents.Remove(content);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

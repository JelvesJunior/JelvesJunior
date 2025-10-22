using Application.Common;
using Application.DTOs.Content;
using Application.DTOs.Playlists;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly AppDbContext _context;

    public PlaylistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(CreatePlaylistDto dto, int creatorId, CancellationToken cancellationToken = default)
    {
        var playlist = new Playlist
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatorId = creatorId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync(cancellationToken);
        return playlist.Id;
    }

    public async Task<PlaylistDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Playlists.AsNoTracking()
            .Include(p => p.Items)
            .ThenInclude(i => i.Content)
            .Where(p => p.Id == id)
            .Select(p => new PlaylistDto(
                p.Id,
                p.Title,
                p.Description,
                p.CreatorId,
                p.CreatedAt,
                p.Items
                    .OrderBy(i => i.Order)
                    .Select(i => new PlaylistItemDto(
                        i.Id,
                        i.ContentId,
                        i.Content!.Title,
                        i.Content.Description,
                        i.Content.MediaUrl,
                        i.Order))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<PlaylistDto>> SearchAsync(PlaylistQuery query, CancellationToken cancellationToken = default)
    {
        var baseQuery = _context.Playlists.AsNoTracking()
            .Include(p => p.Items)
            .ThenInclude(i => i.Content)
            .AsQueryable();

        if (query.CreatorId.HasValue)
        {
            baseQuery = baseQuery.Where(p => p.CreatorId == query.CreatorId);
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var pattern = query.Search.Trim().ToLower();
            baseQuery = baseQuery.Where(p => EF.Functions.Like(p.Title.ToLower(), $"%{pattern}%"));
        }

        var total = await baseQuery.CountAsync(cancellationToken);
        var items = await baseQuery
            .OrderByDescending(p => p.CreatedAt)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new PlaylistDto(
                p.Id,
                p.Title,
                p.Description,
                p.CreatorId,
                p.CreatedAt,
                p.Items
                    .OrderBy(i => i.Order)
                    .Select(i => new PlaylistItemDto(
                        i.Id,
                        i.ContentId,
                        i.Content!.Title,
                        i.Content.Description,
                        i.Content.MediaUrl,
                        i.Order))
                    .ToList()))
            .ToListAsync(cancellationToken);

        return new PagedResult<PlaylistDto>
        {
            Items = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = total
        };
    }

    public async Task UpdateAsync(int id, UpdatePlaylistDto dto, int creatorId, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == id && p.CreatorId == creatorId, cancellationToken);
        if (playlist is null)
        {
            throw new KeyNotFoundException("Playlist not found");
        }

        playlist.Title = dto.Title;
        playlist.Description = dto.Description;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, int creatorId, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == id && p.CreatorId == creatorId, cancellationToken);
        if (playlist is null)
        {
            throw new KeyNotFoundException("Playlist not found");
        }

        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddItemAsync(int playlistId, int contentId, int order, int creatorId, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId && p.CreatorId == creatorId, cancellationToken);
        if (playlist is null)
        {
            throw new KeyNotFoundException("Playlist not found");
        }

        var contentExists = await _context.Contents.AnyAsync(c => c.Id == contentId && c.CreatorId == creatorId, cancellationToken);
        if (!contentExists)
        {
            throw new KeyNotFoundException("Content not found");
        }

        var item = new PlaylistItem
        {
            PlaylistId = playlistId,
            ContentId = contentId,
            Order = order
        };

        _context.PlaylistItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveItemAsync(int playlistId, int itemId, int creatorId, CancellationToken cancellationToken = default)
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == playlistId && p.CreatorId == creatorId, cancellationToken);
        if (playlist is null)
        {
            throw new KeyNotFoundException("Playlist not found");
        }

        var item = await _context.PlaylistItems.FirstOrDefaultAsync(pi => pi.Id == itemId && pi.PlaylistId == playlistId, cancellationToken);
        if (item is null)
        {
            throw new KeyNotFoundException("Item not found");
        }

        _context.PlaylistItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

using Application.Common;
using Application.DTOs.Playlists;

namespace Application.Interfaces;

public interface IPlaylistService
{
    Task<int> CreateAsync(int creatorId, CreatePlaylistDto dto, CancellationToken cancellationToken = default);
    Task<PlaylistDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<PlaylistDto>> SearchAsync(PlaylistQuery query, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, int creatorId, UpdatePlaylistDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, int creatorId, CancellationToken cancellationToken = default);
    Task AddItemAsync(int playlistId, int creatorId, AddPlaylistItemDto dto, CancellationToken cancellationToken = default);
    Task RemoveItemAsync(int playlistId, int creatorId, int itemId, CancellationToken cancellationToken = default);
}

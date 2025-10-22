using Application.Common;
using Application.DTOs.Playlists;

namespace Application.Interfaces;

public interface IPlaylistRepository
{
    Task<int> CreateAsync(CreatePlaylistDto dto, int creatorId, CancellationToken cancellationToken = default);
    Task<PlaylistDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<PlaylistDto>> SearchAsync(PlaylistQuery query, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, UpdatePlaylistDto dto, int creatorId, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, int creatorId, CancellationToken cancellationToken = default);
    Task AddItemAsync(int playlistId, int contentId, int order, int creatorId, CancellationToken cancellationToken = default);
    Task RemoveItemAsync(int playlistId, int itemId, int creatorId, CancellationToken cancellationToken = default);
}

using Application.Common;
using Application.DTOs.Playlists;
using Application.Interfaces;

namespace Infrastructure.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _repository;

    public PlaylistService(IPlaylistRepository repository)
    {
        _repository = repository;
    }

    public Task<int> CreateAsync(int creatorId, CreatePlaylistDto dto, CancellationToken cancellationToken = default)
        => _repository.CreateAsync(dto, creatorId, cancellationToken);

    public Task<PlaylistDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<PagedResult<PlaylistDto>> SearchAsync(PlaylistQuery query, CancellationToken cancellationToken = default)
        => _repository.SearchAsync(query, cancellationToken);

    public Task UpdateAsync(int id, int creatorId, UpdatePlaylistDto dto, CancellationToken cancellationToken = default)
        => _repository.UpdateAsync(id, dto, creatorId, cancellationToken);

    public Task DeleteAsync(int id, int creatorId, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, creatorId, cancellationToken);

    public Task AddItemAsync(int playlistId, int creatorId, AddPlaylistItemDto dto, CancellationToken cancellationToken = default)
        => _repository.AddItemAsync(playlistId, dto.ContentId, dto.Order, creatorId, cancellationToken);

    public Task RemoveItemAsync(int playlistId, int creatorId, int itemId, CancellationToken cancellationToken = default)
        => _repository.RemoveItemAsync(playlistId, itemId, creatorId, cancellationToken);
}

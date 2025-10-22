namespace Application.DTOs.Playlists;

public record PlaylistDto(int Id, string Title, string Description, int CreatorId, DateTime CreatedAt, IReadOnlyCollection<PlaylistItemDto> Items);

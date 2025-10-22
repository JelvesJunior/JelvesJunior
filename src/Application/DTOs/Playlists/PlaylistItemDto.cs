namespace Application.DTOs.Playlists;

public record PlaylistItemDto(int Id, int ContentId, string Title, string Description, string MediaUrl, int Order);

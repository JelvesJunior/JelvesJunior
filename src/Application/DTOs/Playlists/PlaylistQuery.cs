namespace Application.DTOs.Playlists;

public class PlaylistQuery
{
    public int? CreatorId { get; set; }
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

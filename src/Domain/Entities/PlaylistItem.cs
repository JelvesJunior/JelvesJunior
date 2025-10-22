namespace Domain.Entities;

public class PlaylistItem
{
    public int Id { get; set; }
    public int PlaylistId { get; set; }
    public int ContentId { get; set; }
    public int Order { get; set; }

    public Playlist? Playlist { get; set; }
    public Content? Content { get; set; }
}

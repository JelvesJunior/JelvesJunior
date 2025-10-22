namespace Domain.Entities;

public class Playlist
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? Creator { get; set; }
    public ICollection<PlaylistItem> Items { get; set; } = new List<PlaylistItem>();
}

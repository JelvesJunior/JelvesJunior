using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public ICollection<Content> Contents { get; set; } = new List<Content>();
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}

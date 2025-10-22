using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PlaylistItemConfiguration : IEntityTypeConfiguration<PlaylistItem>
{
    public void Configure(EntityTypeBuilder<PlaylistItem> builder)
    {
        builder.ToTable("PlaylistItems");
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Order).IsRequired();

        builder.HasOne(pi => pi.Playlist)
            .WithMany(p => p.Items)
            .HasForeignKey(pi => pi.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pi => pi.Content)
            .WithMany(c => c.PlaylistItems)
            .HasForeignKey(pi => pi.ContentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(pi => new { pi.PlaylistId, pi.Order }).IsUnique();
    }
}

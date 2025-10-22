using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable("Playlists");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).HasMaxLength(120).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(300);
        builder.Property(p => p.CreatedAt).IsRequired();

        builder.HasOne(p => p.Creator)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

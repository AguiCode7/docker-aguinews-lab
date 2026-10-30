using AC.Core.Entities;
using AC.Core.Modules.Videos.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.Data.Configurations;

// Mapeo de la entidad ChannelVideo a la tabla "channel_videos" (snake_case en PostgreSQL).
public class ChannelVideoConfiguration : IEntityTypeConfiguration<ChannelVideo>
{
    public const int KindMaxLength = 10;

    public void Configure(EntityTypeBuilder<ChannelVideo> builder)
    {
        builder.ToTable("channel_videos");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("id");
        builder.Property(v => v.YouTubeId).HasColumnName("youtube_id")
            .HasMaxLength(UpsertVideoValidator.YouTubeIdMaxLength).IsRequired();
        builder.Property(v => v.Title).HasColumnName("title")
            .HasMaxLength(UpsertVideoValidator.TitleMaxLength).IsRequired();
        // El tipo se guarda como texto ("Video", "Short") para que se lea en la base.
        builder.Property(v => v.Kind).HasColumnName("kind")
            .HasConversion<string>().HasMaxLength(KindMaxLength).IsRequired();
        builder.Property(v => v.NewsUrl).HasColumnName("news_url")
            .HasMaxLength(LinkNewsValidator.NewsUrlMaxLength);
        builder.Property(v => v.PublishedAt).HasColumnName("published_at");
        builder.Property(v => v.CreatedAt).HasColumnName("created_at");
        builder.Property(v => v.UpdatedAt).HasColumnName("updated_at");

        // Un video de YouTube solo puede estar una vez; el sitio ordena por fecha.
        builder.HasIndex(v => v.YouTubeId).IsUnique();
        builder.HasIndex(v => v.PublishedAt);
    }
}

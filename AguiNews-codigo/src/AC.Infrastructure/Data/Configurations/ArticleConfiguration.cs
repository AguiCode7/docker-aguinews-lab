using AC.Core.Entities;
using AC.Core.Modules.Articles.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.Data.Configurations;

// Mapeo de la entidad Article a la tabla "articles" (snake_case en PostgreSQL).
public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.SummaryId).HasColumnName("summary_id");
        builder.Property(a => a.Position).HasColumnName("position");
        builder.Property(a => a.Title).HasColumnName("title")
            .HasMaxLength(CreateArticleValidator.TitleMaxLength).IsRequired();
        builder.Property(a => a.Url).HasColumnName("url")
            .HasMaxLength(CreateArticleValidator.UrlMaxLength).IsRequired();
        builder.Property(a => a.SourceName).HasColumnName("source_name")
            .HasMaxLength(CreateArticleValidator.UrlMaxLength).IsRequired();
        builder.Property(a => a.Topic).HasColumnName("topic")
            .HasMaxLength(CreateArticleValidator.TopicMaxLength).IsRequired();
        builder.Property(a => a.Headline).HasColumnName("headline")
            .HasMaxLength(CreateArticleValidator.HeadlineMaxLength).IsRequired();
        builder.Property(a => a.Lead).HasColumnName("lead")
            .HasMaxLength(CreateArticleValidator.LeadMaxLength).IsRequired();
        builder.Property(a => a.Reading).HasColumnName("reading")
            .HasMaxLength(CreateArticleValidator.ReadingMaxLength).IsRequired();
        // PostgreSQL guarda la lista como text[] (sin tabla aparte).
        builder.Property(a => a.KeyPoints).HasColumnName("key_points");
        builder.Property(a => a.ReadingMinutes).HasColumnName("reading_minutes");
        builder.Property(a => a.CreatedAt).HasColumnName("created_at");

        // Si se borra una edición, se borran sus noticias.
        builder.HasOne(a => a.Summary)
            .WithMany(s => s.Articles)
            .HasForeignKey(a => a.SummaryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Las noticias de una edición se piden siempre en orden.
        builder.HasIndex(a => new { a.SummaryId, a.Position }).IsUnique();
    }
}

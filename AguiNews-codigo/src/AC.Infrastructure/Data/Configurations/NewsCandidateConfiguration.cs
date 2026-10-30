using AC.Core.Entities;
using AC.Core.Modules.Candidates.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.Data.Configurations;

// Mapeo de la entidad NewsCandidate a la tabla "news_candidates" (snake_case en PostgreSQL).
public class NewsCandidateConfiguration : IEntityTypeConfiguration<NewsCandidate>
{
    public void Configure(EntityTypeBuilder<NewsCandidate> builder)
    {
        builder.ToTable("news_candidates");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.Title).HasColumnName("title")
            .HasMaxLength(CreateCandidateValidator.TitleMaxLength).IsRequired();
        builder.Property(c => c.Url).HasColumnName("url")
            .HasMaxLength(CreateCandidateValidator.UrlMaxLength).IsRequired();
        builder.Property(c => c.Source).HasColumnName("source")
            .HasMaxLength(CreateCandidateValidator.SourceMaxLength).IsRequired();
        // PostgreSQL guarda la lista como text[] (sin tabla aparte).
        builder.Property(c => c.Sources).HasColumnName("sources");
        builder.Property(c => c.Score).HasColumnName("score");
        builder.Property(c => c.Selected).HasColumnName("selected");
        builder.Property(c => c.PublishedAt).HasColumnName("published_at");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at");

        // El ranking semanal filtra por fecha.
        builder.HasIndex(c => c.CreatedAt);
    }
}

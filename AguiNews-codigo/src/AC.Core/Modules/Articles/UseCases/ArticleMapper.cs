using AC.Core.Entities;
using AC.Core.Modules.Articles.DTOs;

namespace AC.Core.Modules.Articles.UseCases;

// Conversión entre la entidad y los DTOs de noticias (un solo lugar para no repetirla).
public static class ArticleMapper
{
    public static Article ToEntity(
        CreateArticleDto dto, Guid summaryId, int position, DateTime createdAt) =>
        new()
        {
            Id = Guid.NewGuid(),
            SummaryId = summaryId,
            Position = position,
            Title = dto.Title.Trim(),
            Url = dto.Url.Trim(),
            SourceName = GetSourceName(dto.Url),
            Topic = dto.Topic.Trim(),
            Headline = dto.Headline.Trim(),
            Lead = dto.Lead.Trim(),
            Reading = dto.Reading.Trim(),
            KeyPoints = (dto.KeyPoints ?? [])
                .Select(p => p.Trim())
                .Where(p => p.Length > 0)
                .ToList(),
            ReadingMinutes = dto.ReadingMinutes,
            CreatedAt = createdAt
        };

    public static ArticleDto ToDto(Article a) =>
        new(a.Id, a.SummaryId, a.Position, a.Title, a.Url, a.SourceName,
            a.Topic, a.Headline, a.Lead, a.Reading, a.KeyPoints,
            a.ReadingMinutes, a.CreatedAt);

    public static ArticleItemDto ToItemDto(Article a) =>
        new(a.Id, a.Position, a.Title, a.Url, a.SourceName, a.Topic, a.Headline,
            a.ReadingMinutes);

    // El nombre de la fuente sale del dominio del enlace, sin "www.".
    private static string GetSourceName(string url)
    {
        var host = new Uri(url.Trim()).Host;
        return host.StartsWith("www.", StringComparison.OrdinalIgnoreCase)
            ? host[4..]
            : host;
    }
}

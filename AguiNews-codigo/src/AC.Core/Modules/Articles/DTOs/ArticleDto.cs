namespace AC.Core.Modules.Articles.DTOs;

// Noticia completa para el modo lectura.
public record ArticleDto(
    Guid Id,
    Guid SummaryId,
    int Position,
    string Title,
    string Url,
    string SourceName,
    string Topic,
    string Headline,
    string Lead,
    string Reading,
    IReadOnlyList<string> KeyPoints,
    int ReadingMinutes,
    DateTime CreatedAt);

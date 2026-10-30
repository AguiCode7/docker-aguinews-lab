namespace AC.Core.Modules.Articles.DTOs;

// Noticia que envía n8n dentro de una edición.
public record CreateArticleDto(
    string Title,
    string Url,
    string Topic,
    string Headline,
    string Lead,
    string Reading,
    IReadOnlyList<string>? KeyPoints,
    int ReadingMinutes);

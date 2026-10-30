namespace AC.Core.Modules.Articles.DTOs;

// Versión ligera de la noticia para listas (portada y archivo): sin la lectura.
// Url permite enlazar la noticia con su Short del canal.
public record ArticleItemDto(
    Guid Id,
    int Position,
    string Title,
    string Url,
    string SourceName,
    string Topic,
    string Headline,
    int ReadingMinutes);

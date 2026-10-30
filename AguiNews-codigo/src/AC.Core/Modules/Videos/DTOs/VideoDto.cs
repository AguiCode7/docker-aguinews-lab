using AC.Core.Entities;

namespace AC.Core.Modules.Videos.DTOs;

// Video listo para el sitio: con enlace a YouTube y miniatura ya armados.
public record VideoDto(
    string YouTubeId,
    string Title,
    VideoKind Kind,
    string Url,
    string ThumbnailUrl,
    string? NewsUrl,
    DateTime PublishedAt);

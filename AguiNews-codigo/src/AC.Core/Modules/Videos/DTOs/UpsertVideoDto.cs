using AC.Core.Entities;

namespace AC.Core.Modules.Videos.DTOs;

// Video que n8n lee del feed público del canal.
public record UpsertVideoDto(
    string YouTubeId,
    string Title,
    VideoKind Kind,
    DateTime PublishedAt);

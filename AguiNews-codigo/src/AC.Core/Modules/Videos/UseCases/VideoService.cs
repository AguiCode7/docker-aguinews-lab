using AC.Core.Entities;
using AC.Core.Modules.Videos.DTOs;
using AC.Core.Modules.Videos.Ports;
using AC.Core.Shared.Models;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AC.Core.Modules.Videos.UseCases;

// Caso de uso: guarda los videos del feed del canal y los entrega listos para el sitio.
public class VideoService(
    IVideoRepository repository,
    IValidator<UpsertVideosDto> upsertValidator,
    IValidator<LinkNewsDto> linkValidator,
    IOptions<AppConfig> config) : IVideoService
{
    public async Task<IReadOnlyList<VideoDto>> GetLatestAsync(
        VideoKind? kind, int count, CancellationToken ct)
    {
        // El tope de resultados viene de la configuración.
        var limit = Math.Clamp(count, 1, config.Value.MaxVideosPerQuery);
        var videos = await repository.GetLatestAsync(kind, limit, ct);
        return videos.Select(ToDto).ToList();
    }

    public async Task<IReadOnlyList<VideoDto>> GetWithNewsAsync(CancellationToken ct)
    {
        var videos = await repository.GetWithNewsAsync(ct);
        return videos.Select(ToDto).ToList();
    }

    public async Task<VideosSavedDto> UpsertManyAsync(
        UpsertVideosDto dto, CancellationToken ct)
    {
        await upsertValidator.ValidateAndThrowAsync(dto, ct);

        // El feed puede repetir un video: se queda la última aparición.
        var incoming = dto.Videos
            .GroupBy(v => v.YouTubeId.Trim())
            .ToDictionary(g => g.Key, g => g.Last());
        var existing = (await repository.GetByYouTubeIdsAsync(incoming.Keys, ct))
            .ToDictionary(v => v.YouTubeId);

        var now = DateTime.UtcNow; // siempre en UTC
        var created = new List<ChannelVideo>();
        var updated = 0;

        foreach (var (youTubeId, video) in incoming)
        {
            if (existing.TryGetValue(youTubeId, out var current))
            {
                // Solo se actualiza lo que puede cambiar en YouTube (el título).
                // El enlace a la noticia (NewsUrl) no se toca desde el feed.
                if (current.Title != video.Title.Trim() || current.Kind != video.Kind)
                {
                    current.Title = video.Title.Trim();
                    current.Kind = video.Kind;
                    current.UpdatedAt = now;
                    updated++;
                }
                continue;
            }

            created.Add(new ChannelVideo
            {
                Id = Guid.NewGuid(),
                YouTubeId = youTubeId,
                Title = video.Title.Trim(),
                Kind = video.Kind,
                PublishedAt = video.PublishedAt.ToUniversalTime(),
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        repository.AddRange(created);
        await repository.SaveChangesAsync(ct);
        return new VideosSavedDto(created.Count, updated);
    }

    public async Task<VideoDto?> LinkNewsAsync(
        string youTubeId, LinkNewsDto dto, CancellationToken ct)
    {
        await linkValidator.ValidateAndThrowAsync(dto, ct);

        var video = (await repository.GetByYouTubeIdsAsync([youTubeId.Trim()], ct))
            .FirstOrDefault();
        if (video is null) return null;

        video.NewsUrl = string.IsNullOrWhiteSpace(dto.NewsUrl) ? null : dto.NewsUrl.Trim();
        video.UpdatedAt = DateTime.UtcNow;
        await repository.SaveChangesAsync(ct);
        return ToDto(video);
    }

    // Arma el enlace y la miniatura con las plantillas de la configuración.
    private VideoDto ToDto(ChannelVideo video)
    {
        var settings = config.Value;
        var urlTemplate = video.Kind == VideoKind.Short
            ? settings.YouTubeShortUrlTemplate
            : settings.YouTubeVideoUrlTemplate;

        return new VideoDto(
            video.YouTubeId,
            video.Title,
            video.Kind,
            string.Format(urlTemplate, video.YouTubeId),
            string.Format(settings.YouTubeThumbnailUrlTemplate, video.YouTubeId),
            video.NewsUrl,
            video.PublishedAt);
    }
}

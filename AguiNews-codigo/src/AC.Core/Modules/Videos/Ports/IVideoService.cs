using AC.Core.Entities;
using AC.Core.Modules.Videos.DTOs;

namespace AC.Core.Modules.Videos.Ports;

// Puerto de entrada: lo que el módulo de videos ofrece a la capa API.
public interface IVideoService
{
    Task<IReadOnlyList<VideoDto>> GetLatestAsync(VideoKind? kind, int count, CancellationToken ct);
    Task<IReadOnlyList<VideoDto>> GetWithNewsAsync(CancellationToken ct);
    Task<VideosSavedDto> UpsertManyAsync(UpsertVideosDto dto, CancellationToken ct);
    Task<VideoDto?> LinkNewsAsync(string youTubeId, LinkNewsDto dto, CancellationToken ct);
}

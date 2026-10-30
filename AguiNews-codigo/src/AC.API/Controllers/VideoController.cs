using AC.Core.Entities;
using AC.Core.Modules.Videos.DTOs;
using AC.Core.Modules.Videos.Ports;
using Microsoft.AspNetCore.Mvc;

namespace AC.API.Controllers;

// Endpoints de videos del canal: n8n guarda el feed, el sitio los muestra
// y la tarea de Shorts enlaza cada Short con su noticia.
[ApiController]
[Route("api/videos")]
public class VideoController(IVideoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VideoDto>>> GetLatest(
        [FromQuery] VideoKind? kind, [FromQuery] int count = 10, CancellationToken ct = default)
        => Ok(await service.GetLatestAsync(kind, count, ct));

    [HttpGet("with-news")]
    public async Task<ActionResult<IReadOnlyList<VideoDto>>> GetWithNews(CancellationToken ct)
        => Ok(await service.GetWithNewsAsync(ct));

    [HttpPost]
    public async Task<ActionResult<VideosSavedDto>> Upsert(
        UpsertVideosDto dto, CancellationToken ct)
        => Ok(await service.UpsertManyAsync(dto, ct));

    [HttpPut("{youTubeId}/news")]
    public async Task<ActionResult<VideoDto>> LinkNews(
        string youTubeId, LinkNewsDto dto, CancellationToken ct)
    {
        var video = await service.LinkNewsAsync(youTubeId, dto, ct);
        return video is null ? NotFound() : Ok(video);
    }
}

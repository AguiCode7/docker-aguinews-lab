using AC.Core.Modules.Candidates.DTOs;
using AC.Core.Modules.Candidates.Ports;
using Microsoft.AspNetCore.Mvc;

namespace AC.API.Controllers;

// Endpoints de candidatas: n8n guarda las del día y el video semanal pide el ranking.
[ApiController]
[Route("api/candidates")]
public class CandidateController(ICandidateService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CandidatesCreatedDto>> Create(
        CreateCandidatesDto dto, CancellationToken ct)
        => StatusCode(StatusCodes.Status201Created, await service.CreateManyAsync(dto, ct));

    [HttpGet("top")]
    public async Task<ActionResult<IReadOnlyList<CandidateDto>>> GetTop(
        [FromQuery] int days = 7, [FromQuery] int count = 15, CancellationToken ct = default)
        => Ok(await service.GetTopAsync(days, count, ct));
}

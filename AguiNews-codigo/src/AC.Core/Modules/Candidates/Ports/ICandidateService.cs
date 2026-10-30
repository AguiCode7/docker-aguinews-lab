using AC.Core.Modules.Candidates.DTOs;

namespace AC.Core.Modules.Candidates.Ports;

// Puerto de entrada: lo que el módulo de candidatas ofrece a la capa API.
public interface ICandidateService
{
    Task<CandidatesCreatedDto> CreateManyAsync(CreateCandidatesDto dto, CancellationToken ct);
    Task<IReadOnlyList<CandidateDto>> GetTopAsync(int days, int count, CancellationToken ct);
}

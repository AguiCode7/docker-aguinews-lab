namespace AC.Core.Modules.Candidates.DTOs;

// Lote de candidatas de una ejecución del flujo diario.
public record CreateCandidatesDto(IReadOnlyList<CreateCandidateDto> Candidates);

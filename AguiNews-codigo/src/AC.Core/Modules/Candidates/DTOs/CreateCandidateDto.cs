namespace AC.Core.Modules.Candidates.DTOs;

// Candidata que envía n8n: puntaje de la IA y si entró en la edición del día.
public record CreateCandidateDto(
    string Title,
    string Url,
    string Source,
    IReadOnlyList<string>? Sources,
    int Score,
    bool Selected,
    DateTime? PublishedAt);

namespace AC.Core.Modules.Candidates.DTOs;

// Candidata para el ranking semanal. TimesSeen: en cuántos días distintos apareció.
public record CandidateDto(
    string Title,
    string Url,
    string Source,
    IReadOnlyList<string> Sources,
    int Score,
    bool Selected,
    int TimesSeen,
    DateTime? PublishedAt,
    DateTime FirstSeenAt);

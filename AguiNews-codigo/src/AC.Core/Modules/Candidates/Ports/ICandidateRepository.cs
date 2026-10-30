using AC.Core.Entities;

namespace AC.Core.Modules.Candidates.Ports;

// Puerto de salida: cómo se guardan y leen las candidatas, sin decir con qué tecnología.
public interface ICandidateRepository
{
    Task AddRangeAsync(IReadOnlyList<NewsCandidate> candidates, CancellationToken ct);
    Task<IReadOnlyList<NewsCandidate>> GetSinceAsync(DateTime since, CancellationToken ct);
}

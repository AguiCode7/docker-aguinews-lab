using AC.Core.Entities;
using AC.Core.Modules.Candidates.Ports;
using AC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.Repositories;

// Adaptador: implementa el puerto ICandidateRepository con EF Core.
public class CandidateRepository(AguiNewsDbContext db) : ICandidateRepository
{
    public async Task AddRangeAsync(
        IReadOnlyList<NewsCandidate> candidates, CancellationToken ct)
    {
        db.NewsCandidates.AddRange(candidates);
        await db.SaveChangesAsync(ct);
    }

    // Solo lectura: AsNoTracking evita el seguimiento de cambios.
    public async Task<IReadOnlyList<NewsCandidate>> GetSinceAsync(
        DateTime since, CancellationToken ct) =>
        await db.NewsCandidates.AsNoTracking()
            .Where(c => c.CreatedAt >= since)
            .ToListAsync(ct);
}

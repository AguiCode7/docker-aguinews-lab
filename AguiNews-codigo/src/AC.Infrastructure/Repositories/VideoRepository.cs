using AC.Core.Entities;
using AC.Core.Modules.Videos.Ports;
using AC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.Repositories;

// Adaptador: implementa el puerto IVideoRepository con EF Core.
public class VideoRepository(AguiNewsDbContext db) : IVideoRepository
{
    // Solo lectura: AsNoTracking evita el seguimiento de cambios.
    public async Task<IReadOnlyList<ChannelVideo>> GetLatestAsync(
        VideoKind? kind, int count, CancellationToken ct) =>
        await db.ChannelVideos.AsNoTracking()
            .Where(v => kind == null || v.Kind == kind)
            .OrderByDescending(v => v.PublishedAt)
            .Take(count)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<ChannelVideo>> GetWithNewsAsync(CancellationToken ct) =>
        await db.ChannelVideos.AsNoTracking()
            .Where(v => v.NewsUrl != null)
            .OrderByDescending(v => v.PublishedAt)
            .ToListAsync(ct);

    // Con seguimiento: el caso de uso modifica lo que encuentra.
    public async Task<IReadOnlyList<ChannelVideo>> GetByYouTubeIdsAsync(
        IReadOnlyCollection<string> youTubeIds, CancellationToken ct) =>
        await db.ChannelVideos
            .Where(v => youTubeIds.Contains(v.YouTubeId))
            .ToListAsync(ct);

    public void AddRange(IEnumerable<ChannelVideo> videos) => db.ChannelVideos.AddRange(videos);

    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}

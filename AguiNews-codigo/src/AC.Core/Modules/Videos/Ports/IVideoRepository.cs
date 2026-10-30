using AC.Core.Entities;

namespace AC.Core.Modules.Videos.Ports;

// Puerto de salida: cómo se guardan y leen los videos del canal.
public interface IVideoRepository
{
    Task<IReadOnlyList<ChannelVideo>> GetLatestAsync(VideoKind? kind, int count, CancellationToken ct);
    Task<IReadOnlyList<ChannelVideo>> GetWithNewsAsync(CancellationToken ct);
    Task<IReadOnlyList<ChannelVideo>> GetByYouTubeIdsAsync(IReadOnlyCollection<string> youTubeIds, CancellationToken ct);
    void AddRange(IEnumerable<ChannelVideo> videos);
    Task SaveChangesAsync(CancellationToken ct);
}

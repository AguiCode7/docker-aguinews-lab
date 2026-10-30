namespace AC.Core.Entities;

/// <summary>
/// Video o Short del canal de YouTube que se muestra en el sitio.
/// NewsUrl enlaza el Short con la noticia de la que salió (si la hay).
/// </summary>
public class ChannelVideo
{
    public Guid Id { get; set; }
    public string YouTubeId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public VideoKind Kind { get; set; }
    public string? NewsUrl { get; set; }
    public DateTime PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>Tipo de publicación del canal.</summary>
public enum VideoKind
{
    Video,
    Short
}

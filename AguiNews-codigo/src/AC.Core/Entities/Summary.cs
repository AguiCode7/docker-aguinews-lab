namespace AC.Core.Entities;

/// <summary>
/// Edición de noticias generada por la IA y enviada desde n8n.
/// </summary>
public class Summary
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public int NewsCount { get; set; }
    public DateTime CreatedAt { get; set; }

    // Noticias de la edición, en el orden en que llegaron (desde el video 4).
    public List<Article> Articles { get; set; } = [];
}

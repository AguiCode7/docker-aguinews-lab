namespace AC.Core.Entities;

/// <summary>
/// Noticia de una edición: título, enlace a la fuente y lectura en español generada por la IA.
/// </summary>
public class Article
{
    public Guid Id { get; set; }
    public Guid SummaryId { get; set; }
    public int Position { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string SourceName { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Headline { get; set; } = string.Empty;
    public string Lead { get; set; } = string.Empty;
    public string Reading { get; set; } = string.Empty;
    public List<string> KeyPoints { get; set; } = [];
    public int ReadingMinutes { get; set; }
    public DateTime CreatedAt { get; set; }

    public Summary? Summary { get; set; }
}

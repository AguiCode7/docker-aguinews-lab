namespace AC.Core.Entities;

/// <summary>
/// Noticia candidata del flujo diario con su puntaje de la IA.
/// Se guardan todas (elegidas o no) para armar el resumen de la semana.
/// </summary>
public class NewsCandidate
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public List<string> Sources { get; set; } = [];
    public int Score { get; set; }
    public bool Selected { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

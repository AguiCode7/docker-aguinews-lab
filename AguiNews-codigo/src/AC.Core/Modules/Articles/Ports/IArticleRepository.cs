using AC.Core.Entities;

namespace AC.Core.Modules.Articles.Ports;

// Puerto de salida: cómo se leen las noticias, sin decir con qué tecnología.
public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken ct);
}

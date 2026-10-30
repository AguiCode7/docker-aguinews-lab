using AC.Core.Modules.Articles.DTOs;

namespace AC.Core.Modules.Articles.Ports;

// Puerto de entrada: lo que el módulo de noticias ofrece a la capa API.
public interface IArticleService
{
    Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken ct);
}

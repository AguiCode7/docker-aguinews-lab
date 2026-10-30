using AC.Core.Modules.Articles.DTOs;
using AC.Core.Modules.Articles.Ports;

namespace AC.Core.Modules.Articles.UseCases;

// Caso de uso: consulta de una noticia para el modo lectura.
public class ArticleService(IArticleRepository repository) : IArticleService
{
    public async Task<ArticleDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var article = await repository.GetByIdAsync(id, ct);
        return article is null ? null : ArticleMapper.ToDto(article);
    }
}

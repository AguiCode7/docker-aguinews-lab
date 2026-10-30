using AC.Core.Modules.Articles.DTOs;
using AC.Core.Modules.Articles.Ports;
using Microsoft.AspNetCore.Mvc;

namespace AC.API.Controllers;

// Endpoints de noticias: el modo lectura pide una noticia por su id.
[ApiController]
[Route("api/articles")]
public class ArticleController(IArticleService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ArticleDto>> GetById(
        Guid id, CancellationToken ct)
    {
        var article = await service.GetByIdAsync(id, ct);
        return article is null ? NotFound() : Ok(article);
    }
}

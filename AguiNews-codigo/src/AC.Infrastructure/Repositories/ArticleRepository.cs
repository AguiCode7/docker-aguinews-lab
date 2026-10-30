using AC.Core.Entities;
using AC.Core.Modules.Articles.Ports;
using AC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.Repositories;

// Adaptador: implementa el puerto IArticleRepository con EF Core.
public class ArticleRepository(AguiNewsDbContext db) : IArticleRepository
{
    public Task<Article?> GetByIdAsync(Guid id, CancellationToken ct) =>
        db.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, ct);
}

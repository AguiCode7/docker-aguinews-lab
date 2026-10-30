using AC.Core.Modules.Articles.DTOs;

namespace AC.Core.Modules.Summaries.DTOs;

// Datos que envía n8n para registrar una edición.
// Articles es opcional: los flujos del video 2 solo mandan el resumen.
public record CreateSummaryDto(
    string Content,
    string Source,
    int NewsCount,
    IReadOnlyList<CreateArticleDto>? Articles = null);

using AC.Core.Entities;
using AC.Core.Modules.Articles.UseCases;
using AC.Core.Modules.Summaries.DTOs;
using AC.Core.Modules.Summaries.Ports;
using AC.Core.Shared.Models;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AC.Core.Modules.Summaries.UseCases;

// Caso de uso: valida, crea y consulta ediciones con sus noticias.
public class SummaryService(
    ISummaryRepository repository,
    IValidator<CreateSummaryDto> validator,
    IOptions<AppConfig> config) : ISummaryService
{
    public async Task<SummaryDto> CreateAsync(
        CreateSummaryDto dto, CancellationToken ct)
    {
        // Si los datos no son válidos, lanza ValidationException (API: 400).
        await validator.ValidateAndThrowAsync(dto, ct);

        var createdAt = DateTime.UtcNow; // siempre en UTC
        var summary = new Summary
        {
            Id = Guid.NewGuid(),
            Content = dto.Content.Trim(),
            Source = dto.Source.Trim(),
            NewsCount = dto.NewsCount,
            CreatedAt = createdAt
        };

        // Las noticias se guardan junto con la edición, en el orden recibido (1, 2, 3…).
        summary.Articles = (dto.Articles ?? [])
            .Select((a, index) =>
                ArticleMapper.ToEntity(a, summary.Id, index + 1, createdAt))
            .ToList();

        await repository.AddAsync(summary, ct);
        return ToDto(summary);
    }

    public async Task<SummaryDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var summary = await repository.GetByIdAsync(id, ct);
        return summary is null ? null : ToDto(summary);
    }

    public async Task<IReadOnlyList<SummaryDto>> GetLatestAsync(
        int count, CancellationToken ct)
    {
        // El límite máximo viene de la configuración, no del código.
        var limit = Math.Clamp(count, 1, config.Value.MaxSummariesPerQuery);
        var summaries = await repository.GetLatestAsync(limit, ct);
        return summaries.Select(ToDto).ToList();
    }

    private static SummaryDto ToDto(Summary s) =>
        new(s.Id, s.Content, s.Source, s.NewsCount, s.CreatedAt,
            s.Articles.OrderBy(a => a.Position)
                .Select(ArticleMapper.ToItemDto)
                .ToList());
}

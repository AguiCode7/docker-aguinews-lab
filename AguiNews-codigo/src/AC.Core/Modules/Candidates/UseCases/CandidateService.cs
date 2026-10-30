using AC.Core.Entities;
using AC.Core.Modules.Candidates.DTOs;
using AC.Core.Modules.Candidates.Ports;
using AC.Core.Shared.Models;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AC.Core.Modules.Candidates.UseCases;

// Caso de uso: guarda las candidatas del día y arma el ranking de la semana.
public class CandidateService(
    ICandidateRepository repository,
    IValidator<CreateCandidatesDto> validator,
    IOptions<AppConfig> config) : ICandidateService
{
    public async Task<CandidatesCreatedDto> CreateManyAsync(
        CreateCandidatesDto dto, CancellationToken ct)
    {
        // Si los datos no son válidos, lanza ValidationException (API: 400).
        await validator.ValidateAndThrowAsync(dto, ct);

        var createdAt = DateTime.UtcNow; // siempre en UTC
        var candidates = dto.Candidates.Select(c => new NewsCandidate
        {
            Id = Guid.NewGuid(),
            Title = c.Title.Trim(),
            Url = c.Url.Trim(),
            Source = c.Source.Trim(),
            Sources = (c.Sources ?? [c.Source])
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Distinct()
                .ToList(),
            Score = c.Score,
            Selected = c.Selected,
            PublishedAt = c.PublishedAt?.ToUniversalTime(),
            CreatedAt = createdAt
        }).ToList();

        await repository.AddRangeAsync(candidates, ct);
        return new CandidatesCreatedDto(candidates.Count);
    }

    public async Task<IReadOnlyList<CandidateDto>> GetTopAsync(
        int days, int count, CancellationToken ct)
    {
        // Los límites vienen de la configuración, no del código.
        var window = Math.Clamp(days, 1, config.Value.MaxCandidateDays);
        var limit = Math.Clamp(count, 1, config.Value.MaxCandidatesPerQuery);
        var since = DateTime.UtcNow.AddDays(-window);
        var rows = await repository.GetSinceAsync(since, ct);

        // Una misma noticia puede salir varios días: se agrupa por enlace,
        // se queda el mejor puntaje y se suman fuentes y días en que apareció.
        return rows
            .GroupBy(c => NormalizeUrl(c.Url))
            .Select(g =>
            {
                var best = g.OrderByDescending(c => c.Score).First();
                return new CandidateDto(
                    best.Title,
                    best.Url,
                    best.Source,
                    g.SelectMany(c => c.Sources).Distinct().ToList(),
                    best.Score,
                    g.Any(c => c.Selected),
                    g.Select(c => c.CreatedAt.Date).Distinct().Count(),
                    best.PublishedAt,
                    g.Min(c => c.CreatedAt));
            })
            .OrderByDescending(c => c.Score)
            .ThenByDescending(c => c.Sources.Count)
            .ThenByDescending(c => c.TimesSeen)
            .Take(limit)
            .ToList();
    }

    // Enlace comparable: sin esquema, sin "www.", sin barra final ni parámetros.
    private static string NormalizeUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)) return url;
        var host = uri.Host.StartsWith("www.", StringComparison.OrdinalIgnoreCase)
            ? uri.Host[4..]
            : uri.Host;
        return $"{host}{uri.AbsolutePath.TrimEnd('/')}".ToLowerInvariant();
    }
}

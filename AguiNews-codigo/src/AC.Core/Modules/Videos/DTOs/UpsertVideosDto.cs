namespace AC.Core.Modules.Videos.DTOs;

// Lote de videos de una lectura del feed.
public record UpsertVideosDto(IReadOnlyList<UpsertVideoDto> Videos);

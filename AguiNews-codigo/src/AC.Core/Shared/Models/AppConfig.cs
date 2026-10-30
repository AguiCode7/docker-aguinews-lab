namespace AC.Core.Shared.Models;

// Configuración tipada de la aplicación (Options pattern).
// Los valores se leen de appsettings, user-secrets o variables de entorno.
public class AppConfig
{
    public const string SectionName = "AppConfig";

    public string ApiKey { get; set; } = string.Empty;
    public int MaxSummariesPerQuery { get; set; } = 50;

    // Ranking de candidatas: días hacia atrás y cantidad máxima por consulta.
    public int MaxCandidateDays { get; set; } = 31;
    public int MaxCandidatesPerQuery { get; set; } = 50;

    // Videos del canal: tope por consulta y plantillas de enlaces ({0} = id del video).
    public int MaxVideosPerQuery { get; set; } = 50;
    public string YouTubeVideoUrlTemplate { get; set; } = string.Empty;
    public string YouTubeShortUrlTemplate { get; set; } = string.Empty;
    public string YouTubeThumbnailUrlTemplate { get; set; } = string.Empty;

    // Orígenes que pueden leer la API desde el navegador (CORS).
    public string[] AllowedOrigins { get; set; } = [];
}

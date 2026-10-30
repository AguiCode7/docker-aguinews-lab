using AC.API.Extensions;
using AC.Core;
using AC.Core.Shared.Models;
using AC.Infrastructure;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración tipada y capas
// Fail fast: sin las plantillas de enlaces de YouTube la app no arranca.
builder.Services.AddOptions<AppConfig>()
    .Bind(builder.Configuration.GetSection(AppConfig.SectionName))
    .Validate(c => !string.IsNullOrWhiteSpace(c.YouTubeVideoUrlTemplate)
                && !string.IsNullOrWhiteSpace(c.YouTubeShortUrlTemplate)
                && !string.IsNullOrWhiteSpace(c.YouTubeThumbnailUrlTemplate),
        "Missing YouTube URL templates in AppConfig.")
    .ValidateOnStart();
builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDashboardCors(builder.Configuration);
// Los enums viajan como texto ("Video", "Short") en el JSON.
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Documentación interactiva en /scalar
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseCors(CorsExtensions.DashboardPolicy);
app.UseMiddleware<ApiKeyAuthorizer>();
app.MapControllers();

app.Run();

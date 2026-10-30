using AC.Core.Modules.Articles.Ports;
using AC.Core.Modules.Articles.UseCases;
using AC.Core.Modules.Candidates.Ports;
using AC.Core.Modules.Candidates.UseCases;
using AC.Core.Modules.Summaries.Ports;
using AC.Core.Modules.Summaries.UseCases;
using AC.Core.Modules.Summaries.Validators;
using AC.Core.Modules.Videos.Ports;
using AC.Core.Modules.Videos.UseCases;
using System.Globalization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AC.Core;

// Registro de dependencias de la capa Core.
public static class Startup
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // Mensajes de validación siempre en inglés.
        ValidatorOptions.Global.LanguageManager.Culture =
            CultureInfo.InvariantCulture;

        services.AddValidatorsFromAssemblyContaining<CreateSummaryValidator>();
        services.AddScoped<ISummaryService, SummaryService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IVideoService, VideoService>();
        return services;
    }
}

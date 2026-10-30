using AC.Core.Modules.Articles.Ports;
using AC.Core.Modules.Candidates.Ports;
using AC.Core.Modules.Summaries.Ports;
using AC.Core.Modules.Videos.Ports;
using AC.Infrastructure.Data;
using AC.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AC.Infrastructure;

// Registro de dependencias de la capa Infrastructure.
public static class Startup
{
    public const string ConnectionStringName = "AguiNews";

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Fail fast: si falta la cadena de conexión, la app no arranca.
        var connectionString =
            configuration.GetConnectionString(ConnectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Missing connection string '{ConnectionStringName}'. " +
                "Set it with user-secrets or environment variables.");

        services.AddDbContext<AguiNewsDbContext>(
            options => options.UseNpgsql(connectionString));
        services.AddScoped<ISummaryRepository, SummaryRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        return services;
    }
}

using AC.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.Data;

public class AguiNewsDbContext(DbContextOptions<AguiNewsDbContext> options)
    : DbContext(options)
{
    public DbSet<Summary> Summaries => Set<Summary>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<NewsCandidate> NewsCandidates => Set<NewsCandidate>();
    public DbSet<ChannelVideo> ChannelVideos => Set<ChannelVideo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas las IEntityTypeConfiguration de este proyecto.
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AguiNewsDbContext).Assembly);
    }
}

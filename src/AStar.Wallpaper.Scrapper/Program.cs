using AStar.ASPNet.Extensions.ServiceCollectionExtensions;
using AStar.Infrastructure.Data;
using AStar.Logging.Extensions;
using AStar.Wallpaper.Scrapper.Components;

namespace AStar.Wallpaper.Scrapper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        _ = builder.CreateBootstrapLogger("astar-logging-settings.json")
                   .AddLogging("astar-logging-settings.json");
        _ = builder.Services.AddScoped(_ => new FilesContext(new() { Value = builder.Configuration.GetConnectionString("SqlServer")! }, new() { EnableLogging = false, IncludeSensitiveData = false, InMemory = false }));

        var app = builder.Build();

        _ = app.UseHttpsRedirection();

        _ = app.UseStaticFiles();
        _ = app.UseAntiforgery();

        _ = app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}

using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AStar.Wallpaper.Scrapper.Components.Pages;

public partial class Categories
{
    private IList<SearchCategory>? CategoriesList { get; set; }

    [Inject]
    public required FilesContext Context { get; set; }

    private string UpdateMessage { get; set; } = string.Empty;

    private async Task HandleValidSubmit()
    {
        _ = Context.SaveChanges();
        UpdateMessage = "The Categories have been updated";
        await Task.Delay(2000);
        UpdateMessage = string.Empty;
    }

    protected override async Task OnInitializedAsync()
    {
        var scrapeConfiguration = await Context.ScrapeConfiguration
                                                .Include(scrapeConfiguration => scrapeConfiguration.UserConfiguration)
                                                .Include(scrapeConfiguration => scrapeConfiguration.SearchConfiguration)
                                                .Include(c => c.SearchConfiguration.SearchCategories)
                                                .SingleAsync();

        CategoriesList = [.. scrapeConfiguration.SearchConfiguration.SearchCategories.OrderBy(sc=>sc.Order)];
    }
}

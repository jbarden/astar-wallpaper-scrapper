using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AStar.Wallpaper.Scrapper.Components.Pages;

public partial class Categories
{
    [Inject]
    public FilesContext Context { get; set; }

    private IList<SearchCategory>? CategoriesList { get; set; }

    private string UpdateMessage { get; set; } = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            var scrapeConfiguration = await Context.ScrapeConfiguration
                                                .Include(scrapeConfiguration => scrapeConfiguration.UserConfiguration)
                                                .Include(scrapeConfiguration => scrapeConfiguration.SearchConfiguration)
                                                .Include(c => c.SearchConfiguration.SearchCategories)
                                                .SingleAsync();

            CategoriesList = [.. scrapeConfiguration.SearchConfiguration.SearchCategories.OrderBy(sc => sc.Order)];

            StateHasChanged();
        }
    }

    private async Task HandleValidSubmit()
    {
        _ = Context.SaveChanges();
        UpdateMessage = "The Categories have been updated";
        await Task.Delay(2000);
        UpdateMessage = string.Empty;
    }
}

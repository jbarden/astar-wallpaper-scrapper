using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AStar.Wallpaper.Scrapper.Components.Pages;

public partial class Categories
{
    private IList<SearchCategory> CategoriesList { get; set; } = [];

    [Inject]
    public required FilesContext Context { get; set; }

    private bool Loading { get;  set; }

    private void HandleValidSubmit() => Context.SaveChanges();

    protected override async Task OnInitializedAsync()
    {
        if(Loading)
        {
            return;
        }

        try
        {
            Loading = true;
            var scrapeConfiguration =  Context.ScrapeConfiguration.Include(scrapeConfiguration => scrapeConfiguration.UserConfiguration)
                .Include(scrapeConfiguration => scrapeConfiguration.SearchConfiguration).Include(c => c.SearchConfiguration.SearchCategories).First();

            CategoriesList = scrapeConfiguration.SearchConfiguration.SearchCategories;
        }
        finally
        {
            Loading = false;
        }

        await base.OnInitializedAsync();
    }
}

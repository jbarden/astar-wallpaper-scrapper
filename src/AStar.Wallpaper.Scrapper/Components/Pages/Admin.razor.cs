using System.Runtime.CompilerServices;
using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AStar.Wallpaper.Scrapper.Components.Pages;

public partial class Admin
{
    private ScrapeConfiguration ScrapeConfiguration { get; set; } = new();

    [Inject]
    public required FilesContext Context { get;set; }

    public bool Loading { get; private set; }
    
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
            ScrapeConfiguration = await Context.ScrapeConfiguration.Include(scrapeConfiguration => scrapeConfiguration.UserConfiguration)
                .Include(scrapeConfiguration => scrapeConfiguration.SearchConfiguration)
                .Include(scrapeConfiguration => scrapeConfiguration.ScrapeDirectories).SingleAsync();
        }
        finally
        {
            Loading = false;
        }

        await base.OnInitializedAsync();
    }
}

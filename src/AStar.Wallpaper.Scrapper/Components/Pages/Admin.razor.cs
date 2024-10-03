using System.Runtime.CompilerServices;
using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AStar.Wallpaper.Scrapper.Components.Pages;

public partial class Admin
{
    [Inject]
    public required FilesContext Context { get; set; }

    private ScrapeConfiguration? ScrapeConfiguration { get; set; }

    private string UpdateMessage { get; set; } = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            ScrapeConfiguration = await Context.ScrapeConfiguration
                                                .Include(scrapeConfiguration => scrapeConfiguration.UserConfiguration)
                                                .Include(scrapeConfiguration => scrapeConfiguration.SearchConfiguration)
                                                .Include(scrapeConfiguration => scrapeConfiguration.ScrapeDirectories)
                                                .SingleAsync();
            StateHasChanged();
        }
    }

    private async Task HandleValidSubmit()
    {
        _ = Context.SaveChanges();
        UpdateMessage = "Admin settings have been updated";
        await Task.Delay(2000);
        UpdateMessage = string.Empty;
    }
}

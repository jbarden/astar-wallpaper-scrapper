using AStar.Infrastructure.Data;
using AStar.Infrastructure.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace AStar.Wallpaper.Scrapper.Components.Pages;

public partial class Weather
{
    private IList<SearchCategory>? CategoriesList { get; set; }
    [Inject]
    public required FilesContext Context { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // Simulate asynchronous loading to demonstrate streaming rendering
        await Task.Delay(500);

        var scrapeConfiguration =  Context.ScrapeConfiguration.Include(scrapeConfiguration => scrapeConfiguration.UserConfiguration)
            .Include(scrapeConfiguration => scrapeConfiguration.SearchConfiguration).Include(c => c.SearchConfiguration.SearchCategories).First();

        CategoriesList = scrapeConfiguration.SearchConfiguration.SearchCategories;
    }

    private void HandleValidSubmit() => Context.SaveChanges();

    private class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

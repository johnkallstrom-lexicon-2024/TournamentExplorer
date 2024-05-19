using Microsoft.EntityFrameworkCore;
using TournamentExplorer.Data;

namespace TournamentExplorer.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TournamentExplorerDbContext>();

                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();

                await InitializeDatabase.SeedAsync(context);
            }
        }
    }
}

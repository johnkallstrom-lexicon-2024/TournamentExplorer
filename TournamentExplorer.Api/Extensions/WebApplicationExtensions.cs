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
                await InitializeDatabase.SeedAsync(context);
            }
        }
    }
}

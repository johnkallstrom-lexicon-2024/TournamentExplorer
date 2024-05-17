using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TournamentExplorer.Data
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TournamentExplorerDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            return services;
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TournamentExplorer.Core.Contracts;
using TournamentExplorer.Core.Entities;
using TournamentExplorer.Data.Repositories;

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

            services.AddScoped<IRepository<Tournament>, TournamentRepository>();
            services.AddScoped<IRepository<Game>, GameRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

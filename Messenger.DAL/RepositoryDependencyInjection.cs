using Messenger.DAL.Context;
using Messenger.DAL.Repositories;
using Messenger.DAL.Repositories.Interfaces;
using Messenger.DAL.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.DAL
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            return services;
        }
    }
}
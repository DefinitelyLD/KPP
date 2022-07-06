using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories;
using Messenger.DAL.Repositories.Interfaces;
using Messenger.DAL.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.DAL
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMessagesRepository, MessagesRepository>();
            services.AddTransient<IMessageImagesRepository, MessageImagesRepository>();
            services.AddTransient<IChatsRepository, ChatsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserAccountsRepository, UserAccountsRepository>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            return services;
        }
    }
}
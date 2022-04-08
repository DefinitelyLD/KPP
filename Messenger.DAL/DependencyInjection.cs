using Messenger.DAL.Context;
using Messenger.DAL.Entities;
using Messenger.DAL.Repositories;
using Messenger.DAL.Repositories.Interfaces;
using Messenger.DAL.UoW;
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
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IMessagesRepository, MessagesRepository>();
            services.AddTransient<IChatsRepository, ChatsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped(x => new DbContext(services.BuildServiceProvider().GetService<IMessengerDatabaseSettings>()));
            return services;
        }
    }
}
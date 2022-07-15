using Messenger.BLL.Managers;
using Messenger.BLL.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.BLL
{
    public static class ManagerDependencyInjection
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IMessageManager, MessageManager>();
            services.AddTransient<IChatroomManager, ChatroomManager>();
            services.AddTransient<IEmailManager, EmailManager>();

            return services;
        }
    }
}


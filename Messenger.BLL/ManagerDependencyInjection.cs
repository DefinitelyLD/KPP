using Messenger.BLL.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL
{
    public static class ManagerDependencyInjection
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IMessageManager, MessageManager>();

            return services;
        }
    }
}


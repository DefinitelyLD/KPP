using Messenger.Mapping.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Mapping
{
    public static class MapperDependencyInjection
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(MessageProfile));
            services.AddAutoMapper(typeof(ChatProfile));
            services.AddAutoMapper(typeof(UserImageProfile));

            return services;
        }
    }
}

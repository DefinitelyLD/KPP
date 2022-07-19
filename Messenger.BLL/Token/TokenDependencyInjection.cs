using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Messenger.BLL.Token
{
    public static class TokenDependencyInjection
    {
        public static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"])),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            return services;
        }
    }
}


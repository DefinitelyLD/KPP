using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Messenger.DAL;
using Messenger.Mapping;
using Messenger.BLL;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Messenger.DAL.Entities;
using Messenger.DAL.Context;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Messenger.BLL.Token;
using Messenger.BLL.Validators.UserAccounts;
using Messenger.WEB.SignalR;
using Messenger.Middleware;

namespace Messenger.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepository(Configuration);
            services.AddMappers();
            services.AddManagers();

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddJwtToken(Configuration);
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllers();
            services.AddSignalR();
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserAccountCreateModelValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messenger", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSignalRSwaggerGen();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger v1"));
            }

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireAuthorization();
                endpoints.MapHub<MessageHub>("/message");
            });
        }

    }

}
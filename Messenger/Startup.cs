using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Messenger.DAL;
using Messenger.Mapping;
using Messenger.BLL;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Messenger.DAL.Entities;
using Messenger.DAL.Context;
using Messenger.BLL.Token;
using Messenger.BLL.Validators.UserAccounts;
using Messenger.WEB.SignalR;
using Messenger.Middleware;
using Messenger.WEB.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;
using System.IO;
using System;
using Messenger.DAL.UoW;

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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200")
                .WithOrigins("https://localhost:44309")
                .WithOrigins("https://localhost:44389");
            }));

            services.AddMvcCore(options =>
            {
                var police = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(police));
            });
            services.AddRepository(Configuration);

            var builder = services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            });

            services.AddJwtToken(Configuration); // Authorization
            services.AddMappers();
            services.AddManagers();
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
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
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            ContextSeeder.SeedRoles(roleManager);
            ContextSeeder.SeedUsers(Configuration, userManager, unitOfWork);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });

            LoggerConfig.ConfigureLogger(Configuration);
        }
    }
}
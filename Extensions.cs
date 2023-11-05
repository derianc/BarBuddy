using BarBuddy.Data;
using BarBuddy.Repositories.Interfaces;
using BarBuddy.Repositories;
using BarBuddy.Services.Interfaces;
using BarBuddy.Services;
using Microsoft.OpenApi.Models;

namespace BarBuddy
{
    public static class Extensions
    {
        public static IServiceCollection AddReposAndServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(typeof(IRepository), typeof(Repository));

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IVenueRepository, VenueRepository>();
            builder.Services.AddScoped<IVenueSpendRepository, VenueSpendRepository>();
            builder.Services.AddScoped<IVenueCheckinRepository, VenueCheckinRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IVenueService, VenueService>();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                   .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
                   (
                        builder.Configuration["ConnectionStrings:MongoDb"],
                        builder.Configuration["ConnectionStrings:DbName"]
                   );

            return builder.Services;
        }

        public static IServiceCollection AddSwaggerGen(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
            });

            return builder.Services;
        }
    }
}

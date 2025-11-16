using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PantryCloud.HouseholdService.Application;
using PantryCloud.HouseholdService.Infrastructure.Persistence;
using PantryCloud.HouseholdService.Infrastructure.Services;

namespace PantryCloud.HouseholdService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HouseholdDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5072";
                options.Audience = "PantryCloud.WebClient";
                options.MetadataAddress = "https://localhost:5072/.well-known/openid-configuration";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:5072",
                    ValidateAudience = true,
                    ValidAudience = "PantryCloud.WebClient",
                    ValidateLifetime = true,
                };
            });

        services.AddAuthorization();

        services.AddHttpContextAccessor();
        
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IHouseholdService, Services.HouseholdService>();
        services.AddScoped<IInvitationService, InvitationService>();
        
        return services;
    }
}
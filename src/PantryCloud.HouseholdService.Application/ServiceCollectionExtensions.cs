using Microsoft.Extensions.DependencyInjection;
using PantryCloud.HouseholdService.Application.Queries;

namespace PantryCloud.HouseholdService.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCurrentHouseholdQuery).Assembly));

        //services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}
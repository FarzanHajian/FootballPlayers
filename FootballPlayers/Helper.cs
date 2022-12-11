using FluentValidation;
using FootballPlayers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers;

public static class Helper
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<TeamService>();
        services.AddScoped<PlayerService>();

        services.AddValidatorsFromAssemblyContaining<ServiceBase>();

        return services;
    }
}
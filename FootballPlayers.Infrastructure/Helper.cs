using Dapper.Contrib.Extensions;
using FootballPlayers.Abstractions.Persistence;
using FootballPlayers.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Infrastructure;

public static class Helper
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();

        // Overriding the default implementation of Dapper.Contrib which adds 's' to table names.
        SqlMapperExtensions.TableNameMapper = (type) => type.Name;

        return services;
    }
}
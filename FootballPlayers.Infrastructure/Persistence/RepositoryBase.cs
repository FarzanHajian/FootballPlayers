using Microsoft.Extensions.Configuration;

namespace FootballPlayers.Infrastructure.Persistence;

internal abstract class RepositoryBase
{
    protected readonly string connectionString;

    public RepositoryBase(IConfiguration config)
    {
        connectionString =
            config.GetConnectionString("main") ?? throw new InvalidOperationException("The connection string not found");
    }
}
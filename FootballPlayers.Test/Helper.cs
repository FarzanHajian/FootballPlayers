using FootballPlayers.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Test;

internal static class Helper
{
    public static IServiceProvider BootstrapServiceProvider(string configPath)
    {
        var services = new ServiceCollection();

        services.AddApplicationServices();
        services.AddInfrastructureServices();
        services.AddSingleton<IConfiguration>(GetIConfigurationRoot(configPath));

        return services.BuildServiceProvider();

        static IConfigurationRoot GetIConfigurationRoot(string configPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }

    public static void CopyFreshDatabase(string testDeploymentDirectory)
    {
        File.Copy(
            Path.Combine(Directory.GetParent(testDeploymentDirectory)!.Parent!.Parent!.FullName, "database.db"),
            Path.Combine(testDeploymentDirectory, "database.db"),
            true
        );
    }
}
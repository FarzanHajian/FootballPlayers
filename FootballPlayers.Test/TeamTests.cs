using FootballPlayers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Test;

[TestClass]
public class TeamTests
{
    private static IServiceProvider sp = null!;
    private static TeamService service = null!;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        sp = Helper.BootstrapServiceProvider(context.TestDeploymentDir);
        service = sp.GetRequiredService<TeamService>();

        Helper.CopyFreshDatabase(context.TestDeploymentDir);
    }

    [TestMethod]
    public void GetAllTeams()
    {
        var teams = service.GetAll();
        Assert.IsTrue(teams.Count() == 2, "Number of teams must be 2");
    }

    [TestMethod]
    public void GetAllInterMilanPlayers()
    {
        var players = service.GetAllPlayersByTeam(1);
        Assert.IsTrue(players.Count() == 5, "Number of players in Inter Milan must be 5");
    }
}
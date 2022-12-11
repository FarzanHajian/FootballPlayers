using FootballPlayers.Entities;
using FootballPlayers.Models;
using FootballPlayers.Services;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Frameworks;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FootballPlayers.Test;

[TestClass]
public class PlayerTests
{
    private static IServiceProvider sp = null!;
    private static PlayerService service = null!;
    private static TestContext context = null!;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        sp = Helper.BootstrapServiceProvider(context.TestDeploymentDir);
        service = sp.GetRequiredService<PlayerService>();
        PlayerTests.context = context;

        Helper.CopyFreshDatabase(context.TestDeploymentDir);
    }

    [TestMethod]
    public void GetAllPlayers()
    {
        Helper.CopyFreshDatabase(context.TestDeploymentDir);

        var players = service.GetAll();
        Assert.IsTrue(players.Count() == 10, "Number of players must be 10");
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(7)]
    [DataRow(11)]
    public void GetPlayerById(int id)
    {
        Helper.CopyFreshDatabase(context.TestDeploymentDir);

        var player = service.GetById(id);
        if (id == 5)
        {
            Assert.IsNotNull(player, "Lautaro Martinez not found");
            Assert.IsTrue(
                player.Name == "Lautaro Martinez" && player.Age == 25 && player.Height == 1.74f && player.TeamId == 1,
                "Failed to obtain Lautaro Martinez's data"
            );
        }
        else if (id == 7)
        {
            Assert.IsNotNull(player, "Phil Jones not found");
            Assert.IsTrue(
                player.Name == "Phil Jones" && player.Age == 30 && player.Height == 1.85f && player.TeamId == 2,
                "Failed to obtain Phil Jones's data"
            );
        }
        else if (id == 11)
        {
            Assert.IsNull(player, $"Player with ID={id} must be null");
        }
    }

    [TestMethod]
    public void CreatePlayer()
    {
        var model = new CreateEditPlayerModel("Lionel Messi", 35, 1.70f, 1);

        var id = service.Create(model);
        Assert.AreEqual(id, 11, "The new player's Id must be 11");

        var player = service.GetById(11);
        Assert.IsNotNull(player, "The new player not found");
        Assert.IsTrue(
            player.Name == "Lionel Messi" && player.Age == 35 && player.Height == 1.70f && player.TeamId == 1,
            "Failed to obtain the new player's data"
        );
    }

    [TestMethod]
    [DataRow(11)]
    [DataRow(15)]
    public void EditPlayer(int id)
    {
        var model = new CreateEditPlayerModel("Manuel Neuer", 36, 1.93f, 1);
        var result = service.Edit(id, model);

        if (id == 11)
        {
            Assert.IsTrue(result, "Edit the new player failed");

            var player = service.GetById(11);
            Assert.IsNotNull(player, "The new player not found");
            Assert.IsTrue(
                player.Name == "Manuel Neuer" && player.Age == 36 && player.Height == 1.93f && player.TeamId == 1,
                "Failed to obtain the new player's data"
            );
        }
        else if (id == 15)
        {
            Assert.IsFalse(result, "Edit result of a non-existing player must be false");
        }
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(15)]
    public void DeletePlayer(int id)
    {
        var result = service.Delete(id);

        if (id == 1)
        {
            Assert.IsTrue(result, "Delete player failed");

            var player = service.GetById(1);
            Assert.IsNull(player, "The player should have been deleted but not");
        }
        else if (id == 15)
        {
            Assert.IsFalse(result, "Delete result of a non-existing player must be false");
        }
    }

    [TestMethod]
    [DataRow(6, 1)]
    [DataRow(3, 1)]
    [DataRow(3, 3)]
    [DataRow(15, 2)]
    public void TransferPlayer(int id, int destTeamId)
    {
        if (id == 6 && destTeamId == 1)
        {
            service.Transfer(id, new TransferPlayerModel(destTeamId));
        }
        else if (id == 3 && destTeamId == 1)
        {
            Assert.ThrowsException<ApplicationException>(
                () => service.Transfer(id, new TransferPlayerModel(destTeamId)),
                "Failed to check selling a player to their current team"
            );
        }
        else if (id == 3 && destTeamId == 3)
        {
            Assert.ThrowsException<ApplicationException>(
                () => service.Transfer(id, new TransferPlayerModel(destTeamId)),
                "Failed to check selling a player to a non-existing team"
            );
        }
        else if (id == 15 && destTeamId == 1)
        {
            Assert.ThrowsException<ApplicationException>(
                () => service.Transfer(id, new TransferPlayerModel(destTeamId)),
                "Failed to check selling a non-existing player"
            );
        }
    }
}
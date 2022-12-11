using Dapper;
using Dapper.Contrib.Extensions;
using FootballPlayers.Abstractions.Persistence;
using FootballPlayers.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace FootballPlayers.Infrastructure.Persistence;

internal class PlayerRepository : RepositoryBase, IPlayerRepository
{
    public PlayerRepository(IConfiguration config) : base(config)
    {
    }

    public IEnumerable<Player> FetchAll()
    {
        using SqliteConnection connection = new(connectionString);
        return connection.Query<Player, Team, Player>(
            "select p.*, t.* from Player p inner join Team t on p.TeamId=t.Id",
            (player, team) => { player.Team = team; return player; }
        );
    }

    public Player? FetchById(int id)
    {
        using SqliteConnection connection = new(connectionString);
        return connection.Query<Player, Team, Player>(
            "select p.*, t.* from Player p inner join Team t on p.TeamId=t.Id where p.Id = @Id",
            (player, team) => { player.Team = team; return player; },
            new { Id = @id }
        ).FirstOrDefault();
    }

    public IEnumerable<Player> FetchByTeam(int teamId)
    {
        using SqliteConnection connection = new(connectionString);
        return connection.Query<Player>("select * from Player where TeamId=@TeamId", new { TeamId = teamId });
    }

    public int Insert(Player player)
    {
        using SqliteConnection connection = new(connectionString);
        var multi = connection.QueryMultiple(
            "insert into Player(Name, Age, Height, TeamId) values(@Name, @Age, @Height, @TeamId); select last_insert_rowid() id;",
            new { player.Name, player.Age, player.Height, player.TeamId }
        );

        var id = (int)multi.Read().First().id;
        player.Id = id;
        return id;
    }

    public bool Update(Player player)
    {
        using SqliteConnection connection = new(connectionString);
        var result = connection.Execute(
            "update Player set Name=@Name, Age=@Age, Height=@Height, Teamid=@TeamId where Id=@Id",
            new { player.Id, player.Name, player.Age, player.Height, player.TeamId }
        );
        return result > 0;
    }

    public bool Delete(int id)
    {
        using SqliteConnection connection = new(connectionString);
        return connection.Delete(new Player { Id = id });
    }
}
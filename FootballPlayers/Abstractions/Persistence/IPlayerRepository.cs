using FootballPlayers.Entities;

namespace FootballPlayers.Abstractions.Persistence;

public interface IPlayerRepository
{
    IEnumerable<Player> FetchAll();

    Player? FetchById(int id);

    IEnumerable<Player> FetchByTeam(int teamId);

    int Insert(Player player);

    bool Update(Player player);

    bool Delete(int id);
}
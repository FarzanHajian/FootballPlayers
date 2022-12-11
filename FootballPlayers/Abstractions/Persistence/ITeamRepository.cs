using FootballPlayers.Entities;

namespace FootballPlayers.Abstractions.Persistence;

public interface ITeamRepository
{
    IEnumerable<Team> FetchAll();

    bool Exists(int id);
}
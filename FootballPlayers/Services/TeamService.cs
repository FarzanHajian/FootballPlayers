using FootballPlayers.Abstractions.Persistence;
using FootballPlayers.Entities;
using FootballPlayers.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Services;

public class TeamService : ServiceBase
{
    private readonly ITeamRepository repository;
    private readonly IPlayerRepository playerRepository;

    public TeamService(IServiceProvider ioc) : base(ioc)
    {
        repository = ioc.GetRequiredService<ITeamRepository>();
        playerRepository = ioc.GetRequiredService<IPlayerRepository>();
    }

    public IEnumerable<Team> GetAll()
    {
        return repository.FetchAll();
    }

    public IEnumerable<TeamPlayerListModel> GetAllPlayersByTeam(int teamdId)
    {
        if (!repository.Exists(teamdId)) throw new ApplicationException("The specified team does not exist.");
        return
            from p in playerRepository.FetchByTeam(teamdId)
            select new TeamPlayerListModel(p.Id, p.Name, p.Age, p.Height);
    }
}
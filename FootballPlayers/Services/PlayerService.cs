using FluentValidation;
using FluentValidation.Results;
using FootballPlayers.Abstractions.Persistence;
using FootballPlayers.Entities;
using FootballPlayers.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Services;

public class PlayerService : ServiceBase
{
    private readonly IPlayerRepository repository;

    public PlayerService(IServiceProvider ioc) : base(ioc)
    {
        repository = ioc.GetRequiredService<IPlayerRepository>();
    }

    public IEnumerable<Player> GetAll()
    {
        return repository.FetchAll();
    }

    public Player? GetById(int id)
    {
        return repository.FetchById(id);
    }

    public int Create(CreateEditPlayerModel model)
    {
        ValidateModel(model);
        return repository.Insert(
            new Player
            {
                Name = model.Name,
                Age = model.Age,
                Height = model.Height,
                TeamId = model.TeamId
            }
        );
    }

    public bool Edit(int playerId, CreateEditPlayerModel model)
    {
        ValidateModel(model);
        return repository.Update(
            new Player
            {
                Id = playerId,
                Name = model.Name,
                Age = model.Age,
                Height = model.Height,
                TeamId = model.TeamId
            }
        );
    }

    public bool Delete(int id)
    {
        return repository.Delete(id);
    }

    public void Transfer(int id, TransferPlayerModel model)
    {
        ValidateModel(model);

        ITeamRepository teamRepository = ioc.GetRequiredService<ITeamRepository>();
        if (!teamRepository.Exists(model.DestinationTeamId)) throw new ApplicationException("The destination team does not exist.");

        if (repository.FetchById(id) is not Player player) throw new ApplicationException("The player does not exist.");
        if (player.TeamId == model.DestinationTeamId) throw new ApplicationException("The player cannot be sold to their current team. Please specifiy another team.");

        player.TeamId = model.DestinationTeamId;
        repository.Update(player);
    }
}
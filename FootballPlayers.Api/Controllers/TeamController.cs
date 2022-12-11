using FootballPlayers.Entities;
using FootballPlayers.Models;
using FootballPlayers.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayers.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class TeamController : ControllerBase
{
    private readonly TeamService service;

    public TeamController(TeamService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IEnumerable<Team> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("{id:int}/players")]
    public IEnumerable<TeamPlayerListModel> GetAllPlayers(int id)
    {
        return service.GetAllPlayersByTeam(id);
    }
}
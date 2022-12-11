using FootballPlayers.Entities;
using FootballPlayers.Models;
using FootballPlayers.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayers.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService service;

    public PlayerController(PlayerService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IEnumerable<Player> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = service.GetById(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(CreateEditPlayerModel model)
    {
        var result = service.Create(model);
        return CreatedAtAction(nameof(GetById), new { Id = result }, null);
    }

    [HttpPut("{id:int}")]
    public IActionResult Edit(int id, CreateEditPlayerModel model)
    {
        var result = service.Edit(id, model);
        return result ? Ok() : Problem("Update failed. Make sure to update an existing player.", statusCode: 400);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = service.Delete(id);
        return result ? Ok() : Problem("Delete failed. Make sure to delete an existing player.", statusCode: 400);
    }

    [HttpPut("{id:int}/transfer")]
    public IActionResult Transfer(int id, TransferPlayerModel model)
    {
        service.Transfer(id, model);
        return Ok();
    }
}
using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;

    public GameRoomController(IGameRoomService gameRoomService)
    {
        _gameRoomService = gameRoomService;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<GameRoomDto>>> List(CancellationToken cancellationToken)
    {
        var result = await _gameRoomService.GetList(cancellationToken);
        return new JsonResult(result);
    }

    [HttpPost("add")]
    public async Task<ActionResult<Guid>> Add(GameRoomDto gameRoomDto, CancellationToken cancellationToken)
    {
        var result = await _gameRoomService.Add(gameRoomDto, cancellationToken);
        return new JsonResult(result);
    }
}
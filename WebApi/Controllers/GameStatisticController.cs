using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GameStatisticController : ControllerBase
{
    private readonly IGameStatisticService _gameStatisticService;

    public GameStatisticController(IGameStatisticService gameStatisticService)
    {
        _gameStatisticService = gameStatisticService;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<GameStatisticDto>>> List(CancellationToken cancellationToken)
    {
        var result = await _gameStatisticService.GetList(cancellationToken);
        return new JsonResult(result);
    }
    
    [HttpGet("list/{userId}")]
    public async Task<ActionResult<List<GameStatisticDto>>> List(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _gameStatisticService.GetListByUser(userId, cancellationToken);
        return new JsonResult(result);
    }
}
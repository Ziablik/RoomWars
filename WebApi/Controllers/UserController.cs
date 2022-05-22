using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("add")]
    public async Task<ActionResult<Guid>> Add(UserDto userDto, CancellationToken cancellationToken)
    {
        var result = await _userService.Add(userDto, cancellationToken);
        return new JsonResult(result);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<UserDto>>> List(CancellationToken cancellationToken)
    {
        var result = await _userService.GetList(cancellationToken);
        return new JsonResult(result);
    }
}
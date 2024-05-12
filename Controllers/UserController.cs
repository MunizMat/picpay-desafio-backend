using Microsoft.AspNetCore.Mvc;
using Models;
using Dtos;
using Services.Interfaces;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> Get()
    {
        var result = await _userService.GetAllUsersAsync();
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] UserModel user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var newUser = await _userService.CreateUserAsync(user);

        return CreatedAtAction(nameof(Post), newUser);
    }

}


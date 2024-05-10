using Contexts;
using Microsoft.AspNetCore.Mvc;
using Models;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly PostgreSqlContext _postgreSqlContext;

    public UserController(PostgreSqlContext postgreSqlContext)
    {
        _postgreSqlContext = postgreSqlContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> Get()
    {
        var users = await _postgreSqlContext.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Cpf = u.Cpf,
                FullName = u.FullName,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                UserType = u.UserType.ToString().ToLower()
            })
            .ToListAsync();

        return users;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] UserModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _postgreSqlContext.Users.Add(user);
        await _postgreSqlContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
    }

}


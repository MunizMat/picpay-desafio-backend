using Contexts;
using Dtos;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Interfaces;

namespace Services;

public class UserService : IUserService
{
    private readonly PostgreSqlContext _postgreSqlContext;

    public UserService(PostgreSqlContext postgreSqlContext)
    {
        _postgreSqlContext = postgreSqlContext;
    }

    public async Task<UserDto> CreateUserAsync(UserModel newUser)
    {
        _postgreSqlContext.Users.Add(newUser);
        await _postgreSqlContext.SaveChangesAsync();

        return new UserDto
        {
            AccountBalance = 5000.0m,
            Cpf = newUser.Cpf,
            CreatedAt = newUser.CreatedAt,
            Email = newUser.Email,
            FullName = newUser.FullName,
            Id = newUser.Id,
            UserType = newUser.UserType.ToString().ToLower()
        };
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _postgreSqlContext.Users
        .Select(u => new UserDto
        {
            Id = u.Id,
            Cpf = u.Cpf,
            FullName = u.FullName,
            Email = u.Email,
            CreatedAt = u.CreatedAt,
            AccountBalance = u.AccountBalance,
            UserType = u.UserType.ToString().ToLower()
        }).ToListAsync();

        return users;
    }
}

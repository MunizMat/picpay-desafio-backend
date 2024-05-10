using Dtos;
using Models;

namespace Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> CreateUserAsync(UserModel newUser);
}

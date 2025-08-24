using Luma.API.Models;

namespace Luma.API.Services;

public interface IUserService
{
    Task<UserDto?> GetCurrentUserAsync();
    Task<UserDto?> GetUserByIdAsync(Guid userId);
}
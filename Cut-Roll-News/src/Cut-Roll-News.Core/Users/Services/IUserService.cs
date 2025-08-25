namespace Cut_Roll_News.Core.Users.Services;

using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Users.Dtos;
using Cut_Roll_News.Core.Users.Models;

public interface IUserService
{
    Task<string> CreateUserAsync(UserCreateDto? userCreateDto);
    Task<string> UpdateUserAsync(UserUpdateDto? userUpdateDto);
    Task<UserResponseDto?> GetUserByIdAsync(string? userId);
    Task<string> DeleteUserByIdAsync(string? userId);
    Task<UserResponseDto?> GetUserByUsernameAsync(string? username);
    Task<UserResponseDto?> GetUserByEmailAsync(string? email);
    Task<PagedResult<UserResponseDto>> SearchUsersAsync(UserSearchDto? dto);
    Task<bool> UserExistsByUsernameAsync(string? username);
    Task<bool> UserExistsByEmailAsync(string? email);
    Task<string> UpdateUserAvatarAsync(UserUpdateAvatarDto? dto);
    Task<IQueryable<User>> GetUsersAsQueryableAsync();
}

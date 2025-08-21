using Cut_Roll_News.Core.Common.Dtos;
using Cut_Roll_News.Core.Common.Repositories.Base;
using Cut_Roll_News.Core.Users.Dtos;
using Cut_Roll_News.Core.Users.Models;

namespace Cut_Roll_News.Core.Users.Repositories;

public interface IUserRepository :
    ICreateAsync<UserCreateDto, string?>,
    IGetByIdAsync<string, UserResponseDto?>,
    IUpdateAsync<UserUpdateDto, string?>,
    IDeleteByIdAsync<string, string?>
{
    Task<UserResponseDto?> GetUserByUsernameAsync(string username);
    Task<UserResponseDto?> GetUserByEmailAsync(string email);
    Task<int> CountUsersByRoleAsync(string roleId);
    Task<PagedResult<UserResponseDto>> SearchUsersAsync(UserSearchDto dto);
    Task<bool> UserExistsByUsernameAsync(string username);
    Task<bool> UserExistsByEmailAsync(string email);
    Task<string?> UpdateAvatarAsync(UserUpdateAvatarDto dto);
    Task<IQueryable<User>> GetUsersAsQueryableAsync();
}

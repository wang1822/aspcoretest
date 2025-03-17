using AspCoreStudy.Models;

/**
* 用户服务接口 
*/
namespace AspCoreStudy.Services
{
    public interface IUserService
    {
        // 分配角色给用户
        Task AssignRoleToUserAsync(int userId, int roleId);

        // 查询用户是否存在
        Task<User> AuthenticateUserAsync(string username, string password);

        // 创建用户
        Task CreateAsync(User user);

        // 获取用户权限
        Task<List<string>> FetchPermissionsForUserAsync(int userId);

        // 根据账号查询用户
        Task<User> FetchUserByUsernameAsync(string username);
    }
}
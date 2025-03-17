using AspCoreStudy.Models;

/**
* 实现用户数据操作接口
*/
namespace AspCoreStudy.Repositories
{
    // 实现用户个性化的接口
    public interface IUserRepository : IRepository<User>
    {
        // 根据账号查询用户
        Task<User> GetUserByUsernameAsync(string username, string password);
        // 根据账号查询用户
        Task<User> GetUserByUsernameAsync(string username);
        // 分配角色
        Task AssignRoleAsync(int userId, int roleId);
        // 获取用户权限
        Task<List<string>> GetUserPermissionsAsync(int userId);
    }
}
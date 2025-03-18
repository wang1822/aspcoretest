using AspCoreStudy.Models;

namespace AspCoreStudy.Repositories
{
    /// <summary>
    /// 用户存储库操作的接口。
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 根据用户名和密码检索用户。
        /// </summary>
        /// <param name="username">用户的用户名。</param>
        /// <param name="password">用户的密码。</param>
        /// <returns>表示异步操作的任务。任务结果包含用户。</returns>
        Task<User> GetUserByUsernameAsync(string username, string password);
        /// <summary>
        /// 根据账号查询用户。
        /// </summary>
        /// <param name="username">用户的用户名。</param>
        /// <returns>表示异步操作的任务。任务结果包含用户。</returns>
        Task<User> GetUserByUsernameAsync(string username);
        /// <summary>
        /// 分配角色给用户。
        /// </summary>
        /// <param name="userId">用户的ID。</param>
        /// <param name="roleId">角色的ID。</param>
        /// <returns>表示异步操作的任务。</returns>
        Task AssignRoleAsync(int userId, int roleId);
        /// <summary>
        /// 获取用户的权限。
        /// </summary>
        /// <param name="userId">用户的ID。</param>
        /// <returns>表示异步操作的任务。任务结果包含用户的权限列表。</returns>
        Task<List<string>> GetUserPermissionsAsync(int userId);
    }
}
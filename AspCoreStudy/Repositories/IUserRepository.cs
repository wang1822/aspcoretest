using AspCoreStudy.Models;

namespace AspCoreStudy.Repositories
{
    /// <summary>
    /// 用户存储库操作的接口。
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
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

        /// <summary>
        /// 获取分页的用户列表。
        /// </summary>
        /// <param name="page">页码。</param>
        /// <param name="pageSize">每页的用户数量。</param>
        /// <returns>表示异步操作的任务。任务结果包含用户列表。</returns>
        Task<List<User>> GetAllUserAsync(int page, int pageSize);

        /// <summary>
        /// 根据用户名获取分页的用户列表。
        /// </summary>
        /// <param name="userName">用户的用户名。</param>
        /// <param name="page">页码。</param>
        /// <param name="pageSize">每页的用户数量。</param>
        /// <returns>表示异步操作的任务。任务结果包含用户列表。</returns>
        Task<List<User>> GetAllUserByUserNameAsync(string userName, int page, int pageSize);

        /// <summary>
        /// 获取用户总数。
        /// </summary>
        /// <returns>表示异步操作的任务。任务结果包含用户总数。</returns>
        Task<int> CountAllUsersAsnyc();
    }
}
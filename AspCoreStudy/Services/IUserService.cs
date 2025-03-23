using AspCoreStudy.Models;

namespace AspCoreStudy.Services
{
    /// <summary>
    /// 提供用户管理和身份验证的方法。
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 异步分配角色给用户。
        /// </summary>
        /// <param name="userId">用户的ID。</param>
        /// <param name="roleId">要分配的角色ID。</param>
        Task AssignRoleToUserAsync(int userId, int roleId);

        /// <summary>
        /// 异步验证用户的用户名和密码。
        /// </summary>
        /// <param name="username">用户的用户名。</param>
        /// <param name="password">用户的密码。</param>
        /// <returns>返回验证成功的用户对象。</returns>
        Task<User> AuthenticateUserAsync(string username, string password);
        /// <summary>
        /// 异步统计符合条件的用户总数。
        /// </summary>
        /// <returns>返回符合条件的用户总数。</returns>
        Task<int> CountAllUsersAsync();

        /// <summary>
        /// 异步创建一个新的用户。
        /// </summary>
        /// <param name="user">要创建的用户对象。</param>
        Task CreateAsync(User user);
        /// <summary>
        /// 异步获取所有用户。
        /// </summary>
        /// <returns>返回包含所有用户的用户对象。</returns>
        Task<List<User>> FetchAllUsersAsync(string username, int page, int pageSize);

        /// <summary>
        /// 异步获取指定用户的权限列表。
        /// </summary>
        /// <param name="userId">用户的ID。</param>
        /// <returns>返回包含用户权限的字符串列表。</returns>
        Task<List<string>> FetchPermissionsForUserAsync(int userId);

        /// <summary>
        /// 异步获取指定用户名的用户。
        /// </summary>
        /// <param name="username">用户的用户名。</param>
        /// <returns>返回匹配用户名的用户对象。</returns>
        Task<User> FetchUserByUsernameAsync(string username);
    }
}
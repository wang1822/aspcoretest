
using AspCoreStudy.Models;

namespace AspCoreStudy.Services
{
    /// <summary>
    /// 提供管理角色的方法。
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 异步获取指定名称的角色。
        /// </summary>
        /// <param name="roleName">要获取的角色名称。</param>
        /// <returns>表示异步操作的任务。任务结果包含角色。</returns>
        Task<Role> FetchRoleByNameAsync(string roleName);
    }
}
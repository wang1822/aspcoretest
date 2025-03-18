
using AspCoreStudy.Models;

namespace AspCoreStudy.Repositories
{
    /// <summary>
    /// 角色存储库的接口，用于管理角色实体。
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// 根据指定的角色名称检索角色实体。
        /// </summary>
        /// <param name="roleName">要检索的角色名称。</param>
        /// <returns>表示异步操作的任务。任务结果包含角色实体。</returns>
        Task<Role> GetRoleByNameAsync(string roleName);
    }
}
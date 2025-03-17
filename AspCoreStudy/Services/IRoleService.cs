
using AspCoreStudy.Models;

namespace AspCoreStudy.Services
{
    public interface IRoleService
    {
        // 根据角色名称获取角色
        Task<Role> FetchRoleByNameAsync(string roleName);
    }
}
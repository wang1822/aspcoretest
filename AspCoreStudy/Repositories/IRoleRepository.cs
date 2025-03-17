
using AspCoreStudy.Models;

namespace AspCoreStudy.Repositories
{
    public interface IRoleRepository: IRepository<Role>
    {
        //根据角色名称获取角色
        Task<Role> GetRoleByNameAsync(string roleName);
    }
}
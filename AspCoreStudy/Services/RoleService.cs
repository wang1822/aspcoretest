using AspCoreStudy.Models;
using AspCoreStudy.Repositories;

namespace AspCoreStudy.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        // 构造函数
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // 根据角色名称获取角色
        public async Task<Role> FetchRoleByNameAsync(string roleName)
        {
            return await _roleRepository.GetRoleByNameAsync(roleName);
        }
    }
}
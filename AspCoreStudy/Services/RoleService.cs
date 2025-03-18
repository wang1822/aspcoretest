using AspCoreStudy.Models;
using AspCoreStudy.Repositories;

namespace AspCoreStudy.Services
{
    /// <inheritdoc/>
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        /// <inheritdoc/>
        public async Task<Role> FetchRoleByNameAsync(string roleName)
        {
            return await _roleRepository.GetRoleByNameAsync(roleName);
        }
    }
}
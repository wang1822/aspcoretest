using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreStudy.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) { }

        // 根据角色名称获取角色
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
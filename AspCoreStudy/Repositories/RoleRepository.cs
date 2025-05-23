using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreStudy.Repositories
{
    /// <ihneritdoc />
    public class RoleRepository(ApplicationDbContext context) : Repository<Role>(context), IRoleRepository
    {
        /// <ihneritdoc />
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
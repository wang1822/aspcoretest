using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;

/**
* 实现用户数据操作接口
*/
namespace AspCoreStudy.Repositories
{
    // 实现用户个性化的类
    public class UserRepository : Repository<User>, IUserRepository
    {
        //构造函数
        public UserRepository(ApplicationDbContext context) : base(context) { }

        // 分配角色
        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var userRole = new Dictionary<string, object>
            {
                { "UserId", userId },
                { "RoleId", roleId }
            };
            _context.Set<Dictionary<string, object>>("UserRole").Add(userRole);
            await _context.SaveChangesAsync();
        }

        // 根据账号查询用户
        public async Task<User> GetUserByUsernameAsync(string username, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
        }

        // 根据账号查询用户
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
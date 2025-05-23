using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreStudy.Repositories
{
    /// <inheritdoc/>
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {

        /// <inheritdoc/>
        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var role = await _context.Roles.FindAsync(roleId);

            if (user != null && role != null && !user.Roles.Contains(role))
            {
                user.Roles.Add(role);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<int> CountAllUsersAsnyc()
        {
            var users = await GetAllAsync();
            return users.Count();
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetAllUserAsync(int page, int pageSize)
        {
            var users = await GetAllAsync();
            return [.. users.Skip((page - 1) * pageSize).Take(pageSize)];
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetAllUserByUserNameAsync(string userName, int page, int pageSize)
        {
            var users = await GetAllAsync();
            return [.. users.Where(u => u.Username == userName).Skip((page - 1) * pageSize).Take(pageSize)];
        }

        /// <inheritdoc/>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .SelectMany(r => r.Permissions)
                .Select(p => p.Name)
                .Distinct()
                .ToListAsync();
        }
    }
}
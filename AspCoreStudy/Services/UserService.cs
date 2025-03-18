using AspCoreStudy.Models;
using AspCoreStudy.Repositories;

namespace AspCoreStudy.Services
{
    /// <inheritdoc/>
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        /// <inheritdoc/>
        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            await _userRepository.AssignRoleAsync(userId, roleId);
        }

        /// <inheritdoc/>
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            // if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            //     return null; // 密码错误或用户不存在

            return user; // 用户验证通过，返回用户对象
        }

        /// <inheritdoc/>
        public async Task CreateAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // 使用 BCrypt 哈希密码
            user.CreatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);
        }

        /// <inheritdoc/>
        public async Task<List<string>> FetchPermissionsForUserAsync(int userId)
        {
            return await _userRepository.GetUserPermissionsAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<User> FetchUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}
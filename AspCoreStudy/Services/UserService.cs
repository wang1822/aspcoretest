using AspCoreStudy.Models;
using AspCoreStudy.Repositories;

namespace AspCoreStudy.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        // 构造函数
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // 分配角色给用户
        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            await _userRepository.AssignRoleAsync(userId, roleId);
        }

        // 查询用户是否存在
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null; // 密码错误或用户不存在

            return user; // 用户验证通过，返回用户对象
        }

        // 创建用户
        public async Task CreateAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // 使用 BCrypt 哈希密码
            user.CreatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);
        }

        // 获取用户权限
        public async Task<List<string>> FetchPermissionsForUserAsync(int userId)
        {
            return await _userRepository.GetUserPermissionsAsync(userId);
        }

        // 根据账号查询用户
        public async Task<User> FetchUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}
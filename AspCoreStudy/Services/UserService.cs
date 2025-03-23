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
            User user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
                throw new CustomException("密码或账号错误");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new CustomException("密码或账号错误");

            return user;
        }

        /// <inheritdoc/>
        public async Task<int> CountAllUsersAsync()
        {
            return await _userRepository.CountAllUsersAsnyc();
        }

        /// <inheritdoc/>
        public async Task CreateAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // 使用 BCrypt 哈希密码
            user.CreatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);
        }

        /// <inheritdoc/>
        public async Task<List<User>> FetchAllUsersAsync(string username, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(username))
                return await _userRepository.GetAllUserByUserNameAsync(username, page, pageSize);
            else
                return await _userRepository.GetAllUserAsync(page, pageSize);
        }

        /// <inheritdoc/>
        public async Task<List<string>> FetchPermissionsForUserAsync(int userId)
        {
            return await _userRepository.GetUserPermissionsAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<User> FetchUserByUsernameAsync(string username)
        {
            User user = await _userRepository.GetUserByUsernameAsync(username);

            if (user != null)
                throw new CustomException("用户已存在");

            return user;
        }
    }
}
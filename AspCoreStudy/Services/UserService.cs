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

        public async Task<User> GetUserByCredentialsAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
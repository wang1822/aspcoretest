using AspCoreStudy.Models;

/**
* 用户服务接口 
*/
namespace AspCoreStudy.Services
{
    public interface IUserService
    {
        // 查询用户是否存在
         Task<User> GetUserByCredentialsAsync(string username, string password);
    }
}
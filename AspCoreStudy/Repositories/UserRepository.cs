using AspCoreStudy.Models;

/**
* 实现用户数据操作接口
*/
namespace AspCoreStudy.Repositories
{
    // 实现用户个性化的类
    public class UserRepository : Repository<User>, IUserRepository
    {
        //构造函数
        public UserRepository(my_databaseContext context) : base(context){}
    }
}
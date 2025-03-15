/**
* 通用的Repository接口
*/
namespace AspCoreStudy.Repositories
{
    public interface IRepository<T> where T : class
    {
        //获取所有数据
        Task<IEnumerable<T>> GetAllAsync();
        //根据Id获取数据
        Task<T> GetByIdAsync(int id);
        //创建数据
        Task CreateAsync(T entity);
        //更新数据
        Task UpdateAsync(int id, T entity);
        //删除数据
        Task DeleteAsync(int id);
    }
}
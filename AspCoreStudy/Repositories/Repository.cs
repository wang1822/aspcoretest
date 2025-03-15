using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;

/**
* 实现通用仓储类
*/
namespace AspCoreStudy.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //数据库上下文
        protected readonly my_databaseContext _context;
        //数据集
        private readonly DbSet<T> _dbSet;

        //构造函数
        public Repository(my_databaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //实现创建数据
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //实现删除数据
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        //实现获取所有数据
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        //实现根据Id获取数据
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //实现更新数据
        public async Task UpdateAsync(int id, T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
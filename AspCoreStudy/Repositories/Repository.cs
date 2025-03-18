using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreStudy.Repositories
{
    /// <inheritdoc />
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
    {
        
        /// <summary>
        /// 用于访问数据库的数据库上下文。
        /// </summary>
        protected readonly ApplicationDbContext _context = context;
        /// <summary>
        /// 表示实体类型<typeparamref name="T"/>的数据库集。
        /// </summary>
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        /// <inheritdoc/>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id) 
            ?? throw new InvalidOperationException($"未找到具有 ID {id} 的实体。");
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(int id, T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
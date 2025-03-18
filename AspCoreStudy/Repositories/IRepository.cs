namespace AspCoreStudy.Repositories
{
    /// <summary>
    /// 表示用于管理<typeparamref name="T"/>类型实体的通用存储库.
    /// </summary>
    /// <typeparam name="T">实体的类型。</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 获取所有数据.
        /// </summary>
        /// <returns>一个包含所有<typeparamref name="T"/>类型实体的任务.</returns>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// 根据Id获取数据.
        /// </summary>
        /// <param name="id">实体的唯一标识符.</param>
        /// <returns>一个包含指定<typeparamref name="T"/>类型实体的任务.</returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// 创建一个新的<typeparamref name="T"/>类型实体.
        /// </summary>
        /// <param name="entity">要创建的实体.</param>
        Task AddAsync(T entity);
        /// <summary>
        /// 更新指定Id的<typeparamref name="T"/>类型实体.
        /// </summary>
        /// <param name="id">实体的唯一标识符.</param>
        /// <param name="entity">要更新的实体.</param>
        Task UpdateAsync(int id, T entity);
        /// <summary>
        /// 删除指定Id的<typeparamref name="T"/>类型实体.
        /// </summary>
        /// <param name="id">实体的唯一标识符.</param>
        Task DeleteAsync(int id);
    }
}
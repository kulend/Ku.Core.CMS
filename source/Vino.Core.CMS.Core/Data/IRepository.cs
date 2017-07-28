using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.CMS.Core.Data
{
    /// <summary>
    /// 仓储接口定义
    /// </summary>
    public interface IRepository
    {
        void Save();

        Task SaveAsync();
    }

    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        /// <summary>
        /// 获取检索对象
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        TEntity GetById(TPrimaryKey id);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        bool Delete(TPrimaryKey id);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        Task<bool> DeleteAsync(TPrimaryKey id);

        #region Any

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        bool Any(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

        #endregion

        #region 分页查询

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="page">查询页码</param>
        /// <param name="size">查询条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件，格式类似【 Name asc, CreateTime desc】</param>
        /// <returns></returns>
        Task<(int count, List<TEntity> items)> PageQueryAsync(int page, int size,
                    Expression<Func<TEntity, bool>> where, string order);

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="page">查询页码</param>
        /// <param name="size">查询条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isDesc">排序方式</param>
        /// <returns></returns>
        Task<(int count, List<TEntity> items)> PageQueryAsync<Tkey>(int page, int size,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, Tkey>> order, bool isDesc = false);

        #endregion
    }

    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : BaseEntity
    {

    }
}
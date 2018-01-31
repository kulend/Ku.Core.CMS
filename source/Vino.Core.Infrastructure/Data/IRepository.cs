using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.Infrastructure.Data
{
    /// <summary>
    /// 仓储接口定义
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// 保存
        /// </summary>
        void Save();

        /// <summary>
        /// 异步保存
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        Task<ITransaction> BeginTransactionAsync();
    }

    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface IRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        #region 主键查询

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

        #endregion

        #region 通用查询

        /// <summary>
        /// 获取检索对象
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="where">lambda表达式条件</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where, string order);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where, string order, Expression<Func<TEntity, object>>[] includes);

        #endregion

        #region 插入

        /// <summary>
        /// 新增实体
        /// </summary>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 新增实体
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 普通批量插入
        /// </summary>
        void InsertRange(List<TEntity> entitys);

        /// <summary>
        /// 异步批量插入
        /// </summary>
        Task InsertRangeAsync(List<TEntity> entitys);

        #endregion

        #region 更新

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 单个对象指定列修改
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity, List<string> proNames);

        #endregion

        #region 删除

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

        #endregion

        #region 是否存在

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        bool Any(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);

        #endregion

        #region 件数

        /// <summary>
        /// 取得件数
        /// </summary>
        int Count(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 异步取得件数
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where);
        
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
        Task<(int count, List<TEntity> items)> PageQueryAsync(int page, int size, Expression<Func<TEntity, bool>> where, string order);

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="page">查询页码</param>
        /// <param name="size">查询条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件，格式类似【 Name asc, CreateTime desc】</param>
        /// <param name="includes">连接其他表</param>
        /// <returns></returns>
        Task<(int count, List<TEntity> items)> PageQueryAsync(int page, int size, Expression<Func<TEntity, bool>> where, string order, Expression<Func<TEntity, object>>[] includes);

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
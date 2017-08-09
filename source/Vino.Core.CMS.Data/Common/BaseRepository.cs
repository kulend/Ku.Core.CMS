using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Core.Extensions;

namespace Vino.Core.CMS.Data.Common
{
    public abstract class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        //定义数据访问上下文对象
        protected readonly VinoDbContext _dbContext;

        /// <summary>
        /// 获取DbSet
        /// </summary>
        protected virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        protected BaseRepository(VinoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 主键查询

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public TEntity GetById(TPrimaryKey id)
        {
            return Table.FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await Table.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        #endregion

        #region 通用查询

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var propertySelector in includes)
                {
                    query = query.Include(propertySelector);
                }
            }
            //处理有IsDelete字段表
            if (typeof(BaseProtectedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(x => !(x as BaseProtectedEntity).IsDeleted);
            }
            return query;
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where, string order)
        {
            var query = Table.AsQueryable();
            if (where != null)
            {
                query = query.Where(where);
            }
            //处理有IsDelete字段表
            if (typeof(BaseProtectedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(x => !(x as BaseProtectedEntity).IsDeleted);
            }

            if (order.IsNotNullOrEmpty())
            {
                var orderFileds = order.SplitRemoveEmpty(',').Select(x => x.Trim()).ToArray();
                var filedFirst = orderFileds.First();

                var orderFiledFirstParts = filedFirst.SplitRemoveEmpty(' ').Select(x => x.Trim()).ToArray();
                var nameFirst = orderFiledFirstParts[0];
                var isDescFirst = false;
                if (orderFiledFirstParts.Length > 1)
                {
                    isDescFirst = orderFiledFirstParts[1].EqualOrdinalIgnoreCase("desc");
                }
                query = isDescFirst ? query.OrderByDescending(nameFirst) : query.OrderBy(nameFirst);

                if (orderFileds.Count() > 1)
                {
                    for (int i = 1; i < orderFileds.Length; i++)
                    {
                        var orderFiledParts = orderFileds[i].SplitRemoveEmpty(' ').Select(x => x.Trim()).ToArray();
                        var name = orderFiledParts[0];
                        var isDesc = false;
                        if (orderFiledParts.Length > 1)
                        {
                            isDesc = orderFiledParts[1].EqualOrdinalIgnoreCase("desc");
                        }
                        query = isDesc ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(name) : ((IOrderedQueryable<TEntity>)query).ThenBy(name);
                    }
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> QueryAsync<Tkey>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, Tkey>> order, bool isDesc = false)
        {
            var query = Table.AsQueryable();
            if (where != null)
            {
                query = query.Where(where);
            }
            //处理有IsDelete字段表
            if (typeof(BaseProtectedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(x => !(x as BaseProtectedEntity).IsDeleted);
            }
            if (order != null)
            {
                query = isDesc ? query.OrderByDescending(order) : query.OrderBy(order);
            }
            return await query.ToListAsync();
        }

        #endregion

        #region 插入

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            //BaseEntity baseEntity = entity as BaseEntity;
            //if (baseEntity != null && baseEntity.CreateTime == default(DateTime))
            //{
            //    baseEntity.CreateTime = DateTime.Now;
            //}
            Table.Add(entity);
            return entity;
        }

        /// <summary>
        /// 异步插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            //BaseEntity baseEntity = entity as BaseEntity;
            //if (baseEntity != null && baseEntity.CreateTime == default(DateTime))
            //{
            //    baseEntity.CreateTime = DateTime.Now;
            //}
            await Table.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// 普通批量插入
        /// </summary>
        public void InsertRange(List<TEntity> entitys)
        {
            Table.AddRange(entitys);
        }

        /// <summary>
        /// 普通批量插入
        /// </summary>
        public async Task InsertRangeAsync(List<TEntity> entitys)
        {
            await Table.AddRangeAsync(entitys);
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(TEntity entity)
        {
            Table.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 单个对象指定列修改
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(TEntity entity, List<string> proNames)
        {
            Table.Attach(entity);
            //TODO:未实现
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        public bool Delete(TPrimaryKey id)
        {
            Table.Remove(GetById(id));
            return true;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        public async Task<bool> DeleteAsync(TPrimaryKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            if (entity is BaseProtectedEntity)
            {
                //假删除
                var protectedEntity = entity as BaseProtectedEntity;
                protectedEntity.IsDeleted = true;
                Update(entity);
            }
            else
            {
                Table.Remove(entity);
            }
            return true;
        }

        #endregion

        #region 是否存在

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        public bool Any(Expression<Func<TEntity, bool>> where)
        {
            return Table.Any(where);
        }

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Table.AnyAsync(where);
        }

        #endregion

        #region 件数

        /// <summary>
        /// 取得件数
        /// </summary>
        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return Table.Count(where);
        }

        /// <summary>
        /// 异步取得件数
        /// </summary>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Table.CountAsync(where);
        }

        #endregion

        #region 分页查询

        /// <summary>
        /// 异步分页查询
        /// </summary>
        public async Task<(int count, List<TEntity> items)> PageQueryAsync(int page, int size,
            Expression<Func<TEntity, bool>> where,
            string order)
        {
            return await PageQueryAsync(page, size, where, order, null);
        }

        /// <summary>
        /// 异步分页查询
        /// </summary>
        public async Task<(int count, List<TEntity> items)> PageQueryAsync(int page, int size,
            Expression<Func<TEntity, bool>> where,
            string order, Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (where != null)
            {
                query = query.Where(where);
            }
            //处理有IsDelete字段表
            if (typeof(BaseProtectedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(x => !(x as BaseProtectedEntity).IsDeleted);
            }

            var total = await query.CountAsync();
            if (order.IsNotNullOrEmpty())
            {
                var orderFileds = order.SplitRemoveEmpty(',').Select(x => x.Trim()).ToArray();
                var filedFirst = orderFileds.First();

                var orderFiledFirstParts = filedFirst.SplitRemoveEmpty(' ').Select(x => x.Trim()).ToArray();
                var nameFirst = orderFiledFirstParts[0];
                var isDescFirst = false;
                if (orderFiledFirstParts.Length > 1)
                {
                    isDescFirst = orderFiledFirstParts[1].EqualOrdinalIgnoreCase("desc");
                }
                query = isDescFirst ? query.OrderByDescending(nameFirst) : query.OrderBy(nameFirst);

                if (orderFileds.Count() > 1)
                {
                    for (int i = 1; i < orderFileds.Length; i++)
                    {
                        var orderFiledParts = orderFileds[i].SplitRemoveEmpty(' ').Select(x => x.Trim()).ToArray();
                        var name = orderFiledParts[0];
                        var isDesc = false;
                        if (orderFiledParts.Length > 1)
                        {
                            isDesc = orderFiledParts[1].EqualOrdinalIgnoreCase("desc");
                        }
                        query = isDesc ? ((IOrderedQueryable<TEntity>)query).ThenByDescending(name) : ((IOrderedQueryable<TEntity>)query).ThenBy(name);
                    }
                }
            }

            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return (total, items);
        }

        /// <summary>
        /// 异步分页查询
        /// </summary>
        public async Task<(int count, List<TEntity> items)> PageQueryAsync<Tkey>(int page, int size,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, Tkey>> order, bool isDesc = false)
        {
            var query = Table.AsQueryable();
            if (where != null)
            {
                query = query.Where(where);
            }
            //处理有IsDelete字段表
            if (typeof(BaseProtectedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(x => !(x as BaseProtectedEntity).IsDeleted);
            }
            var total = await query.CountAsync();
            if (order != null) {
                query = isDesc ? query.OrderByDescending(order) : query.OrderBy(order);
            }

            var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();
            return (total, items);
        }

        #endregion

        #region Save

        /// <summary>
        /// 事务性保存
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        } 

        #endregion

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }

    /// <summary>
    /// 主键为Guid类型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class BaseRepository<TEntity> : BaseRepository<TEntity, long> where TEntity : BaseEntity
    {
        protected BaseRepository(VinoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

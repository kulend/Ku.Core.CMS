using AutoMapper;
using Ku.Core.CMS.Domain;
using Dnc.Extensions.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service
{
    public abstract class BaseService
    {

    }

    public abstract class BaseService<TEntity, TDto, TSearch> 
        where TSearch: BaseSearch<TEntity> 
        where TEntity : class
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<ArticleDto></returns>
        public virtual async Task<List<TDto>> GetListAsync(TSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryListAsync<TEntity>(where.ParseToDapperSql(dapper.Dialect), sort as object);
                return Mapper.Map<List<TDto>>(data);
            }
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public virtual async Task<(int count, List<TDto> items)> GetListAsync(int page, int size, TSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryPageAsync<TEntity>(page, size, where.ParseToDapperSql(dapper.Dialect), sort as object);
                return (data.count, Mapper.Map<List<TDto>>(data.items));
            }
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual async Task<TDto> GetByIdAsync(long id)
        {
            using (var dapper = DapperFactory.Create())
            {
                return Mapper.Map<TDto>(await dapper.QueryOneAsync<TEntity>(new { Id = id }));
            }
        }

        /// <summary>
        /// 根据条件取得数据一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual async Task<TDto> GetOneAsync(TSearch where, dynamic sort = null)
        {
            using (var dapper = DapperFactory.Create())
            {
                return Mapper.Map<TDto>(await dapper.QueryOneAsync<TEntity>(where.ParseToDapperSql(dapper.Dialect), sort as object));
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.Create())
            {
                await _dapper.DeleteAsync<TEntity>(new DapperSql("Id IN @Ids", new { Ids = id }));
            }
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual async Task RestoreAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.Create())
            {
                await _dapper.RestoreAsync<TEntity>(new DapperSql("Id IN @Ids", new { Ids = id }));
            }
        }
    }
}

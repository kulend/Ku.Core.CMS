using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain;

namespace Vino.Core.CMS.Data.Common
{
    public interface IDapperRepository
    {

    }

    public interface IDapperRepository<TEntity, in TPrimaryKey> : IDapperRepository where TEntity : Entity<TPrimaryKey>
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
    }

    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, long> where TEntity : BaseEntity
    {

    }
}

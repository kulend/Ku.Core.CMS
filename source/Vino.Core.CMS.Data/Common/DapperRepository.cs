using Ku.Core.Extensions.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain;

namespace Vino.Core.CMS.Data.Common
{
    public abstract class DapperRepository<TEntity, TPrimaryKey> : IDapperRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        protected IDapper _dapper;

        protected DapperRepository(IDapper dapper)
        {
            _dapper = dapper;
        }

        #region 主键查询

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public TEntity GetById(TPrimaryKey id)
        {
            return null;
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return null;
        }

        #endregion
    }

    public abstract class DapperRepository<TEntity> : DapperRepository<TEntity, long> where TEntity : BaseEntity
    {
        protected DapperRepository(IDapper dapper) : base(dapper)
        {
        }
    }
}

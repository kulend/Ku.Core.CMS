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


        #endregion

    }

    public abstract class DapperRepository<TEntity> : DapperRepository<TEntity, long> where TEntity : BaseEntity
    {
        protected DapperRepository(IDapper dapper) : base(dapper)
        {
        }
    }
}

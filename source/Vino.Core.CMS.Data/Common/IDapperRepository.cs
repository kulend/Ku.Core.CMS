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

    }

    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, long> where TEntity : BaseEntity
    {

    }
}

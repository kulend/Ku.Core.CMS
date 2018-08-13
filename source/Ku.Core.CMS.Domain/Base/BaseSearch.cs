
using Dnc.Extensions.Dapper;

namespace Ku.Core.CMS.Domain
{
    public abstract class BaseSearch<T> : DapperSearch<T>
    {
    }

    public abstract class BaseProtectedSearch<T> : BaseSearch<T>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { set; get; } = false;
    }
}

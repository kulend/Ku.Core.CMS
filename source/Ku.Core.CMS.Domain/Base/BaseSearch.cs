
namespace Ku.Core.CMS.Domain
{
    public abstract class BaseSearch<T>
    {
        //[SearchCondition("Name", SearchConditionOperation.Contains)]
        //public string Keyword { set; get; }

        //[CustomCondition] //添加该特性，则不会自动组合到GetExpression中
        //public string Name { set; get; }
    }

    public abstract class BaseProtectedSearch<T> : BaseSearch<T>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { set; get; } = false;
    }
}

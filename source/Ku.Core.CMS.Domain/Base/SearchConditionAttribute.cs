using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Domain
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SearchConditionAttribute : Attribute
    {
        /// <summary>
        /// 属性名，如果为空，则默认为当前属性名
        /// </summary>
        public string PropertyName { set; get; }

        /// <summary>
        /// 操作符
        /// </summary>
        public SearchConditionOperation Operation { set; get; } = SearchConditionOperation.Equal;

        /// <summary>
        /// true:当值为NULL时，忽略当前条件
        /// false：当值为NULL时，添加条件==NULL
        /// </summary>
        public bool IgnoreWhenNull { set; get; } = true;


        public string Sql { set; get; }

        public SearchConditionAttribute()
        {
        }

        public SearchConditionAttribute(string propertyName, SearchConditionOperation operation)
        {
            this.PropertyName = propertyName;
            this.Operation = operation;
        }

        public SearchConditionAttribute(string propertyName)
        {
            this.PropertyName = PropertyName;
        }

        public SearchConditionAttribute(SearchConditionOperation operation)
        {
            this.Operation = operation;
        }

        public SearchConditionAttribute(bool ignoreWhenNull)
        {
            this.IgnoreWhenNull = ignoreWhenNull;
        }
    }

    public enum SearchConditionOperation
    {
        Equal,
        NotEqual,
        Contains,
        NotContains,
        Sql
    }
}

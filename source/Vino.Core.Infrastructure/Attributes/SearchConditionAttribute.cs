using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SearchConditionAttribute : Attribute
    {
        public string PropertyName { set; get; }

        public SearchConditionOperation Operation { set; get; } = SearchConditionOperation.Equal;

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
    }

    public enum SearchConditionOperation
    {
        Equal,
        NotEqual,
        Contains,
        NotContains,
    }
}

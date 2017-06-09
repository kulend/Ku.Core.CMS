using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Vino.Core.CMS.Core.Common
{
    /// <summary>
    /// 基础Entity
    /// </summary>
    public abstract partial class BaseProtectedEntity : BaseEntity
    {
        /// <summary>
        /// 是否被删除
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { set; get; } = false;
    }
}

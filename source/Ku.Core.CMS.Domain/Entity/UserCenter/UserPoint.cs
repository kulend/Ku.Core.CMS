//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserPoint.cs
// 功能描述：用户积分 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-23 10:50
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.CMS.Domain.Enum.UserCenter;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 用户积分
    /// </summary>
    [Table("usercenter_user_point")]
    public class UserPoint : BaseProtectedEntity
    {
        /// <summary>
        /// 积分类型
        /// </summary>
        public EmUserPointType Type { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
	    /// 积分
	    /// </summary>
		public int Points { get; set; }

        /// <summary>
        /// 可用积分
        /// </summary>
        public int UsablePoints { get; set; }

        /// <summary>
        /// 过期积分
        /// </summary>
        public int ExpiredPoints { get; set; }

        /// <summary>
        /// 已用积分
        /// </summary>
        public int UsedPoints { get; set; }

    }

    /// <summary>
    /// 用户积分 检索条件
    /// </summary>
    public class UserPointSearch : BaseProtectedSearch<UserPoint>
    {
        public EmUserPointType? Type { set; get; }
    }
}

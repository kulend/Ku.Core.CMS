//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserPointDto.cs
// 功能描述：用户积分 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-23 10:50
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.UserCenter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 用户积分
    /// </summary>
    public class UserPointDto : BaseProtectedDto
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
        public virtual UserDto User { get; set; }

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
}

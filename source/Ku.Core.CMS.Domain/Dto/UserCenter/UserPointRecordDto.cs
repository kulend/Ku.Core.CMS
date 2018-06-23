//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserPointRecordDto.cs
// 功能描述：用户积分记录 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-23 15:30
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.UserCenter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 用户积分记录
    /// </summary>
    public class UserPointRecordDto : BaseProtectedDto
    {
        /// <summary>
        /// 用户积分ID
        /// </summary>
        public long UserPointId { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public virtual UserPointDto UserPoint { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual UserDto User { set; get; }

        /// <summary>
        /// 变动类型
        /// </summary>
        public EmUserPointChangeType ChangeType { get; set; }

        /// <summary>
        /// 变动数值
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public EmUserPointBusType BusType { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public long BusId { get; set; }

        /// <summary>
        /// 业务说明
        /// </summary>
        [StringLength(500)]
        public string BusDesc { get; set; }

        /// <summary>
	    /// 操作用户ID
	    /// </summary>
		public long? OperatorId { get; set; }

        /// <summary>
        /// 用户员
        /// </summary>
        public virtual UserDto Operator { set; get; }
    }
}

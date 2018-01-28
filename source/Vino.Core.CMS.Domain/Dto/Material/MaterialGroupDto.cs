using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Enum.Material;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.Material
{
    public class MaterialGroupDto : BaseDto
    {
        /// <summary>
        /// 分组类型
        /// </summary>
        public EmMaterialGroupType Type { set; get; } = EmMaterialGroupType.Picture;

        /// <summary>
        /// 分组名称
        /// </summary>
        [Required, MaxLength(30)]
        public string Name { set; get; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public long CreateUserId { set; get; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public UserDto CreateUser { set; get; }
    }
}

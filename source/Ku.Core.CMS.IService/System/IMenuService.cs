//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMenuService.cs
// 功能描述：菜单 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface IMenuService : IBaseService<Menu, MenuDto, MenuSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MenuDto dto);

        #region 其他接口

        Task<List<MenuDto>> GetParentsAsync(long parentId);

        Task<List<MenuDto>> GetSubsAsync(long? parentId);

        Task<List<MenuDto>> GetMenuTreeAsync();

        #endregion
    }
}

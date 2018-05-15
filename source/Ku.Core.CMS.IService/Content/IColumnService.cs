//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IColumnService.cs
// 功能描述：栏目 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-23 14:15
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IColumnService : IBaseService<Column, ColumnDto, ColumnSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(ColumnDto dto);

        Task<List<ColumnDto>> GetParentsAsync(long parentId);
    }
}

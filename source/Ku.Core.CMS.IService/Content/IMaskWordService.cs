//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMaskWordService.cs
// 功能描述：屏蔽关键词 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-03 14:22
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IMaskWordService : IBaseService<MaskWord, MaskWordDto, MaskWordSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MaskWordDto dto);

        /// <summary>
        /// 取得所有标签
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetTags();
    }
}

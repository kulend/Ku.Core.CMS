//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IBrandService.cs
// 功能描述：品牌 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-26 11:09
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Mall
{
    public partial interface IBrandService : IBaseService<Brand, BrandDto, BrandSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(BrandDto dto, long[] CategoryIds);

        Task<BrandDto> GetByIdWithRefAsync(long id);
    }
}
